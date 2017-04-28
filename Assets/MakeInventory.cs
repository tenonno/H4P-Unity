using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MakeInventory : MonoBehaviour
{

    public GameObject inventoryBox;



    private static int boxNum = 6;

    public List<GameObject> inventoryBoxes;


    // Use this for initialization
    void Start()
    {

        inventoryBoxes = new List<GameObject>();



        for (var i = 0; i < boxNum; ++i)
        {


            var obj = Object.Instantiate(inventoryBox,
                 new Vector3(i * 70 - 2.5f * 70, 60, 0),
                 Quaternion.identity);


            obj.transform.SetParent(transform, false);

            inventoryBoxes.Add(obj);


        }


    }

    void Update()
    {


        var targetPlayer = Player.Instance;
        if (targetPlayer == null) return;


        var index = 0;


        foreach (var obj in inventoryBoxes)
        {


            var text = obj.transform.FindChild("Text").gameObject.GetComponent<Text>();
            text.text = "-";

            var image = obj.GetComponent<Image>();
            var color = image.color;
            color.a = 0.3f;
            image.color = color;



        }


        foreach (var key in targetPlayer.Keys())
        {

            var box = inventoryBoxes[index];


            var text = box.transform.FindChild("Text").gameObject.GetComponent<Text>();
            text.text = key;


            var value = box.transform.FindChild("Value").gameObject.GetComponent<Text>();
            value.text = targetPlayer.GetType().GetField(key).GetValue(targetPlayer).ToString();



            var image = box.GetComponent<Image>();
            var color = image.color;
            color.a = 1f;
            image.color = color;


            ++index;
        }





    }
}
