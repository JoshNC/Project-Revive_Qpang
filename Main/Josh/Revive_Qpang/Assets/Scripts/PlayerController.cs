using System;
using System.Collections.Generic;
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
    public float playerJump_Force = 50f;
    public float LookSensitivity = 5f;

    public KeyCode RunKey = KeyCode.LeftShift;
    public KeyCode SlowKey = KeyCode.LeftAlt;
    public KeyCode JumpKey = KeyCode.Space;

    // Statics
    public static bool player_isRunning;
    public static bool player_isGrounded;
    public static bool player_isSlowWalking;

    public static float currentPlayerForwardSpeed = 0;
    public static float currentPlayerStrafeSpeed = 0;
    public static float currentPlayerBackwardsSpeed = 0;
    public static float mouseSensitivity = 0;
    public static float currentPlayerPosX = 0;
    public static float currentPlayerPosY = 0;
    public static float currentPlayerPosZ = 0;
    public static float currentJumpForce = 0;
    public static float runMultiplier = 0;
    public static float slowMultiplier = 0;

    //private fields
    private float moveCounter = 1f;

    private Rigidbody rB;

    // Use this for initialization
    void Start ()
    {
        player_isRunning = false;
        player_isGrounded = true;
        player_isSlowWalking = false;

        mouseSensitivity = LookSensitivity;
        runMultiplier = playerSpeed_RunMultiplier;
        slowMultiplier = playerSpeed_SlowMultiplier;
        currentJumpForce = playerJump_Force;

        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // ==============================================\
        // Movement
        // ==============================================\
        // player position
        currentPlayerPosX = gameObject.GetComponent<Rigidbody>().position.x;
        currentPlayerPosY = gameObject.GetComponent<Rigidbody>().position.y;
        currentPlayerPosZ = gameObject.GetComponent<Rigidbody>().position.z;

        // Calculate movement velocity as a 3D vector
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove * playerSpeed_Strafe;
        Vector3 _moveVertical = transform.forward * _zMove * playerSpeed_Forward;

        // z1: voor, z-1: achter | x1: rechts, x-1 links
        // Speed slope
        if ((Input.GetAxisRaw("Horizontal") == 1) || (Input.GetAxisRaw("Horizontal") == -1)) // right
        {
            if (moveCounter < playerSpeed_Strafe) moveCounter += 0.08f + moveCounter;
            if (moveCounter > playerSpeed_Strafe)
            {
                _moveHorizontal = transform.right * _xMove * playerSpeed_Strafe;
                currentPlayerStrafeSpeed = _xMove * playerSpeed_Strafe;
            } else
            {
                _moveHorizontal = transform.right * _xMove * moveCounter;
                currentPlayerStrafeSpeed = _xMove * moveCounter;
            }
        }
        else if(Input.GetAxisRaw("Vertical") == 1) // Forward
        {
            if (moveCounter < playerSpeed_Forward) moveCounter += 0.08f + moveCounter;
            if (moveCounter > playerSpeed_Forward)
            {
                _moveVertical = transform.forward * _zMove * playerSpeed_Forward;
                currentPlayerForwardSpeed = _zMove * playerSpeed_Forward;
            }
            else
            {
                _moveVertical = transform.forward * _zMove * moveCounter;
                currentPlayerForwardSpeed = _zMove * moveCounter;
            }
        }
        else if (Input.GetAxisRaw("Vertical") == -1) // Backwards
        {
            if (moveCounter < playerSpeed_Backwards) moveCounter += 0.08f + moveCounter;
            if (moveCounter > playerSpeed_Backwards)
            {
                _moveVertical = transform.forward * _zMove * playerSpeed_Backwards;
                currentPlayerBackwardsSpeed = _zMove * playerSpeed_Backwards;
            }
            else
            {
                _moveVertical = transform.forward * _zMove * moveCounter;
                currentPlayerBackwardsSpeed = _zMove * moveCounter;
            }
        }
        else
        {
            moveCounter = 0;
            currentPlayerStrafeSpeed = _xMove * moveCounter;
            currentPlayerForwardSpeed = _zMove * moveCounter;
            currentPlayerBackwardsSpeed = _zMove * moveCounter;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player_isGrounded)
            {
                rB.AddForce(Vector3.up * playerJump_Force);
            }
        }


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

    void OnCollisionStay(Collision collisionInfo)
    {
        player_isGrounded = true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        player_isGrounded = false;
    }
}