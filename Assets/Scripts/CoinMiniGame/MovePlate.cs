using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovePlate : MonoBehaviour
{
    [SerializeField] private CreateObjects m_gameManager;

    Rigidbody rb;

    public float plateSpeed = 1f;
    public Text m_scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogError(collision.gameObject);


        Destroy(collision.gameObject);

        // TODO Add score depending on which gameobject it is !
        GameObject objectCollected = collision.gameObject;

        if(objectCollected.GetComponent<MoveScript>() != null)
        {
            int value = objectCollected.GetComponent<MoveScript>().m_score;


            // Debug.LogError("SCORE " + CreateObjects.m_currentScore);

            m_gameManager.IncreaseScore(value);
            m_scoreText.text = m_gameManager.GetCurrentScore().ToString();

        }



    }
}
