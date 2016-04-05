using System;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : NetworkBehaviour
{

    // Public fields
    public float playerSpeed_Forward = 10f;
    public float playerSpeed_Strafe = 7f;
    public float playerSpeed_Backwards = 5f;
    public float playerSpeed_RunMultiplier = 2f;
    public float playerSpeed_SlowMultiplier = 2f;
    public float playerJump_Force = 30f;
    public float LookSensitivity = 5f;

    public KeyCode RunKey = KeyCode.LeftShift;
    public KeyCode SlowKey = KeyCode.LeftAlt;
    public KeyCode JumpKey = KeyCode.Space;

    // Statics
    public static bool player_isRunning;
    public static bool player_isGrounded;
    public static bool player_isSlowWalking;


    // Use this for initialization
    void Start ()
    {
        player_isRunning = false;
        player_isGrounded = true;
        player_isSlowWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ==============================================\
        // Movement
        // ==============================================\
        // Calculate movement velocity as a 3D vector
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove * playerSpeed_Strafe;
        Vector3 _moveVertical = transform.forward * _zMove * playerSpeed_Forward;

        // Final movement vector
        Vector3 _velocityGrounded = (_moveHorizontal + _moveVertical);

        GetComponent<PlayerMovement>().Move(_velocityGrounded);


        // ==============================================\
        // Camera look
        // ==============================================\
        // Calculate Rotation as a 3D vector
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * LookSensitivity;

        // apply  rotation
        GetComponent<PlayerMovement>().Rotation(_rotation);

        // Calculate camera as a 3D vector
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * LookSensitivity;

        // apply  rotation
        GetComponent<PlayerMovement>().RotateCamera(_cameraRotation);
    }
}