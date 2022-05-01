//Class to check if gps is running on android and if not, prompt user to open grant permissions
using UnityEngine;
using UnityEngine.Android;
using GameJamHeritage.Android;
using TMPro;
using System.Collections;

public class AndroidManager : MonoBehaviour
{
    #region Variables
    private OnlineMapsLocationService locationService;
    #endregion

    #region Unity Functions
    private void Start()
    {
        SetupLocationService();
    }

    private void Update()
    {
        // Check if location service is enabled by user
        if (!IsLSEnabledByUser())
            return;

       
    }

    private void OnApplicationQuit()
    {
        if (locationService != null)
            locationService.StopLocationService();
    }
    #endregion

    #region Methods
    private void SetupLocationService()
    {
        locationService = OnlineMapsLocationService.instance;

        if (locationService == null)
        {
            Debug.LogError("Location Service not found.\nAdd Location Service Component (Component / Infinity Code / Online Maps / Plugins / Location Service).");
            return;
        }

    }

    private bool IsLSEnabledByUser()
    {
        if (!(Application.platform == RuntimePlatform.WindowsEditor) && !(Application.platform == RuntimePlatform.OSXEditor))
        {
            if (!Input.location.isEnabledByUser)
            {
                if (!GameManager.Instance.mainManager.pnlGps.activeSelf)
                    GameManager.Instance.mainManager.pnlGps.SetActive(true);
                return false;
            }
            else
            {
                if (GameManager.Instance.mainManager.pnlGps.activeSelf)
                    GameManager.Instance.mainManager.pnlGps.SetActive(false);
                return true;
            }
        }
        else
            return true;
    }

   

    // if true then location servise is activated by user and is receiving GPS data.
    public bool HasGPS()
    {
        if (locationService == null)
            return false;

        // Check if platform is editor
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            // Reurn true if gps emulator is enabled and false if it is not.
            return locationService.useGPSEmulator;
        }
        else
        {
            // If location service is running then we are receiving gps data normally.
            /*if (locationService.IsLocationServiceRunning())
                return true;
            else
                return locationService.TryStartLocationService();*/
            return (locationService.IsLocationServiceRunning() && (Input.location.lastData.horizontalAccuracy <= locationService.desiredAccuracy));
        }
    }

    //open the grant permissions on android devices
    public void OpenNativeAndroidSettings()
    {
       /* if (locationService.useGPSEmulator)
        {
            GameManager.Instance.mainManager.pnlGps.SetActive(false);
        }
        else
        {*/
            AndroidBridge.OpenIntent(IntentNames.GPS_SETTINGS);
        //}
           
        
    }
    #endregion

    
}

public static class IntentNames
{
    public const string WIFI_SETTINGS = "android.settings.WIFI_SETTINGS";
    public const string VPN_SETTINGS = "android.net.vpn.SETTINGS";
    public const string BLUETOOTH_SETTINGS = "android.settings.BLUETOOTH_SETTINGS";
    public const string GPS_SETTINGS = "android.settings.LOCATION_SOURCE_SETTINGS";
}
