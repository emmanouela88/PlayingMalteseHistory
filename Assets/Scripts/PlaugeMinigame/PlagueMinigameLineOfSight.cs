using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueMinigameLineOfSight : MonoBehaviour
{
    [SerializeField] private PlagueMinigameManager m_manager;

	private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLIDEAD!");
        if (collision.tag == "Player")
        {
            m_manager.EndMiniGame(false);
        }
    }
}
