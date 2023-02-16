using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    public CreateObjects m_gameManager;

    public Animator m_animator1;
    public Animator m_animator2;
    public Animator m_animator3;
    public Animator m_animator4;

    public GameObject m_shadow1;
    public GameObject m_shadow2;
    public GameObject m_shadow3;
    public GameObject m_shadow4;

    private int m_mostRecentNumber = 0;
    private float m_waitTime = 2f;
    private bool m_once = true;

    public void StartAnimating()
    {
        ChooseNextShadow();
        StartCoroutine(ChooseNextShadowCoroutine());
    }

    void Update()
    {
        if (m_gameManager.GetRemainingTime() < 1 && m_once)
        {
            Shadow1();
            Shadow2();
            Shadow3();
            Shadow4();
            m_once = false;
        }
        else if (m_gameManager.GetRemainingTime() < 5)
        {
            m_waitTime = 0.25f;
            ChangeAnimatorsSpeed(3f);
        }
        else if (m_gameManager.GetRemainingTime() < 10)
        {
            m_waitTime = 0.5f;
            ChangeAnimatorsSpeed(2.5f);
        }
        else if (m_gameManager.GetRemainingTime() < 20)
        {
            m_waitTime = 1f;
            ChangeAnimatorsSpeed(1.75f);
        }
        else if (m_gameManager.GetRemainingTime() < 25)
        {
            m_waitTime = 1.5f;
            ChangeAnimatorsSpeed(1.25f);
        }
    }

    void ChooseNextShadow()
    {
        if (m_gameManager.IsPlaying())
        {
            int randomNum = Random.Range(0, 5);

            while (m_mostRecentNumber == randomNum)
            {
                randomNum = Random.Range(0, 5);
            }

            m_mostRecentNumber = randomNum;

            if (randomNum == 0)
            {
                Shadow1();
            }

            if (randomNum == 1)
            {
                Shadow2();
            }

            if (randomNum == 2)
            {
                Shadow3();
            }

            if (randomNum == 3)
            {
                Shadow4();
            }
        }
    }

    void Shadow1()
    {
        m_shadow1.SetActive(false);
        m_shadow1.SetActive(true);
    }

    void Shadow2()
    {
        m_shadow2.SetActive(false);
        m_shadow2.SetActive(true);
    }

    void Shadow3()
    {
        m_shadow3.SetActive(false);
        m_shadow3.SetActive(true);
    }

    void Shadow4()
    {
        m_shadow4.SetActive(false);
        m_shadow4.SetActive(true);
    }

    private IEnumerator ChooseNextShadowCoroutine()
    {
        while (m_gameManager.IsPlaying())
        {
            yield return new WaitForSeconds(m_waitTime);
            ChooseNextShadow();
        }
    }

    private void ChangeAnimatorsSpeed(float newSpeed)
    {
        m_animator1.speed = newSpeed;
        m_animator2.speed = newSpeed;
        m_animator3.speed = newSpeed;
        m_animator4.speed = newSpeed;
    }
}
