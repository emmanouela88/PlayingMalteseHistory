using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] private RectTransform m_victoryMessage;
    [SerializeField] private RectTransform m_defeatMessage;
    [SerializeField] private RectTransform m_FTUEMessage;
    public GameObject[] m_coinGameObjects;
    public Text m_timerText;
    public float m_timer = 30f;
    public int m_targetScore = 10;

    public static int m_currentScore = 0;
    public static bool m_playing = false;

    private SceneManagerScript m_sceneManager;

    private void Start()
	{
        m_victoryMessage.gameObject.SetActive(false);
        m_defeatMessage.gameObject.SetActive(false);
        m_FTUEMessage.gameObject.SetActive(true);
        m_sceneManager = GetComponent<SceneManagerScript>();
        m_playing = false;
        m_currentScore = 0;
        Time.timeScale = 0f;
    }

	public void StartMiniGame()
    {
        Time.timeScale = 1f;
        m_FTUEMessage.gameObject.SetActive(false);
        m_playing = true;
        InstantiateCoin();
    }
    public void ResetMinigame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (m_playing)
        {
            int seconds = Mathf.FloorToInt(m_timer % 60F);
            int milliseconds = Mathf.FloorToInt((m_timer * 100F) % 100F);
            m_timerText.text = seconds.ToString("00") + ":" + milliseconds.ToString("00");

            m_timer -= Time.deltaTime;
            if (m_timer <= 0f)
            {
                m_timer = 0f;
                m_timerText.text = m_timer.ToString();
                GameOver(m_currentScore >= m_targetScore);
            }
        }

    }

    private void GameOver(bool victory)
    {
        m_playing = false;
        // Show the message.
        if (!victory)
        {
            m_defeatMessage.gameObject.SetActive(true);
        }
        else
        {
            m_victoryMessage.gameObject.SetActive(true);
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
