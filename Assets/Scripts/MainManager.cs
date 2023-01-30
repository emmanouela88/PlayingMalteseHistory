using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
public enum LOCATIONSCENES { Poverty, Quarantine, Jewish };


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
    public Button btnNext;
    public string poisFolder;


    [Space]
    [Header("Gps Panel")]
    public GameObject pnlGps;
    public Button btnGPS;


    [Space]
    [Header("Variables")]
    public string nameOfMainLevel;
    public string nameOfARLevel;//we need to make it as a list since we are having more than one ar level to load    
    public string nameOfARLevel2;//we need to make it as a list since we are having more than one ar level to load    
    public string nameOfMiniGameLevel; //we need to make it as a list since we are having more than one minigame level to load
    public string nameOfMiniGameLevel2; //we need to make it as a list since we are having more than one minigame level to load
    public string nameOfMiniGameLevel3; //we need to make it as a list since we are having more than one minigame level to load
    public LOCATIONSCENES currentLocationScene;
    

    [Space]
    [Header("UI Elements general")]
    public GameObject pnlWarning;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI warningMainText;
    public Button btnCloseWarning;
    public Button btnBackMain;
    public Button btnMiniGame;
    public Button btnMiniGame2;
    public Button btnMiniGame3;
    
    

    [Space]
    [Header("Map Elements")]
    public OnlineMapsMarker[] poiMarker;
    public OnlineMapsMarker userMarker;

    public PlayMusic pm;

    [Space]
    [Header("AR Elements")]
    public Button btnARScene;
    public Button btnARScene2;

    //Private member variables
    private string saveFile;
    private SaveData saveData;

    private List<PointsToVisit> currentStorySavedPoints = new List<PointsToVisit>();

    #region UnityMethods


    public void Awake()
    {                
        LoadFiles(poisFolder);
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
        switch (currentLocationScene)
        {
            case LOCATIONSCENES.Poverty:
                currentStorySavedPoints = saveData.unlockedPointsPoverty;
                break;
            case LOCATIONSCENES.Quarantine:
                currentStorySavedPoints = saveData.unlockedPointsQuarantine;
                break;
            case LOCATIONSCENES.Jewish:
                currentStorySavedPoints = saveData.unlockedPointsJewish;
                break;
        }

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
        
        DisableAll();
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
        btnBackMain.onClick.AddListener(() => OpenScene(nameOfMainLevel));
        btnARScene.onClick.AddListener(() => OpenScene(nameOfARLevel));
        btnARScene2.onClick.AddListener(() => OpenScene(nameOfARLevel2));
        btnMiniGame.onClick.AddListener(() => OpenScene(nameOfMiniGameLevel));
        btnMiniGame2.onClick.AddListener(() => OpenScene(nameOfMiniGameLevel2));
        btnMiniGame3.onClick.AddListener(() => OpenScene(nameOfMiniGameLevel3));
        
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
                    txtMainInfo.text = pois[i].firstPartInfo;
                    btnNext.onClick.AddListener(()=> Changetext(pois[i].secondPartInfo));
                    
                    //sourceCam.clip = pois[i].clip;
                    /*btnPlaySound.onClick.AddListener(() => PlaySound(sourceCam.clip));
                    btnPauseSound.onClick.AddListener(PauseSound);*/
                    if (pois[i].nameOfPoint == "City Gate" || pois[i].nameOfPoint== "Marsamxett" || pois[i].nameOfPoint == "Archbishop's Palace")
                    {
                        btnARScene.gameObject.SetActive(true);
                        btnARScene2.gameObject.SetActive(false);
                    }
                    else if(pois[i].nameOfPoint == "Ondina's Residency")
                    {
                        btnARScene.gameObject.SetActive(false);
                        btnARScene2.gameObject.SetActive(true);
                    }
                    else
                    {
                        btnARScene.gameObject.SetActive(false);
                        btnARScene2.gameObject.SetActive(false);
                    }

                    if (pois[i].nameOfPoint == "Nix Mangiare steps" || pois[i].nameOfPoint == "Hastings Garden"
                        || pois[i].nameOfPoint == "Ondina's Residency")
                    {
                        btnMiniGame.gameObject.SetActive(true);
                        btnMiniGame3.gameObject.SetActive(false);
                        btnMiniGame2.gameObject.SetActive(false);
                    }
                    else if (pois[i].nameOfPoint == "Wells Street")
                    {
                        btnMiniGame.gameObject.SetActive(false);
                        btnMiniGame2.gameObject.SetActive(true);
                        btnMiniGame3.gameObject.SetActive(false);
                    }
                    else if (pois[i].nameOfPoint == "227 St Paul’s Street ")
                    {
                        btnMiniGame.gameObject.SetActive(false);
                        btnMiniGame2.gameObject.SetActive(false);
                        btnMiniGame3.gameObject.SetActive(true);
                    }
                    else
                    {
                        btnMiniGame.gameObject.SetActive(false);
                        btnMiniGame2.gameObject.SetActive(false);
                        btnMiniGame3.gameObject.SetActive(false);
                    }
                }

            }
        }

    }

    private void Changetext(string newText)
    {
        txtMainInfo.text = newText;
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
         
            if (distance < 0.01f)
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

        
        //DisableAll();
        for (int i = 0; i < pois.Length; i++)
        {
            float distance = OnlineMapsUtils.DistanceBetweenPoints(pois[i].coordinatesPoint, OnlineMapsLocationService.instance.position).magnitude;
            
            
            bool isUnlocked = i <= /*UnlockNextPart.partsUnlocked*/currentStorySavedPoints.Count || i == 0;

            poiMarker[i].enabled = isUnlocked;
            if (distance < 0.1f && i<= currentStorySavedPoints.Count)
            {
                Debug.Log("first info: " + pois[i].name + " distance: " + distance + " i: " + i);

                pnlWarning.SetActive(true);
                warningMainText.text = "You need to walk " + (distance * 1000).ToString("f4") + " km, for the next clue.";
                UnlockNewPart(pois[i]);//if we put pois[i] will unlock all the points in-game. Is good for testing not for final development
                poiMarker[i].enabled = true;//same as above. On 0 we only unlock the first poi


                //TODO: In the comment below we have a warning panel for point 0. Should we keep that just for the first
                //or do it everytime?

                #region Not_Used
                /*
                switch (i)
                {
                    case 0:
                        UnlockNewPart(pois[1]);
                        pnlWarning.SetActive(true);
                        warningMainText.text = "You need to walk " + (distance * 1000).ToString("f4") + " km, for the next clue.";
                        poiMarker[0].enabled = true;
                        //UnlockNextPart.partsUnlocked.Where(pm => pm.name == pois[0].name);
                        //PlayerPrefs.SetInt("Parts Unlocked", 1);
                        break;
                    case 1:
                        UnlockNewPart(pois[2]);
                        poiMarker[1].enabled = true;
                        //UnlockNextPart.partsUnlocked.Where(pm => pm.name == pois[1].name);
                        //PlayerPrefs.SetInt("Parts Unlocked", 2);
                        break;
                    case 2:
                        UnlockNewPart(pois[3]);
                        poiMarker[2].enabled = true;
                        //UnlockNextPart.partsUnlocked.Where(pm => pm.name == pois[2].name);
                        //PlayerPrefs.SetInt("Parts Unlocked", 3);
                        break;
                    case 3:
                        UnlockNewPart(pois[4]);
                        poiMarker[3].enabled = true;
                        poiMarker[4].enabled = true;
                        //UnlockNextPart.partsUnlocked.Where(pm => pm.name == pois[3].name);
                        //PlayerPrefs.SetInt("Parts Unlocked", 5);
                        break;
                    case 4:
                        UnlockNewPart(pois[5]);
                        poiMarker[5].enabled = true;
                        //UnlockNextPart.partsUnlocked.Where(pm => pm.name == pois[5].name);
                        //PlayerPrefs.SetInt("Parts Unlocked", 5);
                        break;
                    default:
                        Debug.Log("info: " + pois[i].name + " distance: " + distance + " i: " + i);
                        break;
                }
                */
                #endregion

            }
        }

    }

  
    public void UnlockNewPart(PointsToVisit newPart)
    {
        if (currentStorySavedPoints.Count < pois.Length && !currentStorySavedPoints.Contains(newPart))
        {
            // increment unlocked variable 
            currentStorySavedPoints.Add(newPart);

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

        Debug.Log("Folder Name: " + fileName);
    }
    #endregion
}
