using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteHandler : MonoBehaviour {
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
		swithDirLightEvent tempEvent = e as swithDirLightEvent;
		DirLightSwitchManager switchManager_Start = tempEvent.light_Start.GetComponent<DirLightSwitchManager>();
		DirLightSwitchManager switchManager_End = tempEvent.light_End.GetComponent<DirLightSwitchManager>();
		
		if(!switchManager_Start.ifSwitch)
		{
			Debug.Log("StartLight Set");
			switchManager_Start.ifSwitch = true;
			switchManager_Start.SetIntensity(0.0f);
		}
		if(!switchManager_End.ifSwitch)
		{
			Debug.Log("EndLight Set");
			switchManager_End.ifSwitch = true;
			switchManager_End.SetIntensity(1.0f);
		}
	}

	public void RegisterFunction()
	{
		EventManager.Instance.Register<CameraMoveEvent>(CameraMoveHandler);
		EventManager.Instance.Register<changeDirLightEvent>(ChangeDirLightHandler);
		EventManager.Instance.Register<swithDirLightEvent>(DirLightSwitchHandler);
	}

	public void UnRegisterAllFunction()
	{
		EventManager.Instance.UnRegister<CameraMoveEvent>(CameraMoveHandler);
		EventManager.Instance.UnRegister<changeDirLightEvent>(ChangeDirLightHandler);
		EventManager.Instance.UnRegister<swithDirLightEvent>(DirLightSwitchHandler);
	}

}
