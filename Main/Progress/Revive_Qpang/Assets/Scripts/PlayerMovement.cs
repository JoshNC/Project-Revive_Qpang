using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private float playerSpeed_Forward;
    private float playerSpeed_Strafe;
    private float playerSpeed_Backwards;
    private float playerSpeed_RunMultiplier;
    private float playerSpeed_SlowMultiplier;
    private float playerJump_Force;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody rb;
    private PlayerController pC;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        pC = GetComponent<PlayerController>();

        playerSpeed_Forward = pC.playerSpeed_Forward;
        playerSpeed_Strafe = pC.playerSpeed_Strafe;
        playerSpeed_Backwards = pC.playerSpeed_Backwards;
        playerSpeed_RunMultiplier = pC.playerSpeed_RunMultiplier;
        playerSpeed_SlowMultiplier = pC.playerSpeed_SlowMultiplier;
        playerJump_Force = pC.playerJump_Force;
    }

    // Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotation(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    // Runs every physics iteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    // perform movement based on velocity
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    // perform rotation based on velocity
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }

}