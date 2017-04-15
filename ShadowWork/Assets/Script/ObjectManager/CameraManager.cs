using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	private MoveTask moveTask;
	private Animation anime;
	private Task_Manager taskManager = new Task_Manager();
	// Use this for initialization
	void Start () {
		Service.eventManager.Register<CameraMoveEvent>(CameraMoveHandler);
		anime = GetComponent<Animation>();
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
	public void CameraAnimationTrigger(){
		anime.Play();
	}
}
