using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class spawnCars : MonoBehaviour {

	public GameObject[] car;
	public float maxPos;
	public float delay;
	float timer;
	public Transform parentRect;
	public ShowHide showHideComponent;
	private float timeLimitStart;
	private float timeLimitRemain;

	public GameObject pnlWinLose;
	public TMP_Text infoTxt;
	public TMP_Text timerTxt;

	public Button btnStart;
	bool isStarted = false;
	// Use this for initialization
	void Start () {
		timer = delay;
		
		infoTxt.text = "Blow as many torches as you can without the father seeing you!";
		pnlWinLose.SetActive(true);
		timeLimitStart = 20.0f;
		btnStart.gameObject.SetActive(true);
		timeLimitRemain = timeLimitStart;
		//StartGame();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer -= Time.deltaTime;
		timeLimitRemain-=Time.deltaTime;

		timerTxt.text = "Time: " + $"{(int)timeLimitStart / 60}:{(int)timeLimitRemain % 60:D2}"; ;

        if (isStarted)
        {
			StartGame();
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

				if (timeLimitRemain == 0)
				{
					foreach (var item in car)
					{
						Time.timeScale = 0;
						pnlWinLose.SetActive(true);
						infoTxt.text = "Time is up! You manage to blow " + item + " torche(s)";
					}
				}
			}
		}
		
	}

	void StartGame()
    {
		btnStart.gameObject.SetActive(false);
		pnlWinLose.SetActive(false);
		isStarted = true;
		Time.timeScale = 1;
	}

}
