using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowHide : MonoBehaviour
{
    
    public Image imgDoor;
    public float delay;
    public float cooldownTime;
    private bool coolingDown;
    private float fillAmount;
    private float fillTimer;
    private float cooldownTimer;

    void Start()
    {
        imgDoor.fillAmount = 0.0f;
        
        coolingDown = true;
        fillAmount = 0.0f;
        fillTimer = 0.0f;
        cooldownTimer = cooldownTime;
    }

    void Update()
    {
        if (coolingDown)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else
            {
                fillTimer += Time.deltaTime;
                fillAmount = Mathf.Clamp(fillTimer / delay, 0.0f, 1.0f);
                imgDoor.fillAmount = fillAmount;
                

                if (fillAmount >= 1.0f)
                {
                    fillAmount = 1.0f;
                    coolingDown = false;
                    cooldownTimer = cooldownTime;
                }
            }
        }
        else
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else
            {
                fillTimer -= Time.deltaTime;
                fillAmount = Mathf.Clamp(fillTimer / delay, 0.0f, 1.0f);
                imgDoor.fillAmount = fillAmount;
                

                if (fillAmount <= 0.0f)
                {
                    fillAmount = 0.0f;
                    coolingDown = true;
                    cooldownTimer = cooldownTime;
                }
            }
        }//if cooldown


    }
}

