using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIButtonLevelLoad : MonoBehaviour {
	
	public void LoadLevel(string nameScene) {
        SceneManager.LoadScene(nameScene);
	}
}
