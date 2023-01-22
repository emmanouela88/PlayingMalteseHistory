using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovePlate : MonoBehaviour
{
    Rigidbody rb;

    public float plateSpeed = 1f;
    public Text m_scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void Update()
	{
        if (CreateObjects.m_playing)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (gameObject.transform.position.x > -1.9f) gameObject.transform.position -= new Vector3(plateSpeed * 0.1f, 0f, 0f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (gameObject.transform.position.x < 1.9f) gameObject.transform.position += new Vector3(plateSpeed * 0.1f, 0f, 0f);
            }
        }
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

            CreateObjects.m_currentScore += value;
            m_scoreText.text = CreateObjects.m_currentScore.ToString();

        }



    }
}
