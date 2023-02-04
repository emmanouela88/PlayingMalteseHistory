using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueMinigamePlayerController : MonoBehaviour
{
	[SerializeField] private float m_lerpTime = 4.0f;
	[SerializeField] private PlagueMinigameArea m_startingArea = null;
	[SerializeField] private PlagueMinigameArea m_targetArea = null;
	[SerializeField] private PlagueMinigameManager m_manager;

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
			Vector3 vectorToTarget = targetPosition - transform.position;
			float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

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
				transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);
				elapsedTime += Time.deltaTime;
				if (elapsedTime >= m_lerpTime)
				{
					done = true;
				}
				yield return null;
			}
			m_moving = false;
			m_currentArea = area;

			if (m_currentArea == m_targetArea)
			{
				m_manager.EndMiniGame(true);
			}
		}
	}

	public void ResetPlayer()
	{
		StopAllCoroutines();
		m_moving = false;
	}
   
}
