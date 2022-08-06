using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    [SerializeField] private string screenshotName = "";
    [SerializeField] private string directoryName = "Screenshots";

    private int screenshotCount = 0;
    private string dirPath = "";

    private void Awake()
    {
        dirPath = "Assets//" + directoryName;
    }

    public void TakeScreenshot()
    {
        Debug.Log("ScreenshotManager - taking screenshot " + screenshotCount.ToString());
        DirectoryInfo screenshotDir = Directory.CreateDirectory(dirPath);
        string fileName = screenshotName + screenshotCount.ToString() + ".png";
        string fullPath = Path.Combine(screenshotDir.FullName, fileName);
        
        ScreenCapture.CaptureScreenshot(fullPath);
        screenshotCount++;
    }

    public void ResetScreenshotCounter()
    {
        screenshotCount = 0;
    }

    public void OnDisable()
    {
        ResetScreenshotCounter();
    }
}
