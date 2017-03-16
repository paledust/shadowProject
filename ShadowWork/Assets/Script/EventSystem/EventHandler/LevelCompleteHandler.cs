using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteHandler : MonoBehaviour {
	private void LoadNextLevelHandler(Event e)
	{
		CompleteEvent tempEvent = e as CompleteEvent;
		SceneManager.LoadScene(tempEvent.NextLevelIndex);
	}
	private void RestartLevelHandler(Event e)
	{
		// RestartEvent tempEvent = e as RestartEvent;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	private void CameraMoveHandler(Event e)
	{
		CameraMoveEvent tempEvent = e as CameraMoveEvent;
		CameraMoveManager camMoveManager = Camera.main.GetComponent<CameraMoveManager>();
		if(!camMoveManager.ifMove)
		{
			//Debug.Log("Camera Information Sent");
			camMoveManager.ifMove = true;
			camMoveManager.SetMoveInfo(tempEvent.camMoveInfo);
		}
	}
	private void ChangeDirLightHandler(Event e)
	{
		changeDirLightEvent tempEvent = e as changeDirLightEvent;
		DirLightRotationManager dirChangeManager = KeyObjCollect.Instance.ActiveDirLight.transform.GetComponent<DirLightRotationManager>();
		if(!dirChangeManager.ifRotate)
		{
			//Debug.Log("Camera Information Sent");
			dirChangeManager.ifRotate = true;
			dirChangeManager.setRotateInfo(tempEvent.dirRotationInfo);
		}
	}
	private void DirLightSwitchHandler(Event e)
	{
		
	}

	public void RegisterFunction()
	{
		EventManager.Instance.Register<CompleteEvent>(LoadNextLevelHandler);
		EventManager.Instance.Register<RestartEvent>(RestartLevelHandler);
		EventManager.Instance.Register<CameraMoveEvent>(CameraMoveHandler);
		EventManager.Instance.Register<changeDirLightEvent>(ChangeDirLightHandler);
	}

	public void UnRegisterAllFunction()
	{
		EventManager.Instance.UnRegister<CompleteEvent>(LoadNextLevelHandler);
		EventManager.Instance.UnRegister<RestartEvent>(RestartLevelHandler);
		EventManager.Instance.UnRegister<CameraMoveEvent>(CameraMoveHandler);
		EventManager.Instance.UnRegister<changeDirLightEvent>(ChangeDirLightHandler);
	}

}
