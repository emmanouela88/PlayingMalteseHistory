using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockNextPart : MonoBehaviour
{
    public int totalParts = 5;
    public static List<PointsToVisit> partsUnlocked = new List<PointsToVisit>();

    public Text numOfPartsText;

    public Button[] buttonStoryParts; // = new Buttons[totalParts];

    public Text[] bookTitlesText;

    private string saveFile;
    private SaveData saveData;

    // Start is called before the first frame update
    void Start()
    {
      // PlayerPrefs.SetInt("Parts Unlocked", 1); // used to reset back to 1

        if (File.Exists(saveFile))
        {
            // get player pref value 
            string fileContents = File.ReadAllText(saveFile);
            saveData = JsonUtility.FromJson<SaveData>(fileContents);
            //partsUnlocked = saveData.unlockedPoints;
        }
		else
		{
            Debug.Log("Save file: " + saveFile + " does not exist!");
            //File doesn't exist so create it.
            saveData = new SaveData();
            string jsonString = JsonUtility.ToJson(saveData);
            File.WriteAllText(saveFile, jsonString);
		}

        /*if (partsUnlocked == 0)
        {
            PlayerPrefs.SetInt("Parts Unlocked", 1); // used for first time so not 0
            partsUnlocked = PlayerPrefs.GetInt("Parts Unlocked");
        }*/


        // set UI text
        setUIText();

        // enable interactivity
        enableButtons();

    }

	private void Awake()
	{
        saveFile = Application.persistentDataPath + "/save.json";
        //DontDestroyOnLoad(this.gameObject);
	}
	// Update is called once per frame
	void Update()
    {
        
    }

    public void UnlockNewPart(PointsToVisit newPart)
    {
        if (partsUnlocked.Count < totalParts && !partsUnlocked.Contains(newPart))
        {
            // increment unlocked variable 
            partsUnlocked.Add(newPart);

            //saveData.unlockedPoints = partsUnlocked;
            string jsonString = JsonUtility.ToJson(saveData);
            File.WriteAllText(saveFile, jsonString);

            // show user updated
            setUIText();

            // enable interactivity
            enableButtons();
        }
    }

    void setUIText()
    {
        numOfPartsText.text = partsUnlocked.ToString() + " / " + totalParts;

        if (partsUnlocked.Count == 0)
        {
            numOfPartsText.text = 1.ToString() + " / " + totalParts;

        }
    }

    void enableButtons()
    {
        // using parts unlocked to see which to enable 
        for(int i = 0; i < partsUnlocked.Count; i++)
        {
            buttonStoryParts[i].interactable = true;

            // set text to white
            bookTitlesText[i].color = Color.white;

        }
    }

    public void Reset()
    {
        File.Delete(saveFile);

        setUIText();


        // using parts unlocked to see which to enable 
        for (int i = 1; i < totalParts; i++) // all parts except part 1
        {
            buttonStoryParts[i].interactable = false;

            // set text to gray
            bookTitlesText[i].color = new Color32(144,144,144,255);

        }




    }

}
