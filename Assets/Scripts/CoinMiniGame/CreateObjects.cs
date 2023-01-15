using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateObjects : MonoBehaviour
{
    public GameObject[] m_coinGameObjects;
    public Text m_timerText;
    public float m_timer = 30f;

    public static int m_currentScore = 0;
    public static bool m_playing = true;


    void Start()
    {
        InstantiateCoin();
    }

    void Update()
    {
        if (m_playing)
        {
            int seconds = Mathf.FloorToInt(m_timer % 60F);
            int milliseconds = Mathf.FloorToInt((m_timer * 100F) % 100F);
            m_timerText.text = seconds.ToString("00") + ":" + milliseconds.ToString("00");

            if (m_timer >= 0f)
            {
                m_timer -= Time.deltaTime;
            }
            else
            {
                m_playing = false;
                m_timer = 0f;
                m_timerText.text = m_timer.ToString();
            }

        }

    }

    void InstantiateCoin()
    {
        float randomPos = Random.Range(-1.0f, 1.0f); 
        float y = 2.25f; 

        int randomCoin = Random.Range(0, m_coinGameObjects.Length);
        Instantiate(m_coinGameObjects[randomCoin], new Vector3(randomPos, y, 0), Quaternion.identity);

        float newDelay = Random.Range(0.3f, 1f);
        StartCoroutine(WaitBeforeInstantiate(newDelay));
    }

    IEnumerator WaitBeforeInstantiate(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (m_playing)
        {
            InstantiateCoin();
        }
    }

    
}
