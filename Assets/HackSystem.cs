using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackSystem : MonoBehaviour
{

    public InputField editor;

    public Button runButton;

    public GameObject luaEngine;

    void Start()
    {

        editor.text = "log(1)";

        runButton.onClick.AddListener(Run);

    }


    void Run()
    {

        var text = editor.text;

        luaEngine.GetComponent<LuaEngine>().script.DoString(text);

    }
}
