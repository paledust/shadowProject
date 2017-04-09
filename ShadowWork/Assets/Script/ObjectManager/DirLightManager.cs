using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLightManager : MonoBehaviour {
	private RotateTask rotateTask;
	private Task_Manager taskManager = new Task_Manager();
	
	// Update is called once per frame
	void Update () {
		taskManager.Update();
	}
	public bool ifRotateToEnd()
	{
		return rotateTask.ifFinished;
	}
	private void changeDirLightHandler(Event e)
	{
		if(rotateTask != null)
		{
			rotateTask.SetStatus(Task.TaskStatus.Aborted);
		}
		changeDirLightEvent tempEvent = e as changeDirLightEvent;
		rotateTask = new RotateTask(tempEvent.dirRotationInfo, gameObject);

		taskManager.AddTask(rotateTask);
	}

	public void registerFunction()
	{
		Service.eventManager.Register<changeDirLightEvent>(changeDirLightHandler);
	}
	public void UnregisterFunction()
	{
		Service.eventManager.UnRegister<changeDirLightEvent>(changeDirLightHandler);
	}
}
