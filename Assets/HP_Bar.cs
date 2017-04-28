using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{

    public GameObject heart;

    private List<GameObject> hearts;

    static int maxNum = 20;

    // Use this for initialization
    void Start()
    {


        hearts = new List<GameObject>();


        for (var i = 0; i < maxNum; ++i)
        {


            var obj = Object.Instantiate(heart,
                 new Vector3(140 + i * 45, 0, 0),
                 Quaternion.identity);


            obj.transform.SetParent(transform, false);


            hearts.Add(obj);


        }


    }

    // Update is called once per frame
    void Update()
    {


        if (Game.Instance.Player == null) return;


        for (var i = 0; i < maxNum; ++i)
        {
            var image = hearts[i].GetComponent<Image>();

            var color = image.color;

            color.a = Game.Instance.Player.hp - 1 >= i ? 1f : 0f;

            image.color = color;


        }



    }
}
