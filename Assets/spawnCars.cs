using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spawnCars : MonoBehaviour {

	[SerializeField] private RectTransform m_victoryMessage;
	[SerializeField] private RectTransform m_defeatMessage;
	[SerializeField] private RectTransform m_FTUEMessage;

	public GameObject[] car;
	public float maxPos;
	public float delay;
	float timer;
	public Transform parentRect;
	public ShowHide showHideComponent;
	private float timeLimitStart;
	private float timeLimitRemain;

	public TMP_Text infoTxt;
	public TMP_Text timerTxt;

	bool isStarted = false;

	// Use this for initialization
	void Start () 
	{
		m_victoryMessage.gameObject.SetActive(false);
		m_defeatMessage.gameObject.SetActive(false);
		m_FTUEMessage.gameObject.SetActive(true);

		Time.timeScale = 0f;
		timer = delay;
		
		infoTxt.text = "Blow as many torches as you can without the father seeing you!";
		timeLimitStart = 20.0f;
		timeLimitRemain = timeLimitStart;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer -= Time.deltaTime;
		timeLimitRemain-=Time.deltaTime;

		timerTxt.text = "Time: " + $"{(int)timeLimitStart / 60}:{(int)timeLimitRemain % 60:D2}"; ;

        if (isStarted)
        {
			if (timer <= 0)
			{
				Vector3 carPos = new Vector3(0, Random.Range(-maxPos, maxPos), transform.position.z);
				int objectToSpawn = Random.Range(0, car.Length);

				GameObject spawnedObject = Instantiate(car[objectToSpawn], carPos, Quaternion.identity) as GameObject;

				// make the parent the spawner so hierarchy doesn't get super messy
				//spawnedObject.transform.parent = gameObject.transform;
				spawnedObject.transform.SetParent(parentRect, false);
				timer = delay;
				Debug.Log("Position: " + car[objectToSpawn].transform.position.x + " maxPos: " + maxPos);

				if (timeLimitRemain <= 0)
				{
					GameOver(true);
				}
			}
		}
		
	}

	public void StartGame()
    {
		m_FTUEMessage.gameObject.SetActive(false);
		isStarted = true;
		Time.timeScale = 1;
	}

	public void GameOver(bool victory)
	{
		Time.timeScale = 0f;
		isStarted = false;
		// Show the message.
		if (!victory)
		{
			m_defeatMessage.gameObject.SetActive(true);
		}
		else
		{
			//TODO: Show the actual score here
			infoTxt.text = "Time is up! You managed to blow " + 10 + " torche(s)";
			m_victoryMessage.gameObject.SetActive(true);
		}
	}

	public void ResetMinigame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
