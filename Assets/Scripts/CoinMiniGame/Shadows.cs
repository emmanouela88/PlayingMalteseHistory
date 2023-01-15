using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    public Animator m_animator1;
    public Animator m_animator2;
    public Animator m_animator3;
    public Animator m_animator4;


    public GameObject m_shadow1;
    public GameObject m_shadow2;
    public GameObject m_shadow3;
    public GameObject m_shadow4;

    private int m_mostRecentNumber = 0;

    void Start()
    {
        ChooseNextShadow();

        StartCoroutine(ChooseNextShadowCoroutine(2f)); // TODO THEN PUT 1.75, 1.5? + speed up animators?
           // e.g. m_animator2.speed = 2f; // TODO MAYBE PLAY WITH THIS?

    }

    void ChooseNextShadow()
    {
        if (CreateObjects.m_playing)
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

    private IEnumerator ChooseNextShadowCoroutine(float waitTime)
    {
        while (CreateObjects.m_playing)
        {
            yield return new WaitForSeconds(waitTime);
            ChooseNextShadow();
        }
    }
  
}
