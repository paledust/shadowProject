using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	private MoveTask moveTask;
	private Task_Manager taskManager = new Task_Manager();
	// Use this for initialization
	void Start () {
		EventManager.Instance.Register<CameraMoveEvent>(CameraMoveHandler);
	}
	void Update()
	{
		taskManager.Update();
	}
	public bool ifMoveToEndPos()
	{
		return moveTask.ifFinished;
	}
	private void CameraMoveHandler(Event e)
	{
		if(moveTask != null)
		{
			moveTask.SetStatus(Task.TaskStatus.Aborted);
		}
		CameraMoveEvent tempEvent = e as CameraMoveEvent;
		moveTask = new MoveTask(tempEvent.camMoveInfo, Camera.main.gameObject);

		taskManager.AddTask(moveTask);
	}
}
