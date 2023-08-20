using UnityEngine;
using System.IO;

public class ScreenshotScript : MonoBehaviour
{
    public RenderTexture renderTexture;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        string date = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day;
        string filename = date + "_" + System.DateTime.Now.Hour + "-" + System.DateTime.UtcNow.Minute;
        ScreenCapture.CaptureScreenshot("./Screenshots/" + filename + ".png", 1);


    }
}