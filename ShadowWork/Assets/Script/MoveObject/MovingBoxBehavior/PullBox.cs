using UnityEngine;
using CS_Kevin;

public class PullBox : MonoBehaviour {
	public int level_Index;
	private LoadLevelTask loadLevelTask;
	private MoveToTask pullHeroBox;
	private Task_Manager taskManager;
	private bool ifLoad;
	private bool ifPulled;
	void Start(){
		loadLevelTask = new LoadLevelTask(level_Index);
		taskManager = new Task_Manager();
		ifLoad = false;
	}
	void Update(){
		taskManager.Update();
	}
	void OnTriggerStay(Collider m_collider){
		Debug.Log("Collider");
		if(m_collider.gameObject.name == "MovingBox" && m_collider.transform.position == transform.position && !ifLoad){
			Service.audioManager.PlaySound2D("BoxSlide");
			pullHeroBox = new MoveToTask(m_collider.transform, transform.parent.position, 1);
			m_collider.gameObject.GetComponent<MoveObject>().SetStatus(MOVESTATE.PENDING);
			ifLoad = true;
			LoadLevel();
		}
	}

	void LoadLevel(){
		WaitTask startLoadLevel;
		startLoadLevel = new WaitTask(1.0f);
		startLoadLevel.Then(pullHeroBox).
						Then(new WaitTask(2)).
						Then(loadLevelTask);

		taskManager.AddTask(startLoadLevel);
	}
}
