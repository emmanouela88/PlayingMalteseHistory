using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalCheckDropdown : MonoBehaviour
{

    public TMP_Dropdown[] allRowDrpDowns;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < allRowDrpDowns.Length; i++)
        {

            if (!allRowDrpDowns[i].interactable)
            {
                SceneManager.LoadScene(5);
            }
        }
    }
}
