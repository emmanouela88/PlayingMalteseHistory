using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowHide : MonoBehaviour
{

    public Image imgFather;
    public Image imgDoor;
    //private Color c;
    public float delay;
    public bool coolingDown;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
        imgDoor.fillAmount = 1.0f;
        imgFather.fillAmount = 1.0f;
        coolingDown = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (coolingDown == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                coolingDown = false;
                timer = 0.0f;
            }
            float normalizeValue = Mathf.Lerp(timer / delay, 1.0f, 0.0f);
            imgDoor.fillAmount = normalizeValue;
            imgFather.fillAmount = normalizeValue;
           
        }
        
    }
}
