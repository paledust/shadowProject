using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public GameObject ActiveDirLight;
	// Use this for initialization
	void Awake () {
		KeyObjCollect.Instance.SetNewActiveDirLight(ActiveDirLight);
		// EventManager.Instance.Register<RestartEvent>(RestartLevelHandler);
		// EventManager.Instance.Register<LoadLevelEvent>(LoadNextLevelHandler);
	}
	void Start(){
		EventManager.Instance.Register<RestartEvent>(RestartLevelHandler);
		EventManager.Instance.Register<LoadLevelEvent>(LoadNextLevelHandler);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
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
		EventManager.Instance.ClearList();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	private void LoadNextLevelHandler(Event e)
	{
		EventManager.Instance.ClearList();
		LoadLevelEvent tempEvent = e as LoadLevelEvent;
		SceneManager.LoadScene(tempEvent.NextLevelIndex);
	}
}
