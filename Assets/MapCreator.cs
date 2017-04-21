using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class MapCreator : MonoBehaviour
{

    public Texture texture;


    public Material material;

    public int z;

    IEnumerable<int> Range(int size)
    {
        return Enumerable.Range(0, size);
    }

    // Use this for initialization
    void Start()
    {


        var ux = texture.width / 32;
        var uy = texture.height / 32;


        foreach (var x in Range(15))
        {
            foreach (var y in Range(10))
            {

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x, z, y);

                var meshFilter = cube.GetComponent<MeshFilter>();

                var mesh = meshFilter.mesh;


                var uv = mesh.uv;

                var rx = Random.Range(0, 10);
                var ry = Random.Range(0, 10);

                var scale = new Vector2(1f / ux, 1f / uy);

                foreach (var i in Range(mesh.uv.Length))
                {
                    uv[i].Scale(scale);

                    uv[i].x += (1f / ux) * rx;
                    uv[i].y += (1f / uy) * ry;


                }





                mesh.uv = uv;

                material.mainTexture = texture;

                cube.GetComponent<Renderer>().material = material;

            }
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
