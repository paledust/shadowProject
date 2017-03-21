using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public GameObject ActiveDirLight;
	// Use this for initialization
	void Start () {
		KeyObjCollect.Instance.SetNewActiveDirLight(ActiveDirLight);
		EventManager.Instance.Register<RestartEvent>(RestartLevelHandler);
		EventManager.Instance.Register<LoadLevelEvent>(LoadNextLevelHandler);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			Fire_RestartLevel_Event();
		}
	}

	private void Fire_RestartLevel_Event()
	{
		RestartEvent e = new RestartEvent();
		EventManager.Instance.FireEvent(e);
	}
	private void RestartLevelHandler(Event e)
	{
		// RestartEvent tempEvent = e as RestartEvent;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	private void LoadNextLevelHandler(Event e)
	{
		LoadLevelEvent tempEvent = e as LoadLevelEvent;
		SceneManager.LoadScene(tempEvent.NextLevelIndex);
	}
}
