/// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(Animator))]

//Name of class must be name of file as well

public class Motion : MonoBehaviour
{

    protected Animator animator;

    private float speed = 0;
    private float direction = 0;
    private Locomotion locomotion = null;


    public Player player;




    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
    }

    private Vector2 currentDirection = Vector2.up;




    public void _Do(Transform root, Transform camera, ref float speed, ref float direction)
    {
        Vector3 rootDirection = root.forward;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vertical = CrossPlatformInputManager.GetAxisRaw("Vertical");


        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

        // Get camera rotation.    

        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f; // kill Y
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

        // Convert joystick input in Worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection;

        Vector2 speedVec = new Vector2(horizontal, vertical);
        speed = Mathf.Clamp(speedVec.magnitude, 0, 1);

        if (speed > 0.01f) // dead zone
        {
            Vector3 axis = Vector3.Cross(rootDirection, moveDirection);
            direction = Vector3.Angle(rootDirection, moveDirection) / 180.0f * (axis.y < 0 ? -1 : 1);
        }
        else
        {
            direction = 0.0f;
        }
    }






    void Update()
    {
        if (animator && Camera.main)
        {
            _Do(transform, Camera.main.transform, ref speed, ref direction);
            locomotion.Do(speed * 6, direction * 180);


        }
    }

    private void LateUpdate()
    {
        player.position = new RPG.Vec3(
            transform.position.x,
            transform.position.y,
            transform.position.z);

    }


    /*

            void Update()
    {

        // 右・左
        float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        // 上・下
        float y = CrossPlatformInputManager.GetAxisRaw("Vertical");

        // 移動する向きを求める
        Vector2 _direction = new Vector2(x, y).normalized;

        Debug.Log(_direction);
        Debug.Log(_direction.magnitude);
        // 移動の制限
        //    Move(direction);


        // 移動していない
        if (_direction.magnitude <= 0.0005) return;


        //_direction.




        var targetDirection = _direction;// a * Mathf.Rad2Deg;


        var direction = targetDirection - currentDirection;
        currentDirection = targetDirection;

        var a = Mathf.Atan2(direction.x, direction.y);


        locomotion.Do(6, a);

        if (animator && Camera.main)
        {
            JoystickToEvents.Do(transform, Camera.main.transform, ref speed, ref direction);
            //  locomotion.Do(speed * 6, a);
        }
    }

    */

}
