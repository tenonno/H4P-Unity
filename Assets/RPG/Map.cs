using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace RPG
{
    class Map
    {

        public static Dictionary<int, Map> maps = new Dictionary<int, Map>();

        public static Map CurrentMap { get; private set; }

        public List<RPG.Lua_Player> players = new List<RPG.Lua_Player>();

        public List<Lua_Object> Objects { get; private set; }


        public static int Register(Map map)
        {
            var _id = Map._id++;
            Map.maps[_id] = map;
            return _id;
        }

        public static int _id = 0;


        private int w;
        private int h;

        public int[,] data;


        public void AddObject(Lua_Object luaObject)
        {
            Objects.Add(luaObject);
        }

        public Map(int w, int h)
        {

            this.w = w;
            this.h = h;
           
            Objects = new List<Lua_Object>();

            data = new int[h, w];

            FillData(0);

        }

        public void FillData(int value)
        {
            foreach (var x in Range(data.GetLength(1)))
            {
                foreach (var y in Range(data.GetLength(0)))
                {
                    data[y, x] = value;
                }
            }
        }


        IEnumerable<int> Range(int size)
        {
            return Enumerable.Range(0, size);
        }


        public void Load()
        {

            Map.CurrentMap = this;

            GameObject _cube = Resources.Load<GameObject>("Cube");


            var defaultW = 640;
            var defaultH = 1024;


            var ux = defaultW / 32;
            var uy = defaultH / 32;


            Debug.Log("マップ: " + ux + ", " + uy);

            var uux = 1f / ux;
            var uuy = 1f / uy;


            foreach (var x in Range(data.GetLength(1)))
            {
                foreach (var y in Range(data.GetLength(0)))
                {
                    var value = data[y, x];

                    // value = Random.Range(0, 2) > 0 ? 322 : 324;

                    var _x = value % uy;
                    var _y = value / ux;


                    // Debug.Log(value + ", " + _x + ", " + _y);


                    var cube = UnityEngine.Object.Instantiate(_cube);


                    cube.transform.position = new Vector3(x, -0.5f, -y);

                    var meshFilter = cube.GetComponent<MeshFilter>();

                    var mesh = meshFilter.mesh;


                    var uv = mesh.uv;

                    var scale = new Vector2(uux, uuy);


                    var s1 = Random.Range(0, 10);
                    var s2 = Random.Range(0, 10);

                    foreach (var i in Range(mesh.uv.Length))
                    {

                        // UV を反転する
                        uv[i].x -= 0.5f;
                        uv[i].y -= 0.5f;
                        uv[i].x = uv[i].x * -1f;
                        uv[i].y = uv[i].y * -1f;
                        uv[i].x += 0.5f;
                        uv[i].y += 0.5f;


                        uv[i].x *= uux;
                        uv[i].y *= uuy;

                        //uv[i].y = 1f - uv[i].y;


                        uv[i].x += uux * _x;
                        uv[i].y += uuy * (uy - _y - 1);
                        // uv[i].x += uux * (ux - _x - 1);
                        //uv[i].y += uuy * _y;

                        /*

                        uv[i].x *= uux;
                        uv[i].y *= uuy;

                        /*
                        uv[i].x = uv[i].x;
                        uv[i].y = 1f - uv[i].y;
              
                     * 

                        */

                        // Debug.Log(uv[i].x + ", " +  uv[i].y);

                    }

                    mesh.uv = uv;


                }



            }









        }


    }
}
