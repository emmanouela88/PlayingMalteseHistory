using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTheDropdowns : MonoBehaviour
{
    public SelectionOnDropdowns[] allGameObjects;
    public GameObject pnlVictory;
    // Start is called before the first frame update
    void Start()
    {
        pnlVictory.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int counter = 0;
        for(int i=0;i<allGameObjects.Length;i++)
        {
            if (allGameObjects[i].isDone)
            {
                counter++;               
            }
        }

        if (counter == allGameObjects.Length)
        {
            pnlVictory.SetActive(true);
            Debug.Log("isDone");
        }
    }
}
