using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace RPG
{
    class Map
    {
        private int w;
        private int h;

        public int[,] data;





        public Map(int w, int h)
        {

            this.w = w;
            this.h = h;
        }



        IEnumerable<int> Range(int size)
        {
            return Enumerable.Range(0, size);
        }


        public void Load()
        {

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
                    var value = data[y,x];


                    var _x = value % uy;
                    var _y = value / ux;


                    Debug.Log(value + ", " + _x + ", " + _y);


                    var cube = Object.Instantiate(_cube);


                    cube.transform.position = new Vector3(x, -0.5f, y);

                    var meshFilter = cube.GetComponent<MeshFilter>();

                    var mesh = meshFilter.mesh;


                    var uv = mesh.uv;

                    var scale = new Vector2(uux, uuy);

                    foreach (var i in Range(mesh.uv.Length))
                    {
                        uv[i].Scale(scale);


                        uv[i].x = 1f - uv[i].x;
                        uv[i].y = 1f - uv[i].y;

                        uv[i].x += (uux) * _x;
                        uv[i].y += (uuy) * _y;

                    }


                    mesh.uv = uv;


                }



            }









        }


    }
}
