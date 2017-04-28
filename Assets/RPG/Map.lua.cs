using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RPG
{

    /// <summary>
    /// Lua から参照できる RPGMap のサンドボックス
    /// </summary>
    [MoonSharpUserData]
    public class Lua_Map
    {
        private int _id;

        public string imagePath;

        private Map map;


        public Lua_Map(int _x, int _y, int x, int y)
        {

            Debug.Log("RPGObject constractor");


            map = new Map(x, y);


            _id = Map.Register(map);


        }


        public string Type
        {
            set
            {

                var result = LuaEngine.Instance.script.DoString(String.Format(@"
                    return MapObject.dictionary['{0}'];
                ", value));

                map.FillData((int)result.Number);

            }
        }


        public void Load()
        {

            map.Load();

            Debug.Log("RPGMap load");
            Debug.Log(map);
        }

    }
}
