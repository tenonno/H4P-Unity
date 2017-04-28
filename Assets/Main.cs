using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


using MoonSharp.Interpreter;




public class Main : MonoBehaviour
{

    private Game game;


    public GameObject luaEngine;

    public Player player;


    void Log(string text)
    {


#if UNITY_EDITOR
        UnityEditor.EditorUtility.DisplayDialog("Notice", text, "OK");
#else
        MobileNativeMessage msg = new MobileNativeMessage("Message Titile", text);
#endif


    }

    /// <summary>
    /// Lua 実行時のエラーハンドリングを行います。
    /// </summary>
    static void DoError()
    {
        throw new ScriptRuntimeException("This is an exceptional message, no pun intended.");
    }


    // Use this for initialization
    void Start()
    {

        var script = luaEngine.GetComponent<LuaEngine>().script;


        script.Globals["log"] = (System.Action<string>)(Log);


        // player.position.x = 11.45f;
        //script.Globals["Vector3"] = typeof(Vector3);


        // script.Globals[player.globalName] = player;




        game = new Game();


        // game.MakeMap();
        // game.OnLoad();



    }



    // Update is called once per frame
    void Update()
    {



    }
}
