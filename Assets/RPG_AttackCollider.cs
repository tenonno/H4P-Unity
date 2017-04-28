using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]

class RPG_AttackCollider : MonoBehaviour
{

    // 攻撃したオブジェクト
    public GameObject attacker;

    void OnTriggerEnter(Collider collider)
    {

        // 自分なら無視
        if (collider.gameObject == attacker) return;

        // RPGObject ではないなら無視
        if (collider.gameObject.GetComponent<RPG_ObjectCollider>() == null) return;

        // Lua_Object を取得する
        var lua = collider.gameObject.GetComponent<RPG_Lua>().luaObject;

        var thisLua = attacker.GetComponent<RPG_Lua>().luaObject;

        // ダメージイベント
        lua.OnDamage(thisLua.atk);

        Debug.Log("OnTriggerEnter: " + collider.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("cube1 hit OnCollisionStay with " + collision.gameObject);
    }

}
