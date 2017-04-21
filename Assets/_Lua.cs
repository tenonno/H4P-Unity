using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;

using UnityEngine.UI;

public class Lua : MonoBehaviour
{

    Script script;

    public InputField inputField;
    public Button runButton;



    void Log(string text)
    {


#if UNITY_EDITOR
        UnityEditor.EditorUtility.DisplayDialog("Notice", text, "OK");
#else
        MobileNativeMessage msg = new MobileNativeMessage("Message Titile", text);
#endif


    }




    // Use this for initialization
    void Start()
    {

        script = new Script();


        script.Globals["log"] = (System.Action<string>)(Log);



        var textAsset = Resources.Load<TextAsset>("test");




        Debug.Log(textAsset.text);



        DynValue res = Script.RunString(textAsset.text);

        inputField.text = @"
            log(1)
        ";

        runButton.onClick.AddListener(RunScript);




    }

    void RunScript()
    {
        var text = inputField.text;

        script.DoString(text);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
