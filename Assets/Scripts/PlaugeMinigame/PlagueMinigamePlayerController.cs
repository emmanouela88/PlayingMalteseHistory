using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueMinigamePlayerController : MonoBehaviour
{
	[SerializeField] private float m_lerpTime = 4.0f;
	[SerializeField] private PlagueMinigameArea m_startingArea = null;

	private bool m_moving = false;
	private Vector3 m_startingPosition;
	private PlagueMinigameArea m_currentArea;

	private void Start()
	{
		m_startingPosition = transform.position;
		m_currentArea = m_startingArea;
	}

	public void StartMovingToPosition(Vector3 targetPosition, PlagueMinigameArea area)
	{
		StartCoroutine(MoveToTargetPosition(targetPosition, area));
	}

    public IEnumerator MoveToTargetPosition(Vector3 targetPosition, PlagueMinigameArea area)
	{
		if (!m_moving)
		{
			m_moving = true;
			bool done = false;
			Vector3 startingPosition = transform.position;

			float elapsedTime = 0;

			if (m_currentArea == area)
			{
				done = true;
			}

			while (!done)
			{
				transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / m_lerpTime));
				elapsedTime += Time.deltaTime;
				if (elapsedTime >= m_lerpTime)
				{
					done = true;
				}
				yield return null;
			}
			m_moving = false;
			m_currentArea = area;
		}
	}

	public void ResetPlayer()
	{
		StopAllCoroutines();
		m_moving = false;
	}
   
}
