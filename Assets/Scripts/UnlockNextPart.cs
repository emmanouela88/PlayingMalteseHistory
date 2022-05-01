using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockNextPart : MonoBehaviour
{
    public int totalParts = 5;
    public static int partsUnlocked = 5; //to turn it back makeit to 1!

    public Text numOfPartsText;

    public Button[] buttonStoryParts; // = new Buttons[totalParts];

    public Text[] bookTitlesText; 

    // Start is called before the first frame update
    void Start()
    {
      // PlayerPrefs.SetInt("Parts Unlocked", 1); // used to reset back to 1

        // get player pref value 
        partsUnlocked = PlayerPrefs.GetInt("Parts Unlocked");

        if (partsUnlocked == 0)
        {
            PlayerPrefs.SetInt("Parts Unlocked", 1); // used for first time so not 0
            partsUnlocked = PlayerPrefs.GetInt("Parts Unlocked");
        }


        // set UI text
        setUIText();

        // enable interactivity
        enableButtons();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockNewPart()
    {
        if (partsUnlocked < totalParts)
        {
            // increment unlocked variable 
            partsUnlocked++;

            // update player pref value
            PlayerPrefs.SetInt("Parts Unlocked", partsUnlocked);

            // show user updated
            setUIText();

            // enable interactivity
            enableButtons();
        }
    }

    void setUIText()
    {
     

        numOfPartsText.text = partsUnlocked.ToString() + " / " + totalParts;

        if (partsUnlocked == 0)
        {
            numOfPartsText.text = 1.ToString() + " / " + totalParts;

        }
    }

    void enableButtons()
    {
        // using parts unlocked to see which to enable 
        for(int i = 0; i < partsUnlocked; i++)
        {
            buttonStoryParts[i].interactable = true;

            // set text to white
            bookTitlesText[i].color = Color.white;

        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Parts Unlocked", 1); // used to reset back to 1
        partsUnlocked = PlayerPrefs.GetInt("Parts Unlocked");

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
