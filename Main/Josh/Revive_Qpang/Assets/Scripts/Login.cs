using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

    public GameObject usernameField;
    public GameObject usernameText;
    public GameObject passwordField;
    public GameObject passwordText;
    public GameObject toggleBox;
    public GameObject loginButton;

    private string username;
    private string password;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Cycle through options in login section via tab
	    if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (usernameField.GetComponent<InputField>().isFocused)
            {
                passwordField.GetComponent<InputField>().Select();
            }
        }

        // Check elements if they're empty on enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            username = usernameField.GetComponent<InputField>().text;
            password = passwordField.GetComponent<InputField>().text;

            // Checks if element password is empty
            if (password == "")
            {
                passwordText.GetComponent<Text>().text = "* Enter your password!";
                passwordText.GetComponent<Text>().color = Color.red;
                return;
            }

            // Checks if element username is empty
            if (username == "")
            {
                usernameText.GetComponent<Text>().text = "* Enter your username!";
                usernameText.GetComponent<Text>().color = Color.red;
                return;
            }

            loginOnButton();
        }


    }

    // Checks if all elements are permittable or not
    public void loginOnButton()
    {
        bool PW;
        bool UN;

        if (username == "pew")
        {
            UN = true;
        }
        else
        {
            UN = false;
            usernameText.GetComponent<Text>().text = "* Enter a valid username!";
            usernameText.GetComponent<Text>().color = Color.red;
            passwordText.GetComponent<Text>().text = "* Enter a valid password!";
            passwordText.GetComponent<Text>().color = Color.red;
            return;
        }

        if (password == "123")
        {
            PW = true;
        }
        else
        {
            PW = false;
            usernameText.GetComponent<Text>().text = "* Enter a valid username!";
            usernameText.GetComponent<Text>().color = Color.red;
            passwordText.GetComponent<Text>().text = "* Enter a valid password!";
            passwordText.GetComponent<Text>().color = Color.red;
            return;
        }

        if ((PW) && (UN))
        {
            usernameField.GetComponent<InputField>().text = "";
            passwordField.GetComponent<InputField>().text = "";
            print("Login succesfull");
            SceneManager.LoadScene("PlayerMovement");
        }

    }
}
