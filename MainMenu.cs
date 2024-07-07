using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public ScreenShotter ScreenShotter;
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("NewGame");
    }
    
    public void Records()
    {
        SceneManager.LoadScene("Records");
    }
    
    public void GoToRakings()
    {
        SceneManager.LoadScene("Scores");
    }
    
    public void QuitGame()
    {
        Debug.Log("Oyundan Çık");
        Application.Quit();
    }
    
    public void GoToLogin()
    {
        SceneManager.LoadScene("Login");
    }
    
    public void GoToLevel1()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void GoToLevel2()
    {
        SceneManager.LoadScene("Game2");
    }
    
    public void GoToLevel3()
    {
        SceneManager.LoadScene("Game3");
    }
    
    public void GoToLevel4()
    {
        SceneManager.LoadScene("Game4");
    }
    
    public void GoToLevel5()
    {
        SceneManager.LoadScene("Game5");
    }
}
