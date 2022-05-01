using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{

    public GameObject[] panels;

    public Button[] btnClose;


    int recentPanel;
    public PlayMusic pm;

    // Start is called before the first frame update
    void Start()
    {
      
        SubscribeToButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUpPanel(int storyPart)
    {
        int panelNum = storyPart - 1; // starting from 0 cause of the array

        recentPanel = panelNum;
        EaseInPanel(panels[panelNum], new Vector3(1, 1, 1), 1f);

     //   EaseInPanel(panels[panelNum], new Vector3(1, 1, 1), 1f);

    //    panels[panelNum].SetActive(true);
    }
    
 /*   public void HidePanel(int storyPart)
    {
        int panelNum = storyPart - 1; // starting from 0 cause of the array
        panels[panelNum].SetActive(false);
    }
*/




    void SubscribeToButtons()
    {
        for (int i = 0; i < btnClose.Length; i++)
        {
            btnClose[i].onClick.AddListener(ClosePanels);
        }

        //     btnCloseWarning.onClick.AddListener(ClosePanels);

    }



    void EaseInPanel(GameObject target, Vector3 scale, float time)
    {
        target.SetActive(true);
        iTween.ScaleTo(target, scale, time);

    }

    void ClosePanels()
    {
        if (panels[recentPanel].activeSelf)
        {
            EaseInPanel(panels[recentPanel], new Vector3(0, 0, 0), 1f);
            Invoke("IsFinished", 0.5f);
        }
        pm.PlayTheMusic();
    }

    void IsFinished()
    {
        panels[recentPanel].SetActive(false);
    }

}
