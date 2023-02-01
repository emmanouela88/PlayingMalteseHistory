using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] private RectTransform m_victoryMessage;
    [SerializeField] private TMP_Text m_victoryText;
    [SerializeField] private RectTransform m_defeatMessage;
    [SerializeField] private RectTransform m_FTUEMessage;
    [SerializeField] private RectTransform m_coinInfoMessage;

    public GameObject[] m_coinGameObjects;
    public Text m_timerText;
    public static float m_timer = 30f;
    public int m_targetScore = 10;

    public static int m_currentScore = 0;
    public static bool m_playing = false;

    private SceneManagerScript m_sceneManager;

    private void Start()
	{
        m_victoryMessage.gameObject.SetActive(false);
        m_defeatMessage.gameObject.SetActive(false);
        m_FTUEMessage.gameObject.SetActive(true);
        m_coinInfoMessage.gameObject.SetActive(false);
        m_sceneManager = GetComponent<SceneManagerScript>();
        m_playing = true; // If this is false the shadows were not working
        m_currentScore = 0;
        Time.timeScale = 0f;
    }

	public void StartMiniGame()
    {
        Time.timeScale = 1f;
        m_FTUEMessage.gameObject.SetActive(false);
       // m_playing = true;
        InstantiateCoin();
    }
    public void ResetMinigame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MoreCoinInfo()
    {
        m_coinInfoMessage.gameObject.SetActive(true);
    }

    public void CloseCoinInfo()
    {
        m_coinInfoMessage.gameObject.SetActive(false);
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
        if (!victory) // NOT USED RN - Player always 'wins' 
        {
            m_defeatMessage.gameObject.SetActive(true);
        }
        else
        {
            m_victoryMessage.gameObject.SetActive(true);
            m_victoryText.text = "You have managed to gather " + m_currentScore + " coins."
                + "\nUnfortunately, most of these are ħabbiet."
                + "\nIt’s barely enough to get you through another day, let alone your family.";
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
