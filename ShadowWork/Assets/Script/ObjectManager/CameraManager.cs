using UnityEngine;
using Kevin_Event;

public class CameraManager : MonoBehaviour {
	private MoveTask moveTask;
	private Animation anime;
	private Task_Manager taskManager = new Task_Manager();
	// Use this for initialization
	void Start () {
		Service.eventManager.Register<Kevin_Event.CameraMoveEvent>(CameraMoveHandler);
		anime = GetComponent<Animation>();
		Cursor.visible = false;
	}
	void Update()
	{
		taskManager.Update();
	}
	public bool ifMoveToEndPos()
	{
		return moveTask.ifFinished;
	}
	private void CameraMoveHandler(Kevin_Event.Event e)
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
	public void ActivateBox(){
		GameObject.Find("MovingBox").GetComponent<MoveObject>().SetStatus(CS_Kevin.MOVESTATE.FROZEN);
		ActivateCameraPan();
	}
	public void ActivateControllerAnimation(){
		foreach(Controller_Anime_Manager anime_manager in GetComponentsInChildren<Controller_Anime_Manager>()){
			anime_manager.DoFadeIn();
		}
		// GetComponentInChildren<Controller_Anime_Manager>().DoFadeIn();
	}
	public void ActivateCameraPan(){
		GetComponent<CameraPan>().enabled = true;
		GetComponent<CameraPan>().SET_OriRotation(transform.rotation);
	}
}
