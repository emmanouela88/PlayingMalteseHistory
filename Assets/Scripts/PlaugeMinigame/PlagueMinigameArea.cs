using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlagueMinigameArea : MonoBehaviour
{
    [SerializeField] private Button m_areatButton;
    [SerializeField] private PlagueMinigamePlayerController m_player;

    void Start()
    {
        if (m_areatButton != null)
		{
            m_areatButton.onClick.AddListener(() => MovePlayerToArea(transform.position));
		}
    }

   
    private void MovePlayerToArea(Vector3 targetPosition)
	{
        if (m_player != null)
		{
            m_player.StartMovingToPosition(targetPosition);
		}
	}
}
