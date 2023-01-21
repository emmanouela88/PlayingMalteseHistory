using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlagueMinigameManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_victoryMessage;
    [SerializeField] private RectTransform m_defeatMessage;
    [SerializeField] private RectTransform m_FTUEMessage;
    [SerializeField] private PlagueMinigamePlayerController m_player;
    [SerializeField] private List<PlagueMinigameGuard> m_guards;

    private SceneManagerScript m_sceneManager;
    private bool m_ended = false;
    // Start is called before the first frame update
    void Start()
    {
        m_victoryMessage.gameObject.SetActive(false);
        m_defeatMessage.gameObject.SetActive(false);
        m_FTUEMessage.gameObject.SetActive(true);
        m_sceneManager = GetComponent<SceneManagerScript>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Time.timeScale = 0f; //Start the game as paused
        m_ended = true; //start as true to avoid clicking through the blocker
    }

    public void StartMiniGame()
	{
        Time.timeScale = 1f;
        m_ended = false;
        m_FTUEMessage.gameObject.SetActive(false);
	}

    public void EndMiniGame(bool victory)
	{
        m_ended = true;
        if (victory)
        {
            m_victoryMessage.gameObject.SetActive(true);
        }
        else
        {
            m_defeatMessage.gameObject.SetActive(true);
        }
        m_player.ResetPlayer();
        foreach (PlagueMinigameGuard guard in m_guards)
        {
            guard.ResetGuard();
        }
    }

    public void ResetMinigame(bool victory)
	{
        m_sceneManager.ChangeScene(SceneManager.GetActiveScene().buildIndex);
	}

    public void RestoreOrientationAndChangeScene(int sceneIndex)
	{
        Screen.orientation = ScreenOrientation.AutoRotation;
        m_sceneManager.ChangeScene(sceneIndex);
	}

    private void ReloadSceneAfterDelay(int delay)
	{
        m_sceneManager.ChangeSceneAfterDelay(SceneManager.GetActiveScene().buildIndex, delay);
	}

    public bool HasGameEnded()
	{
        return m_ended;
	}
}
