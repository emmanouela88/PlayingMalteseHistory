using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveScript : MonoBehaviour // Move Coin
{
    [SerializeField] private CreateObjects m_gameManager;

    public float speed = 3f;
    public int m_score = 0; 

    void Update()
    {
        MoveDown();
        if (transform.position.y < -6f)
		{
            Destroy(gameObject);
		}
    }

    void MoveDown()
    {
        gameObject.transform.position -= new Vector3(0f, speed * 0.05f, 0f);
    }
}
