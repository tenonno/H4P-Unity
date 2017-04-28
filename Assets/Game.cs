using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using RPG;

class Game : MonoBehaviour
{

    public static Game Instance { get; private set; }

    public Player Player { get; set; }

    public void Awake()
    {
        Debug.Log("Awake Game");
        Instance = this;
    }

    public void OnAttack()
    {
        if (Player.Instance == null) return;

        Player.Instance.lua.Attack();

    }


}

