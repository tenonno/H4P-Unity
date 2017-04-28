using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace RPG
{

    [MoonSharpUserData]
    public class Lua_Object
    {


        private Object rpgObject;

        public float atk = 1;

        public float Hp
        {
            get { return rpgObject.hp; }
            set {
                rpgObject.hp = value;
                
                if (value <= 0.0f)
                {
                    OnDead();
                }
            }
        }

        public Closure onbecomedead;
        public Closure onbecomeidle;
        public Closure onenterframe;

        public Closure onplayerenter;

        /// <summary> ダメージを受けた時のイベント </summary>
        public Closure onattacked;

        public virtual Object New()
        {
            return new Object();
        }

        private Dictionary<string, List<Closure>> eventListeners;

        public Lua_Object()
        {

            //            onattacked

            eventListeners = new Dictionary<string, List<Closure>>();

            if (Map.CurrentMap == null) throw new Exception("アクティブなマップが存在しません");


            Debug.Log("RPGObject が生成されました");

            LuaEngine.Instance.script.DoString(@"

            ");

            rpgObject = New();
            rpgObject.lua = this;
            


            Map.CurrentMap.AddObject(this);

        }


        public void OnDamage(float damage)
        {
            rpgObject.UnityObject.GetComponent<RPG_Animator>().Damage();

            DispatchEvent("attacked");

            Hp -= damage;
        }

        public void Attack()
        {
            rpgObject.UnityObject.GetComponent<RPG_Animator>().Attack();
        }

        // 死亡
        public void OnDead()
        {
            rpgObject.UnityObject.GetComponent<RPG_Animator>().Release();
            Debug.Log("OnDead...");
            Destroy();
            DispatchEvent("becomedead");
        }


        public void Destroy()
        {
            UnityEngine.Object.Destroy(rpgObject.UnityObject);
        }

        public void AddEventListener(string name)
        {

        }

        public void DispatchEvent(string eventName)
        {

            var closure = this.GetType().GetField("on" + eventName).GetValue(this) as Closure;

            if (closure != null)  closure.Call(this);

        }

        public void DispatchEvent(string eventName, params object[] args)
        {

            var closure = this.GetType().GetField("on" + eventName).GetValue(this) as Closure;

            closure.Call(this, args);

        }





        public void Update()
        {


            // Debug.Log("Mod: " + mod);


            rpgObject.Apply();
        }


  

        public void Mod(string mod)
        {

            var resource = Resources.Load("RPG_Prefabs/" + mod);

            // リソースが存在しない場合は "未定義プレハブ" を使用する
            if (resource == null) resource = Resources.Load("RPG_Prefabs/undefined");

            rpgObject.UnityObject = UnityEngine.Object.Instantiate(resource) as GameObject;


            // GameObject に Lua(this) の参照を与える
            rpgObject.UnityObject.AddComponent<RPG_Lua>().luaObject = this;
            
            // GameObject に BoxCollider を追加
            rpgObject.UnityObject.AddComponent<BoxCollider>();
            rpgObject.UnityObject.AddComponent<RPG_ObjectCollider>();
            rpgObject.UnityObject.AddComponent<RPG_Animator>();


        }



        public void Locate(int x, int y)
        {
            // if (rpgObject == null) return;

            Debug.Log("Locateeeeeeee");

            rpgObject.Locate(x, y);

        }

        public void Locate(int x, int y, string mapName)
        {
            // throw new Exception("未実装です");
            Locate(x, y);
        }

    }
}
