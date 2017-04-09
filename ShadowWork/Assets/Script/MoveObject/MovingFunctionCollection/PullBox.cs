using UnityEngine;
using CS_Kevin;

public class PullBox : MonoBehaviour {
	public int level_Index;
	private LoadLevelTask loadLevelTask;
	private WaitTask wait;
	private Task_Manager taskManager;
	private bool ifLoad;
	void Start(){
		loadLevelTask = new LoadLevelTask(level_Index);
		wait = new WaitTask(3);
		taskManager = new Task_Manager();
		ifLoad = false;
	}
	void Update(){
		taskManager.Update();
	}
	void OnTriggerStay(Collider m_collider){
		Debug.Log("Collider");
		if(m_collider.gameObject.name == "MovingBox" && m_collider.transform.position == transform.position){
			m_collider.gameObject.GetComponent<MoveObject>().SetStatus(MOVESTATE.PULLING);
			m_collider.gameObject.GetComponent<MoveObject>().moveTo(transform.parent.position, 1);
			if(!ifLoad){
				ifLoad = true;
				LoadLevel();
			}
		}
	}

	void LoadLevel(){
		Debug.Log("Go");
		wait.Then(loadLevelTask);
		taskManager.AddTask(wait);
	}
}
