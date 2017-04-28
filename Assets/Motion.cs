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

        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<RPG_Animator>().Attack();
        }



    }

    private void LateUpdate()
    {

        if (Player.Instance == null) return;

        Player.Instance.position = new RPG.Vec3(
            transform.position.x,
            transform.position.y,
            transform.position.z);


        Player.Instance.rotation = new RPG.Vec3(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z);





    }


}
