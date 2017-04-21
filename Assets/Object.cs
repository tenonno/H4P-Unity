using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RPG
{

    [MoonSharpUserData]
    public class Vec3
    {
        public float x;
        public float y;
        public float z;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vector3(Vec3 value)
        {
            return new Vector3(value.x, value.y, value.z);
        }

        public override string ToString()
        {
            return "[ " + x.ToString("F2") + ", " + y.ToString("F2") + ", " + z.ToString("F2") + " ]";
        }

    }


    [MoonSharpUserData]
    public class Object : MonoBehaviour
    {


        public Vec3 position = new Vec3(0f, 0f, 0f);
        public Vec3 scale;
        public Vec3 rotate = new Vec3(0f, 0f, 0f);

        public Object()
        {

        }

        public void Locate2D(float x, float y)
        {
            position.x = x;
            position.z = y;
        }


        public IEnumerable<string> Keys()
        {
            yield return "position";
            yield return "scale";
            yield return "rotate";
        }


    }
}
