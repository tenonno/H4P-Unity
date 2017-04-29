/// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(Animator))]

//Name of class must be name of file as well

public class RPG_Animator : MonoBehaviour
{
    protected Animator animator;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // 攻撃中か
    private bool isAttack = false;

    private GameObject attackCollisionSphere = null;

    public void Damage()
    {
        animator.SetBool("Damage", true);
    }
    public void OnDamageStart()
    {
        animator.SetBool("Damage", false);
    }
    public void OnDamageEnd()
    {

    }


    public void Release()
    {
        if (attackCollisionSphere != null) Destroy(attackCollisionSphere);
    }


    public void Dead()
    {
        // 仮
        Damage();
    }

    public void Attack()
    {
        // 既に攻撃中
        if (isAttack) return;



        animator.SetBool("Attack", true);
    }

    // 攻撃開始イベント
    public void OnAttackStart()
    {
        isAttack = true;


        if (attackCollisionSphere != null) Destroy(attackCollisionSphere);

        attackCollisionSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);


        var q = Quaternion.FromToRotation(Vector3.forward, transform.forward);
        Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
        var p = m.MultiplyVector(new Vector3(0, 0.5f, 1));


        attackCollisionSphere.transform.position = transform.position + p;



        attackCollisionSphere.GetComponent<MeshRenderer>().enabled = false;

        // attackCollisionSphere.AddComponent<SphereCollider>();

        attackCollisionSphere.GetComponent<SphereCollider>().isTrigger = true;

        var rigidbody = attackCollisionSphere.AddComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = false;

        attackCollisionSphere.AddComponent<RPG_AttackCollider>().attacker = this.gameObject;

        // 攻撃フラグを false に
        // 攻撃モーションが終わるまで次のモーションに移行しないようにしているので、
        // 開始と同時に false にして問題ない

        animator.SetBool("Attack", false);

    }

    public void OnAttackEnd()
    {

        // 仮
        // モーションで座標がズレるのを防ぐ
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;

        Destroy(attackCollisionSphere);
        attackCollisionSphere = null;

    }


    public void OnIdleStart()
    {
        isAttack = false;
    }


    void Update()
    {


        if (!isAttack)
        {
            GetComponent<RPG_Lua>().luaObject.DispatchEvent("becomeidle");

        }

    }
}
