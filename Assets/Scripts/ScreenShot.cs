using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenShot : MonoBehaviour
{
    [MenuItem("Shot/Screenshot")]
    public static void TakeScreenshot()
    {

        ScreenCapture.CaptureScreenshot(Application.persistentDataPath+"screenshot " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");

        Debug.Log("A screenshot was taken in: "+Application.persistentDataPath);

    }
}
