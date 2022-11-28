using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueMinigamePlayerController : MonoBehaviour
{
	[SerializeField] private float m_lerpTime = 4.0f;

	private bool m_moving = false;
	private Vector3 m_startingPosition;

	private void Start()
	{
		m_startingPosition = transform.position;
	}

	public void StartMovingToPosition(Vector3 targetPosition)
	{
		StartCoroutine(MoveToTargetPosition(targetPosition));
	}

    public IEnumerator MoveToTargetPosition(Vector3 targetPosition)
	{
		if (!m_moving)
		{
			m_moving = true;
			Vector3 startingPosition = transform.position;

			float elapsedTime = 0;

			while (elapsedTime < m_lerpTime)
			{
				transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / m_lerpTime));
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			m_moving = false;
		}
	}

	public void ResetPlayer()
	{
		StopAllCoroutines();
		m_moving = false;
	}
   
}
