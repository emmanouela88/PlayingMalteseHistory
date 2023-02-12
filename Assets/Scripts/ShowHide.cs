using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowHide : MonoBehaviour
{

    public Image imgFather;
    private Color c;
    public float delay;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
        c = imgFather.color;
        //imgFather.color= Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            c.a = Mathf.Lerp(255,0,timer);
            timer = delay;
        }
        else
        {
            c.a = Mathf.Lerp(0, 255, delay);
        }
        
    }
}
