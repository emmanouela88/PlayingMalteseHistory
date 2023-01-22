using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MerchantGameManager : MonoBehaviour
{
    [SerializeField] private List<MerchantController> m_merchants;

    [Header("UI objects")]
    [SerializeField] private RectTransform m_victoryMessage;
    [SerializeField] private RectTransform m_defeatMessage;
    [SerializeField] private RectTransform m_FTUEMessage;
    [SerializeField] private TMPro.TextMeshProUGUI m_timeText;
    [SerializeField] private TMPro.TextMeshProUGUI m_scoreText;
    [SerializeField] private int m_targetScore = 20;

    // Hardcoded variables you may want to tune.
    private float m_startingTime = 30f;

    // Global variables
    private float m_timeRemaining;
    private HashSet<MerchantController> m_currentMerchants = new HashSet<MerchantController>();
    private int m_score;
    private bool m_playing = false;
    private SceneManagerScript m_sceneManager;
    void Start()
    {
        m_victoryMessage.gameObject.SetActive(false);
        m_defeatMessage.gameObject.SetActive(false);
        m_FTUEMessage.gameObject.SetActive(true);
        m_sceneManager = GetComponent<SceneManagerScript>();
        Time.timeScale = 0f; //Start the game as paused
    }

    // This is public so the play button can see it.
    public void StartGame()
    {
        Time.timeScale = 1f;
        m_FTUEMessage.gameObject.SetActive(false);
        // Hide all the visible moles.
        for (int i = 0; i < m_merchants.Count; i++)
        {
            m_merchants[i].Hide();
            m_merchants[i].SetIndex(i);
        }
        // Remove any old game state.
        m_currentMerchants.Clear();
        // Start with 30 seconds.
        m_timeRemaining = m_startingTime;
        m_score = 0;
        m_scoreText.text = "0";
        m_playing = true;
    }

    public void ResetMinigame()
    {
        m_sceneManager.ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameOver(int type)
    {
        // Show the message.
        if (type == 0)
        {
            m_defeatMessage.gameObject.SetActive(true);
        }
        else if (type == 1)
		{
            m_victoryMessage.gameObject.SetActive(true);
		}
        // Hide all moles.
        foreach (MerchantController merchant in m_merchants)
        {
            merchant.StopGame();
        }
        // Stop the game and show the start UI.
        m_playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playing)
        {
            // Update time.
            m_timeRemaining -= Time.deltaTime;
            if (m_timeRemaining <= 0)
            {
                m_timeRemaining = 0;
                GameOver(0);
            }
            else if (m_score >= m_targetScore)
			{
                GameOver(1);
			}
            m_timeText.text = $"{(int)m_timeRemaining / 60}:{(int)m_timeRemaining % 60:D2}";
            // Check if we need to start any more moles.
            if (m_currentMerchants.Count <= (m_score / 10))
            {
                // Choose a random mole/merchant.
                int index = Random.Range(0, m_merchants.Count);
                // Doesn't matter if it's already doing something, we'll just try again next frame.
                if (!m_currentMerchants.Contains(m_merchants[index]))
                {
                    m_currentMerchants.Add(m_merchants[index]);
                    m_merchants[index].Activate(m_score / 10);
                }
            }
        }
    }

    public void AddScore(int merchantIndex)
    {
        // Add and update score.
        m_score += 1;
        m_scoreText.text = $"{m_score}";
        // Increase time by a little bit.
        m_timeRemaining += 1;
        // Remove from active moles.
        m_currentMerchants.Remove(m_merchants[merchantIndex]);
    }

    public void Missed(int merchantIndex, bool isMerchant = true)   //setting default value to true, since we only have one tpy eof merchant
    {
        if (isMerchant)
        {
            // Decrease time by a little bit.
            m_timeRemaining -= 2;
        }
        // Remove from active moles.
        m_currentMerchants.Remove(m_merchants[merchantIndex]);
    }
}
