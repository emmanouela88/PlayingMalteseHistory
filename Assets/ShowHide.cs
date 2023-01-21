using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowHide : MonoBehaviour
{

    public Image imgFather;
    public float delay;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
        imgFather.color= Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            imgFather.color = Color.Lerp(Color.blue,Color.white,timer);
            timer = delay;
        }
        else
        {
            imgFather.color = Color.Lerp(Color.white, Color.blue, delay);
        }
        
    }
}
