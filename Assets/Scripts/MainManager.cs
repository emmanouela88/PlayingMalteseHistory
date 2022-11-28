using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public PointsToVisit[] pois;

    [Space]
    [Header("UI Elements to show Info")]
    public GameObject pnlMainInfo;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtMainInfo;
    public Image imgInstrument;
    public Button btnClose;


    [Space]
    [Header("Gps Panel")]
    public GameObject pnlGps;
    public Button btnGPS;


    [Space]
    [Header("Variables")]
    public string nameOfLevel;
    public string nameOfARLevel;
    

    [Space]
    [Header("UI Elements general")]
    public GameObject pnlWarning;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI warningMainText;
    public Button btnCloseWarning;
    public Button btnBackMain;
    

    [Space]
    [Header("Map Elements")]
    public OnlineMapsMarker[] poiMarker;
    public OnlineMapsMarker userMarker;

    public PlayMusic pm;

    //Private member variables
    private string saveFile;
    private SaveData saveData;

    [Space]
    [Header("AR Elements")]
    public Button btnARScene;

    private enum SceneState { isPoverty, isQuarantine, isJewish };

    #region UnityMethods
    public void Awake()
    {
        var choiceScene = SceneState.isPoverty;

        switch (choiceScene)
        {
            case SceneState.isPoverty:
                LoadFiles("pois/PovertyPois");
                break;
            case SceneState.isQuarantine:
                LoadFiles("pois/QuarantinePois");
                break;
            case SceneState.isJewish:
                LoadFiles("pois/JewishPois");
                break;
            default:
                LoadFiles("pois/");
                break;
        }
        
                
        //LoadFiles("pois/");
        saveFile = Application.persistentDataPath + "/save.json";

        if (File.Exists(saveFile))
        {
            // get player pref value 
            string fileContents = File.ReadAllText(saveFile);
            saveData = JsonUtility.FromJson<SaveData>(fileContents);
        }
        else
        {
            Debug.Log("Save file: " + saveFile + " does not exist!");
            //File doesn't exist so create it.
            saveData = new SaveData();
            string jsonString = JsonUtility.ToJson(saveData);
            File.WriteAllText(saveFile, jsonString);
        }
        //PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        pnlMainInfo.SetActive(false);
        poiMarker = new OnlineMapsMarker[pois.Length]; //we want same length when creating markers same as the array of our instruments
        
        pnlWarning.SetActive(false);
        for (int i = 0; i < pois.Length; i++)
        {

            poiMarker[i] = OnlineMapsMarkerManager.CreateItem(pois[i].coordinatesPoint);

            poiMarker[i].label = pois[i].nameOfPoint;
            poiMarker[i].texture = pois[i].imgMarker.texture;
            poiMarker[i].scale = 0.5f;

        }
        
        SubscribeToButtons();
        SubscribeToEvents();
        CreateUserMarker();
        
        //DisableAll();
    }

    private void Update()
    {
        OnlineMaps.instance.Redraw();
    }
    #endregion

    #region Methods
    void SubscribeToButtons()
    {
        btnGPS.onClick.AddListener(GameManager.Instance.androidManager.OpenNativeAndroidSettings);
        btnClose.onClick.AddListener(ClosePanels);
        btnCloseWarning.onClick.AddListener(ClosePanels);
        btnBackMain.onClick.AddListener(() => OpenScene(nameOfLevel));
        btnARScene.onClick.AddListener(() => OpenScene(nameOfARLevel));
    }
    void SubscribeToEvents()
    {
        OnlineMapsLocationService.instance.OnLocationChanged += OnLocationChanged;
        //this is where we assign the markers an action so they behave like buttons

        for (int i = 0; i < poiMarker.Length; i++)
        {
            poiMarker[i].OnClick += ShowInformation;
        }

    }


    #region MapMethods
    void ShowInformation(OnlineMapsMarkerBase marker)
    {
        
        if (!pnlMainInfo.activeSelf)
        {
            EaseInPanel(pnlMainInfo, new Vector3(1, 1, 1), 1f);

            for (int i = 0; i < pois.Length; i++)
            {
                if (marker.label == pois[i].nameOfPoint)
                {
                    txtName.text = pois[i].nameOfPoint;
                    imgInstrument.sprite = pois[i].imgMain;
                    txtMainInfo.text = pois[i].mainInfo;
                    //sourceCam.clip = pois[i].clip;
                    /*btnPlaySound.onClick.AddListener(() => PlaySound(sourceCam.clip));
                    btnPauseSound.onClick.AddListener(PauseSound);*/

                }

            }
        }

    }



    //to check if user is near the poi or not
    public void CheckUserPosition()
    {
        if (!IsWithinConstraints())
        {
            pnlWarning.SetActive(true);

        }
        else
        {
            pnlWarning.SetActive(false);

        }
    }
    public bool IsWithinConstraints()
    {
        
        for (int i = 0; i < pois.Length; i++)
        {
            float distance = OnlineMapsUtils.DistanceBetweenPoints(pois[i].coordinatesPoint, OnlineMapsLocationService.instance.position).magnitude;
         
            if (distance < 0.1f)
            {
                Debug.Log("first info: " + pois[i].name + " distance: " + distance + " i: " + i);

                return true;
            }
        }

        
        Debug.Log("Is not within constraints");
        return false;
    }
    private void SetMarkerOnUserPosition(Vector2 position)
    {        
        userMarker.position = position;
        OnlineMaps.instance.Redraw();
    }
    public void OnLocationChanged(Vector2 position)
    {
        
        SetMarkerOnUserPosition(position);
        OnlineMaps.instance.SetPositionAndZoom(position.x, position.y, OnlineMaps.MAXZOOM);
        CheckUserPosition();

        DisableAll();
        for (int i = 0; i < pois.Length; i++)
        {
            float distance = OnlineMapsUtils.DistanceBetweenPoints(pois[i].coordinatesPoint, OnlineMapsLocationService.instance.position).magnitude;
            bool isUnlocked = i <= /*UnlockNextPart.partsUnlocked*/saveData.unlockedPoints.Count || i == 0;

            poiMarker[i].enabled = isUnlocked;

            if (distance < 0.1f && i<= saveData.unlockedPoints.Count)
            {
                Debug.Log("first info: " + pois[i].name + " distance: " + distance + " i: " + i);

                UnlockNewPart(pois[i]);
                poiMarker[i].enabled = true;

                //TODO: In the comment below we have a warning panel for point 0. Should we keep that just for the first
                //or do it everytime?

                #region Not_Used
                /*if (i == 0)
                {
                    pnlWarning.SetActive(true);
                    warningMainText.text = "You need to walk " + (distance * 1000).ToString("f4") + " km, for the next clue.";
                    UnlockNextPart.partsUnlocked = 1;
                    PlayerPrefs.SetInt("Parts Unlocked", 1);
                    
                }
                else if (i == 1)
                {
                    poiMarker[i].enabled = true;
                    UnlockNextPart.partsUnlocked = 2;
                    PlayerPrefs.SetInt("Parts Unlocked", 2);
                    
                }
                else if (i == 2)
                {
                    poiMarker[i].enabled = true;
                    UnlockNextPart.partsUnlocked = 3;
                    PlayerPrefs.SetInt("Parts Unlocked", 3);
                    
                }
                else if (i == 3)
                {
                    poiMarker[i].enabled = true;
                    poiMarker[4].enabled = true;
                    UnlockNextPart.partsUnlocked = 5;
                    PlayerPrefs.SetInt("Parts Unlocked", 5);
                    
                }
                else if (i == 4)
                {
                    poiMarker[i].enabled = true;
                    UnlockNextPart.partsUnlocked = 5;
                    PlayerPrefs.SetInt("Parts Unlocked", 5);
                    
                }*/
                #endregion

                Debug.Log("Unlock Part: " + UnlockNextPart.partsUnlocked);
                
            }
        }

    }

    public void UnlockNewPart(PointsToVisit newPart)
    {
        if (saveData.unlockedPoints.Count < pois.Length && !saveData.unlockedPoints.Contains(newPart))
        {
            // increment unlocked variable 
            saveData.unlockedPoints.Add(newPart);

            string jsonString = JsonUtility.ToJson(saveData);
            File.WriteAllText(saveFile, jsonString);
        }
    }
    private void CreateUserMarker()
    {
        // Remove Marker
        if (userMarker != null)
            OnlineMapsMarkerManager.instance.items.Remove(userMarker);

        // Create Marker
        userMarker = OnlineMapsMarkerManager.CreateItem(new Vector2(OnlineMapsLocationService.instance.position.x, OnlineMapsLocationService.instance.position.y), userMarker.texture, "User");
        userMarker.SetDraggable(false);
        userMarker.scale = 2f; //0.3f;
    }

    #endregion
    void EaseInPanel(GameObject target, Vector3 scale, float time)
    {
        target.SetActive(true);
        iTween.ScaleTo(target, scale, time);
        pm.PlayOnInfo();
    }

    
    void ClosePanels()
    {
        if (pnlMainInfo.activeSelf)
        {
            EaseInPanel(pnlMainInfo, new Vector3(0, 0, 0), 1f);
            Invoke("IsFinished", 1.5f);

        }

        if (pnlWarning.activeSelf)
        {
            EaseInPanel(pnlWarning, new Vector3(0, 0, 0), 1f);
            Invoke("IsFinished", 1.5f);
        }
        pm.PlayTheMusic();


    }

    void IsFinished()
    {
        pnlMainInfo.SetActive(false);
        pnlWarning.SetActive(false);
    }
    void OpenScene(string nameL)
    {
        SceneManager.LoadScene(nameL);
    }

    private void DisableAll()
    {
        // complete the body once for each number between 0 and <length of our object array
        for (int i = 0; i < poiMarker.Length; i++)
        { // btw it's < no <= because length of our array may be 10, but the last object is at index 9 since we start at 0!
            poiMarker[i].enabled = false;
            // note that I only write [ i] with a space in plain text since it otherwise makes text italic on the forum. The space is not needed
        }
    }
    #endregion


    #region LoadFiles
    public void LoadFiles(string fileName)
    {
        pois = Resources.LoadAll(fileName, typeof(PointsToVisit)).Cast<PointsToVisit>().ToArray();

        Debug.Log("Folder Name Audio: " + fileName);
    }
    #endregion
}
