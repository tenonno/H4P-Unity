using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LuaClass
{

    public void Each(string name, Script script)
    {

        var infoArray = this.GetType().GetFields().
            Select(p => p.Name);



        // プロパティ情報出力をループで回す
        foreach (var Name in infoArray)
        {
            // Debug.Log(Name);

            script.DoString(name + "." + Name + "=" + "__" + name + "." + Name);

            //Console.WriteLine(info.Name + ": " + info.GetValue(_person, null));
        }



        var infoArray2 = this.GetType().GetMethods().
            Select(p => p.Name);



        // プロパティ情報出力をループで回す
        foreach (var Name in infoArray2)
        {
            // Debug.Log(Name);

            script.DoString(name + "." + Name + "=" + "__" + name + "." + Name);

            //Console.WriteLine(info.Name + ": " + info.GetValue(_person, null));
        }



    }

    public Type type;

    public void Define(string name, Script script)
    {

        script.Globals["__" + name] = Activator.CreateInstance(type);

        script.Globals[name] = new Table(script);


        Each(name, script);
        script.DoString(name + ".__new = __" + name + ".__new");

    }

}







[MoonSharpUserData]
public class RPG_Emitter
{

    Dictionary<string, List<CallbackFunction>> listeners;


    public RPG_Emitter()
    {
        listeners = new Dictionary<string, List<CallbackFunction>>();
    }

    void addEventListener(string name, CallbackFunction function)
    {

        if (listeners[name] == null) listeners[name] = new List<CallbackFunction>();


        listeners[name].Add(function);
    }

    void dispatchEvent(string name)
    {
        if (listeners[name] == null) return;

        foreach (var a in listeners[name])
        {
            //a.ClrCallback.in)
        }
    }

}



[MoonSharpUserData]
public class RPG_Game : LuaClass
{
    public float b = 3;

    public RPG_Game()
    {
        type = typeof(RPG_Game);
    }


}




[MoonSharpUserData]
public class RPG_Hack
{

    public RPG.Lua_Player player;

    public Closure onload;
    public Closure onscorechange;
    public DynValue maps;

    public void Log(string text)
    {
        Debug.Log(text);
    }

    public void Gameover()
    {
        GameObject.Find("GameOverLayer").GetComponent<FadeIn>().Tween();
        Debug.Log("Game Over");
    }

    public void Gameclear()
    {
        GameObject.Find("GameClearLayer").GetComponent<FadeIn>().Tween();
        Debug.Log("Game Clear");
    }








    private float _score = 0;

    public float Score
    {
        get { return _score; }
        set
        {

            var _value = _score;

            _score = value;

            UnityEngine.GameObject.Find("Score Label").GetComponent<Text>().text = "SCORE: " + value;

            // 仮
            if (value != _value)
            {
                LuaEngine.Instance.script.DoString(@"
                    Hack.onscorechange();
                ");
            }

        }
    }


    public Dictionary<string, string> assets;

    public RPG_Hack()
    {

        assets = new Dictionary<string, string>();


        assets["knight"] = "knight";
        assets["slime"] = "slime";

        Score = 0;


    }


}





public class LuaEngine : MonoBehaviour
{


    public static LuaEngine Instance { get; private set; }

    public Script script;

    public void Reload()
    {
        SceneManager.LoadScene("main");
    }

    void Start()
    {

        Instance = this;

        script = new Script(CoreModules.Preset_Complete);




        // https://github.com/pygy/LuLPeg
        UserData.RegisterAssembly();


        UserData.RegisterType<RPG.Lua_Object>();
        UserData.RegisterType<RPG.Lua_Player>();


        script.Globals["RPGObject"] = typeof(RPG.Lua_Object);
        script.Globals["Player"] = typeof(RPG.Lua_Player);

        script.Globals["RPGMap"] = typeof(RPG.Lua_Map);


        script.Globals["MapObject"] = new Table(script);

        script.Globals["Hack"] = new RPG_Hack();


        script.Globals["log"] = (System.Action<string>)(Log);

        var data = new Dictionary<string, object>();



        // (new RPG_Hack()).Define("Hack", script);
        (new RPG_Game()).Define("game", script);



        //  script.Globals["game"] = data;// new IndexerTestClass();

        /*
        script.DoString(@"
            game.__new = __game.__new;
        ");
        */


        script.Options.ScriptLoader = new MyCustomScriptLoader();



        var text = Resources.Load<TextAsset>("Scripts/_main").text;


        text = ReplaceENV(text);
        text = ReplaceNewClass(text);

        Debug.Log(text);
        script.DoString(text);


        script.DoString(@"
            Hack.onload();
            game.onload();
        ");

    }




    // Update is called once per frame
    void Update()
    {

        var map = RPG.Map.CurrentMap;

        if (map == null) return;

        // マップに存在する全てのオブジェクトを更新する
        foreach (var obj in map.Objects)
        {
            obj.Update();
        }

    }




    private class MyCustomScriptLoader : ScriptLoaderBase
    {
        public override object LoadFile(string file, Table globalContext)
        {


            var resource = Resources.Load<TextAsset>("Scripts/" + file);


            //  Debug.Log("Loading... " + file);


            return resource.text;

        }

        public override string ResolveFileName(string filename, Table globalContext)
        {
            return filename;
        }


        protected override string ResolveModuleName(string modname, string[] paths)
        {
            return modname.Replace(".", "/");
        }

        public override bool ScriptFileExists(string name)
        {
            return true;
        }
    }


    static void Log(string text)
    {
        Debug.Log(text);
    }

    private string ReplaceENV(string text)
    {
        return Regex.Replace(text,
            @"local _ENV = (.+)",
            @"
            local w_ENV = $1
            for k,v in pairs(w_ENV) do _ENV[k] = v end
            ");
    }


    /*
    private string ReplaceNewClass(string text)
    {

        return Regex.Replace(text, @"(?<!_)_(?!_)new\((.+?)\)", @"$1.__new()");
        // return Regex.Replace(text, @"(?<!_)_(?!_)new\((.+?)\)", @"$1.__new()");
    }
    */
    private string ReplaceNewClass(string text)
    {
        return Regex.Replace(text, @"(?<!_)_(?!_)new\((.+?)\)", (match) =>
        {

            // クラス名と引数
            // [ Class, arg1, arg2, ... ]
            var g = match.Groups[1].Value.Split(',');

            var args = string.Join(",", g.Skip(1).ToArray());

            return g[0] + ".__new(" + args + ")";


        });

    }

}
