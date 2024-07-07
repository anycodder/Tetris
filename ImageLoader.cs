using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using NativeShareNamespace;
using UnityEngine.iOS;
using System;
using System.Linq;

public class ImageLoader : MonoBehaviour
{
    public string folderName = "screentetris";
    public RawImage imageDisplay;
    private List<Texture2D> loadedTextures = new List<Texture2D>();
    private int currentImageIndex = 0;
    public string Name;
    public Button shareButton;
    private Texture2D loadedImage;
    private void Start()
    {
        string Name = Login.LoggedInUsername;
        Debug.Log("Logged-in username: " + Name);
        LoadImages();
        shareButton.onClick.AddListener(DownloadCurrentImage);
    }
    
    public void LoadImages()
    {
        string folderName = "screentetris";

        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        if (Directory.Exists(folderPath))
        {
            string[] imageFiles = Directory.GetFiles(folderPath, $"screenshot_{Login.LoggedInUsername}_*.png");

            foreach (string filePath in imageFiles)
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                Texture2D texture = new Texture2D(1280, 1080); // Adjust the size as needed
                Debug.Log(folderPath);
                Debug.Log(imageFiles);
                if (texture.LoadImage(fileData))
                {
                    loadedTextures.Add(texture);
                }
                else
                {
                    Debug.LogWarning("Failed to load image: " + filePath);
                }
            }

            if (loadedTextures.Count > 0)
            {
                currentImageIndex = 0;
                UpdateDisplayedImage();
            }
        }
        else
        {
            Debug.Log("Folder does not exist: " + folderPath);
        }
    }
    public void NextImage()
    {
        if (loadedTextures.Count > 0)
        {
            currentImageIndex = (currentImageIndex + 1) % loadedTextures.Count;
            UpdateDisplayedImage();
        }
    }
    public void PreviousImage()
    {
        if (loadedTextures.Count > 0)
        {
            currentImageIndex = (currentImageIndex - 1 + loadedTextures.Count) % loadedTextures.Count;
            UpdateDisplayedImage();
        }
    }
    private void UpdateDisplayedImage()
    {
        imageDisplay.texture = loadedTextures[currentImageIndex];
    }
    public void DownloadCurrentImage()
    {
        Texture2D currentTexture = loadedTextures[currentImageIndex-1];
        
        byte[] imageBytes = currentTexture.EncodeToPNG();
        
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        
        string filename = $"screenshot_{Login.LoggedInUsername}_{currentImageIndex}.png";
        Debug.Log(filename);

        string destinationPath = Path.Combine(desktopPath, filename);

        File.WriteAllBytes(destinationPath, imageBytes);

        Debug.Log("Image downloaded to desktop: " + destinationPath);
    }
}
