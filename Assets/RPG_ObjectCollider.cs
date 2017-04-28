using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]

class RPG_ObjectCollider : MonoBehaviour
{

    void OnTriggerStay(Collider collision)
    {
        // Debug.Log("cube1 hit OnTriggerStay with " + collision.gameObject);
    }


    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("cube1 hit OnCollisionEnter with " + collision.gameObject);
    }

}
