using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Register : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button registerButton;
    public Button goToLoginButton;
    public GameObject panel;
    public GameObject panel2;
    ArrayList credentials;
    
    void Start()
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        registerButton.onClick.AddListener(writeStuffToFile);
        goToLoginButton.onClick.AddListener(goToLoginScene);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/credentials.txt", "");
        }

    }

    void goToLoginScene()
    {
        SceneManager.LoadScene("Login");
    }

    void writeStuffToFile()
    {
        bool isExists = false;
        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

        foreach (var i in credentials)
        {
            if (i.ToString().Contains(usernameInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            Debug.Log($"Username '{usernameInput.text}' already exists");
            panel2.SetActive(false);
            panel.SetActive(true);
            usernameInput.image.color = Color.red;
            passwordInput.image.color = Color.red;
        }

        else
        {
            credentials.Add(usernameInput.text + ":" + passwordInput.text);
            panel.SetActive(false);
            panel2.SetActive(true);
            File.WriteAllLines(Application.dataPath + "/credentials.txt",
                (String[])credentials.ToArray(typeof(string)));
            Debug.Log("Account Registered");
        }
    }
}   
    