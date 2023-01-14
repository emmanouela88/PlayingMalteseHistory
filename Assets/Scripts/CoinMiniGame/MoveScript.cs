using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveScript : MonoBehaviour
{

    public float speed = 3f;
    public GameObject m_scoreTextGameObject;

    public Text m_scoreText;


    // Start is called before the first frame update
    void Start()
    {
        m_scoreTextGameObject = GameObject.Find("TextScore"); // FIX THIS?
        m_scoreText = m_scoreTextGameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
    }

    void MoveDown()
    {
        if (CreateObjects.m_playing)
        {
            gameObject.transform.position -= new Vector3(0f, speed * 0.05f, 0f);
        }
    }

    void OnMouseUpAsButton() 
    {
        if (CreateObjects.m_playing)
        {
            IncreaseScore();
            Destroy(gameObject);
        }
    }


    public void IncreaseScore()
    {
        CreateObjects.m_currentScore += 1;
        //Debug.LogError("SCORE " + CreateObjects.m_currentScore);
        m_scoreText.text = CreateObjects.m_currentScore.ToString();
    }

}
