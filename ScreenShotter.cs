using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling.Experimental;
using UnityEngine.UI;
using System.IO;

public class ScreenShotter : MonoBehaviour
{
    public string folderName = "screentetris"; 
    public string Name;
    public void CaptureScreenshot()
    {
        string Name = Login.LoggedInUsername;
        Debug.Log("Logged-in username for screenshotter: " + Name);
        
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        Debug.Log(folderPath);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        
        string fileName = "screenshot_" + Login.LoggedInUsername + "_" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
        Debug.Log(fileName);
        string filePath = Path.Combine(folderPath, fileName);
        
        ScreenCapture.CaptureScreenshot(filePath);

        Debug.Log("Screenshot captured and saved to: " + filePath);
    }
}


