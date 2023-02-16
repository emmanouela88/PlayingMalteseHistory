using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour // Used to move the plate on device
{
	private Vector3 m_dragOffset;
	private Camera m_camera;
	[SerializeField] private float m_speed;
	[SerializeField] private CreateObjects m_gameManager;

	private void OnMouseDown()
	{
		m_dragOffset = transform.position - GetMousePos();
	}
	private void OnMouseDrag()
	{
		if (m_gameManager.IsPlaying())
		{
			transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + m_dragOffset, m_speed * Time.deltaTime);
		}
	}

	private Vector3 GetMousePos()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		mousePos.y = 0;
		return mousePos;
	}
}
