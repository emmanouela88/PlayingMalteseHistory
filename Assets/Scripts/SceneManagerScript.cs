using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        // Check if the name of the current Active Scene is your first Scene.
        if (scene.name == "fireLastScene")
        {
            StartCoroutine(WaitUntilChangeScene(48,3));
        }

        // Check if the name of the current Active Scene is your first Scene.
        if (scene.name == "MainMenuWithIntro")
        {
            StartCoroutine(WaitUntilChangeScene(40,5));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string sceneName)
    {
        PreviousSceneSaver.instance.m_prevScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneAfterDelay(int sceneNum, int delay)
	{
        StartCoroutine(WaitUntilChangeScene(delay, sceneNum));
	}

    public void ChangeToPreviousScene()
	{
        if (PreviousSceneSaver.instance != null)
        {
            SceneManager.LoadScene(PreviousSceneSaver.instance.m_prevScene);
        }
		else
		{
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator WaitUntilChangeScene(int seconds, int sceneNum)
    {
        yield return new WaitForSeconds(seconds);

        ChangeScene(SceneManager.GetSceneByBuildIndex(sceneNum).name);
    }

}
