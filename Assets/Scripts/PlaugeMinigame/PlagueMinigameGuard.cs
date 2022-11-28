using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlagueMinigameGuard : MonoBehaviour
{
    [SerializeField] private Transform m_point1;
    [SerializeField] private Transform m_point2;
	[SerializeField] private float m_lerpTime;
	[SerializeField] private PlagueMinigameManager m_manager;

    void Start()
    {
		//Guards start moving from pointA to point B.
		StartCoroutine(MoveFromPointToPoint(m_point1.position, m_point2.position));
    }

    private IEnumerator MoveFromPointToPoint(Vector3 pointA, Vector3 pointB)
	{
		Vector3 startPos = pointA;
		Vector3 endPos = pointB;
		float elapsedTime = 0;

		while (elapsedTime < m_lerpTime)
		{
			transform.position = Vector3.Lerp(pointA, pointB, (elapsedTime / m_lerpTime));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.eulerAngles += new Vector3(0, 0, 180);
		//Once the guard reaches the end point, start moving to the other direction
		StartCoroutine(MoveFromPointToPoint(endPos, startPos));
	}

	public void ResetGuard()
	{
		StopAllCoroutines();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("COLLIDEAD!");
		if (collision.tag == "Player")
		{
			m_manager.ResetMinigame();
		}
	}
}
