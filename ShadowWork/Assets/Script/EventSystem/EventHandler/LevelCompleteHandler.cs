using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteHandler : MonoBehaviour {
	public int NextLevelIndex;

	private void LoadNextLevel(Event e)
	{
		CompleteEvent tempEvent = e as CompleteEvent;
		SceneManager.LoadScene(NextLevelIndex);
	}
	private void RestartLevel(Event e)
	{
		RestartEvent tempEvent = e as RestartEvent;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void RegisterFunction()
	{
		EventManager.Instance.Register<CompleteEvent>(LoadNextLevel);
		EventManager.Instance.Register<RestartEvent>(RestartLevel);
	}

}
