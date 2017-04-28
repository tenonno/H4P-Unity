using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RPG
{


    public class Object 
    {
        public virtual void _Start()
        {
        }

        public Lua_Object lua;

        private GameObject _unityObject;

        public GameObject UnityObject
        {
            get { return _unityObject; }
            set
            {
                _unityObject = value;

                defaultPosition = new Vec3(value.transform.position);
                defaultScale = new Vec3(value.transform.localScale);
                defaultRotation = new Vec3(value.transform.rotation.eulerAngles);
            }
        }

        public Vec3 position = new Vec3(0f, 0f, 0f);
        public Vec3 scale = new Vec3(1f, 1f, 1f);
        public Vec3 rotation = new Vec3(0f, 270f, 0f);

        public float hp = 3;

        public Vec3 defaultPosition = new Vec3(0f, 0f, 0f);
        public Vec3 defaultScale = new Vec3(1f, 1f, 1f);
        public Vec3 defaultRotation = new Vec3(0f, 0f, 0f);


        public Object(GameObject unityObject)
        {
            defaultPosition = new Vec3(unityObject.transform.position);
            defaultScale = new Vec3(unityObject.transform.localScale);
            defaultRotation = new Vec3(unityObject.transform.rotation.eulerAngles);
            this.UnityObject = unityObject;
        }

        public Object()
        {

        }

        public void Locate(float x, float y)
        {
            Locate2D(x, y);
        }

        public void Locate2D(float x, float y)
        {
            position.x = x;
            position.z = -y;
        }

        public void Locate3D(float x, float y, float z)
        {
            position.x = x;
            position.y = y;
            position.z = -z;
        }

        private Vector3 DivVec3(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }


        // GameObject に情報を適用する
        public void Apply()
        {
            // まだ GameObject が設定されていない
            if (UnityObject == null) return;

            UnityObject.transform.position = defaultPosition + position;
            UnityObject.transform.localScale = defaultScale * scale;
            UnityObject.transform.rotation = Quaternion.Euler(defaultRotation + rotation);



            // BoxCollider の位置を更新する
            var boxCollider = UnityObject.GetComponent<BoxCollider>();

            var height = scale.y / 2f;

            // スケールの初期値を考慮した値に正規化するためのベクトル
            var normalize = DivVec3(Vector3.one, defaultScale);

            boxCollider.size = Vector3.Scale(normalize, scale);
            boxCollider.center = Vector3.Scale(normalize, new Vector3(0, height, 0));


        }

        public IEnumerable<string> Keys()
        {
            yield return "position";
            yield return "scale";
            yield return "rotation";
            yield return "hp";
        }


    }

}
