using UnityEngine;
using System.Collections;

public class spawnCars : MonoBehaviour {

	public GameObject[] car;
	public float maxPos;
	public float delay;
	float timer;
	public Transform parentRect;

	// Use this for initialization
	void Start () {
		timer = delay;		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer -= Time.deltaTime;

		if (timer <= 0) {

			Vector3 carPos = new Vector3(Random.Range(-maxPos, maxPos), transform.position.y, transform.position.z);
			int objectToSpawn = Random.Range(0, car.Length);

			GameObject spawnedObject = Instantiate(car[objectToSpawn], carPos, transform.rotation) as GameObject;

			// make the parent the spawner so hierarchy doesn't get super messy
			spawnedObject.transform.parent = gameObject.transform;

			timer = delay;
			Debug.Log("Position: "+car[objectToSpawn].transform.position.x +" maxPos: "+maxPos);
		}
	}
}
