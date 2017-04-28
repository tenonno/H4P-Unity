using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : RPG.Object
{

    public static Player Instance { get; private set; }

    /*
    public GameObject player;

    public string globalName;

    private Vector3 lookPos;

    static private Vector3 p = new Vector3(0, 3f, -5f);

    public float hp;


    */

    public Player()
    {

        Debug.Log("Start!!!!!!!!!");
        Debug.Log(this);

        Debug.Log(Game.Instance);
        Game.Instance.Player = this;
        Instance = this;

    }



    /*

    void Start()
    {
        lookPos = player.transform.position;

        scale = new RPG.Vec3(1f, 1f, 1f);

        hp = 3;


    }

    void Update()
    {


        player.transform.position = position;
        player.transform.localScale = scale;


        lookPos = Vector3.Lerp(lookPos, player.transform.position, 0.2f);

        Camera.main.transform.position = lookPos + p;
        Camera.main.transform.LookAt(lookPos);




    }




    public new IEnumerable<string> Keys()
    {
        foreach (var key in base.Keys())
        {
            yield return key;
        }
        yield return "hp";
    }



    */




}
