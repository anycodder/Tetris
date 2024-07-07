using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button goToRegisterButton;
    public GameObject panel3;
    public static string LoggedInUsername { get; private set; }
    private GameManager gameManager;
    private ArrayList credentials;
    
    void Start()
    {
        panel3.SetActive(false);
        loginButton.onClick.AddListener(login);
        goToRegisterButton.onClick.AddListener(moveToRegister);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            Debug.Log("Credential file doesn't exist");
        }
    }

    void login()
    {
        bool isExists = false;
        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

        foreach (var i in credentials)
        {
            string line = i.ToString();
            int indexOfColon = i.ToString().IndexOf(":");
            if (indexOfColon != -1 && i.ToString().Substring(0, indexOfColon).Equals(usernameInput.text) &&
                i.ToString().Substring(indexOfColon + 1).Equals(passwordInput.text))
            {
                isExists = true;
                break;
            }
        }
        
        if (isExists)
        {
            Debug.Log($"Logging in '{usernameInput.text}'");
            SceneManager.LoadScene("Scenes/MainMenu");
            Debug.Log("Scene loaded");
            Login.LoggedInUsername = usernameInput.text;
        }

        else
        {
            Debug.Log("Incorrect credentials");
            panel3.SetActive(true);
            usernameInput.image.color = Color.red;
            passwordInput.image.color = Color.red;
        }
    }
    
    void moveToRegister()
    {
        SceneManager.LoadScene("Register");
    }
}

