using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovePlate : MonoBehaviour
{
    float speed = 0.5F;
    Rigidbody rb;

    public float plateSpeed = 1f;
    public Text m_scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void Update()
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

	void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.LogError(collision.gameObject);


        Destroy(collision.gameObject);

        // TODO Add score depending on which gameobject it is !



        CreateObjects.m_currentScore += 1;
      //  Debug.LogError("SCORE " + CreateObjects.m_currentScore);

        m_scoreText.text = CreateObjects.m_currentScore.ToString();
    }
}
