using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GallerySceneManager : MonoBehaviour
{
    [Header("Fake Buttons")]
    [SerializeField] private GameObject m_povertyFakeButton;
    [SerializeField] private GameObject m_quarantineFakeButton;
    [SerializeField] private GameObject m_jewishFakeButton;

    [Header("Actual Buttons")]
    [SerializeField] private Button m_povertyButton;
    [SerializeField] private Button m_quarantineButton;
    [SerializeField] private Button m_jewishButton;

    [Header("Number of points per story")]
    [SerializeField] private int m_numberOfPovertyPoints;
    [SerializeField] private int m_numberOfQuarantinePoints;
    [SerializeField] private int m_numberOfJewishPoints;


    //Private member variables
    private string m_saveFile;
    private SaveData m_saveData;

    private void Awake()
    {
        m_saveFile = Application.persistentDataPath + "/save.json";

        if (File.Exists(m_saveFile))
        {
            // get player pref value 
            string fileContents = File.ReadAllText(m_saveFile);
            m_saveData = JsonUtility.FromJson<SaveData>(fileContents);
        }
        else
        {
            Debug.Log("Save file: " + m_saveFile + " does not exist!");
            //File doesn't exist so create it.
            m_saveData = new SaveData();
            string jsonString = JsonUtility.ToJson(m_saveData);
            File.WriteAllText(m_saveFile, jsonString);
        }
        //PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        if (m_saveData != null)
		{
            if (m_saveData.unlockedPointsPoverty.Count == m_numberOfPovertyPoints)
			{
                m_povertyFakeButton.SetActive(false);
                m_povertyButton.interactable = true;
            }
			else
			{
                m_povertyFakeButton.SetActive(true);
                m_povertyButton.interactable = false;
            }

            if (m_saveData.unlockedPointsQuarantine.Count == m_numberOfQuarantinePoints)
            {
                m_quarantineFakeButton.SetActive(false);
                m_quarantineButton.interactable = true;
            }
            else
            {
                m_quarantineFakeButton.SetActive(true);
                m_quarantineButton.interactable = false;
            }

            if (m_saveData.unlockedPointsJewish.Count == m_numberOfJewishPoints)
            {
                m_jewishFakeButton.SetActive(false);
                m_jewishButton.interactable = true;
            }
            else
            {
                m_jewishFakeButton.SetActive(true);
                m_jewishButton.interactable = false;
            }
        }
    }

}
