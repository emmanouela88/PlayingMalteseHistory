using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectionOnDropdowns : MonoBehaviour
{
    public TMP_Dropdown[] rowDrpDowns;

    
    public void CompareValues(int whichRowWeCheck)
    {
        int firstValue = rowDrpDowns[0].value;
        int secondValue = rowDrpDowns[1].value;
        int thirdValue = rowDrpDowns[2].value;
        int fourthValue = rowDrpDowns[3].value;       

        switch (whichRowWeCheck)
        {
            case 0:
                if (firstValue == 2 && secondValue == 1 & thirdValue == 3 && fourthValue == 0)
                {
                    NonInteractableDropDowns();
                    Debug.Log("FirstValue: " + firstValue.ToString() + " second: " + secondValue.ToString() + " third: " + thirdValue.ToString() + " fourth: " + fourthValue.ToString());
                }
                break;
            case 1:
                if (firstValue == 3 && secondValue == 4 & thirdValue == 4 && fourthValue == 1)
                {
                    NonInteractableDropDowns();
                    Debug.Log("Second row");
                }
                break;
            case 2:
                if (firstValue == 4 && secondValue == 0 & thirdValue == 2 && fourthValue == 2)
                {
                    NonInteractableDropDowns();
                    Debug.Log("Second third");
                }
                break;
            case 3:
                if (firstValue == 0 && secondValue == 3 & thirdValue == 1 && fourthValue == 3)
                {
                    NonInteractableDropDowns();
                    Debug.Log("Second fourth");
                }
                break;
            case 4:
                if (firstValue == 1 && secondValue == 2 & thirdValue == 0 && fourthValue == 4)
                {
                    NonInteractableDropDowns();
                    Debug.Log("Second fifth");

                }
                break;

        }
        if (!rowDrpDowns[3].interactable && !rowDrpDowns[2].interactable && !rowDrpDowns[1].interactable&& !rowDrpDowns[0].interactable)
        {
            SceneManager.LoadScene(5);
        }

    }

    private void NonInteractableDropDowns()
    {
        for (int i = 0; i < rowDrpDowns.Length; i++)
        {
            rowDrpDowns[i].interactable = false;
            
        }
    }
}
