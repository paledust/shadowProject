using UnityEngine;
using System.Collections;
using CS_Kevin;
using UnityEngine.SceneManagement;
using Kevin_Event;

public class PullBox : MonoBehaviour {
	public int level_Index;
	private LoadLevelTask loadLevelTask;
	private MoveToTask pullHeroBox;
	private Task_Manager taskManager;
	private bool ifLoad;
	private bool ifPulled;
	private bool ifEnd = false;
	void Start(){
		if(SceneManager.GetActiveScene().buildIndex < 13){
			loadLevelTask = new LoadLevelTask(SceneManager.GetActiveScene().buildIndex + 1);
			level_Index = SceneManager.GetActiveScene().buildIndex + 1;
		}
		else{
			loadLevelTask = new LoadLevelTask(0);
			level_Index = 0;
		}

		taskManager = new Task_Manager();
		ifLoad = false;
	}
	void Update(){
		taskManager.Update();
		if(GameObject.Find("MovingBox").transform.position == transform.parent.position && !ifEnd){
			ifEnd = true;
			
			EndGame_Event tempEvent = new EndGame_Event();
			Service.eventManager.FireEvent(tempEvent);
		}
	}
	void OnTriggerEnter(Collider m_collider){
		if(m_collider.gameObject.name == "MovingBox" && m_collider.transform.position == transform.position && !ifLoad){
			pullHeroBox = new MoveToTask(m_collider.transform, transform.parent.position, 1);
			m_collider.gameObject.GetComponent<MoveObject>().SetStatus(MOVESTATE.PENDING);
			ifLoad = true;
			StartCoroutine(LoadLevel(level_Index));
			// LoadLevel();
		}
	}
	void OnTriggerStay(Collider m_collider){
		if(m_collider.gameObject.name == "MovingBox" && m_collider.transform.position == transform.position && !ifLoad){
			pullHeroBox = new MoveToTask(m_collider.transform, transform.parent.position, 1);
			m_collider.gameObject.GetComponent<MoveObject>().SetStatus(MOVESTATE.PENDING);
			ifLoad = true;
			StartCoroutine(LoadLevel(level_Index));
			// LoadLevel();
		}
	}
	IEnumerator LoadLevel(int _LevelIndex){
		yield return new WaitForSeconds(1f);

		taskManager.AddTask(pullHeroBox);
		Service.audioManager.PlaySound2D("BoxSlide", 1.0f);

		yield return new WaitForSeconds(3.0f);

		LoadLevelEvent tempEvent = new LoadLevelEvent();
		tempEvent.NextLevelIndex = _LevelIndex;
		Service.eventManager.FireEvent(tempEvent);
		yield return null;;
	}
	void LoadLevel(){
		WaitTask startLoadLevel;
		startLoadLevel = new WaitTask(1.0f);
		startLoadLevel.Then(pullHeroBox).
						Then(new WaitTask(3.0f)).
						Then(loadLevelTask);

		taskManager.AddTask(startLoadLevel);
	}
}
