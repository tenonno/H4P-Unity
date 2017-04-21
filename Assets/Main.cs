using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


using MoonSharp.Interpreter;


public class Main : MonoBehaviour
{


    public InputField inputField;

    public Button runButton;

    private Game game;

    private Script script;

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

        UserData.RegisterAssembly();



        //UserData.DefaultAccessMode = InteropAccessMode.Preoptimized;


        script = new Script(CoreModules.Preset_HardSandbox);


        script.Globals["Mul"] = (System.Func<int, int, int>)Mul;
        script.Globals["log"] = (System.Action<string>)(Log);


        // player.position.x = 11.45f;
        //script.Globals["Vector3"] = typeof(Vector3);

        script.Globals[player.globalName] = player;




        game = new Game();

        inputField.text = "log(1)";

        runButton.onClick.AddListener(Run);


        game.MakeMap();
        game.OnLoad();



    }

    private static int Mul(int a, int b)
    {
        Debug.Log(Random.Range(0f, 100f));

        return 1;
    }

    void Run()
    {

        var text = inputField.text;



        script.DoString(text);

    }

    // Update is called once per frame
    void Update()
    {

        game.OnEnterFrame();

    }
}
