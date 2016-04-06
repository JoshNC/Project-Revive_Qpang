using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class debugLog : MonoBehaviour {

    public GameObject forwardSpeed;
    public GameObject strafeSpeed;
    public GameObject backwardsSpeed;
    public GameObject mouseSensitivity;
    public GameObject posX;
    public GameObject posY;
    public GameObject posZ;
    public GameObject jumpForce;
    public GameObject runMultiplier;
    public GameObject slowMultiplier;
    public GameObject keysPressed;
    public GameObject isGrounded;
    public GameObject isRunning;
    public GameObject isSlowWalking;

    private PlayerController pC;

    // Use this for initialization
    void Start () {
        pC = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        forwardSpeed.GetComponent<Text>().text = "Forward speed: " + PlayerController.currentPlayerForwardSpeed;
        strafeSpeed.GetComponent<Text>().text = "Strafe speed: " + PlayerController.currentPlayerStrafeSpeed;
        backwardsSpeed.GetComponent<Text>().text = "Backwards Speed: " + +PlayerController.currentPlayerBackwardsSpeed;
        mouseSensitivity.GetComponent<Text>().text = "Mouse sensitivity: " + PlayerController.mouseSensitivity;
        posX.GetComponent<Text>().text = "Pos x: " + PlayerController.currentPlayerPosX;
        posY.GetComponent<Text>().text = "Pos Y: " + PlayerController.currentPlayerPosY;
        posZ.GetComponent<Text>().text = "Pos Z: " + PlayerController.currentPlayerPosZ;
        jumpForce.GetComponent<Text>().text = "Jump force: " + PlayerController.currentJumpForce;
        runMultiplier.GetComponent<Text>().text = "Run multiplier: " + PlayerController.runMultiplier;
        slowMultiplier.GetComponent<Text>().text = "Slow multiplier: " + PlayerController.slowMultiplier;
        isGrounded.GetComponent<Text>().text = "Is grounded: " + PlayerController.player_isGrounded.ToString();
        isRunning.GetComponent<Text>().text = "Is running: " + PlayerController.player_isRunning.ToString();
        isSlowWalking.GetComponent<Text>().text = "Is SlowWalking: " + PlayerController.player_isSlowWalking.ToString();
    }
}
