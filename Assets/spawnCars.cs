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
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
		
			Vector3 carPos = new Vector3(Random.Range (-maxPos, maxPos), parentRect.transform.position.y, transform.position.z);
			int objectToSpawn = Random.Range (0, car.Length);

			GameObject spawnedObject = Instantiate (car[objectToSpawn],new Vector3(carPos.x,parentRect.transform.position.y,0), Quaternion.identity);

			// make the parent the spawner so hierarchy doesn't get super messy
			spawnedObject.transform.parent = parentRect.transform;
		
			timer = delay;
			Debug.Log("Position: "+carPos +" maxPos: "+maxPos);
		}
	}
}
