using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlagueMinigameManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_defeatMessage;
    [SerializeField] private PlagueMinigamePlayerController m_player;
    [SerializeField] private List<PlagueMinigameGuard> m_guards;

    private SceneManagerScript m_sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        m_sceneManager = GetComponent<SceneManagerScript>();
    }

    public void ResetMinigame()
	{
        m_defeatMessage.gameObject.SetActive(true);
        m_player.ResetPlayer();
        foreach(PlagueMinigameGuard guard in m_guards)
		{
            guard.ResetGuard();
		}
        ReloadSceneAfterDelay(3);
	}

    private void ReloadSceneAfterDelay(int delay)
	{
        m_sceneManager.ChangeSceneAfterDelay(SceneManager.GetActiveScene().buildIndex, delay);
	}
}
