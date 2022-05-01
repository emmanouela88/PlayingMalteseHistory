using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnPage : MonoBehaviour
{

    public GameObject part1;
    public GameObject part2;
    public GameObject extraInfo;
    public GameObject moreText;
    public GameObject nextPrevButton;


    public int currentPage = 1;

    public TMP_Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePage()
    {
        if (currentPage == 1)
        {
            part1.SetActive(false);
            part2.SetActive(true);

            buttonText.text = "Previous";

            extraInfo.SetActive(true);

            currentPage = 2;
        }

        else if(currentPage == 2)
        {
            part1.SetActive(true);
            part2.SetActive(false);
            buttonText.text = "Next";

            extraInfo.SetActive(false);


            currentPage = 1;

        }
    }

    public void resetToPage1()
    {
        part1.SetActive(true);
        part2.SetActive(false);
        buttonText.text = "Next";

        extraInfo.SetActive(false);


        moreText.SetActive(false);
        nextPrevButton.SetActive(true);



        currentPage = 1;
    }

    public void more()
    {
        part1.SetActive(false);
        part2.SetActive(false);

        moreText.SetActive(true);
        extraInfo.SetActive(false);
        nextPrevButton.SetActive(false);


    }


}
