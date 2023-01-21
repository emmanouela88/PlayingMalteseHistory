using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlagueMinigameArea : MonoBehaviour
{
    [SerializeField] private PlagueMinigamePlayerController m_player;
	[SerializeField] private PlagueMinigameManager m_manager;

	private void OnMouseDown()
	{
		if (!m_manager.HasGameEnded())
		{
			MovePlayerToArea(transform.position);
		}
	}

	private void MovePlayerToArea(Vector3 targetPosition)
	{
        if (m_player != null)
		{
            m_player.StartMovingToPosition(targetPosition, this);
		}
	}
}
