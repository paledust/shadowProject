using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Kevin_Event;

public class Main : MonoBehaviour {
	[SerializeField] float waitTime = 0.5f;
	[SerializeField] AnimationCurve lights_Off_Curve;
	[SerializeField] protected float Camera_WaitTime = 0.0f;
	protected bool ReadyOff = false;
	protected float timer = 0.0f;
	protected GameObject backGround;
	// Use this for initialization
	protected void Awake () {
		Service.eventManager = new EventManager();
		backGround = Instantiate<GameObject>(Service.prefebList.BackGround);
		ReadyOff = false;
		Service.SetNewActiveDirLight(GameObject.Find("Directional Light"));
		Service.eventManager.Register<EndGame_Event>(EndOff_All_Light);

		if(!AudioManagerScript.Instance)
			Instantiate(Service.prefebList.AudioManager);
		GameObject wallManager = Instantiate<GameObject>(Service.prefebList.WallManager);
		wallManager.name = "WallManager";
	}
	protected void Start(){
		Service.eventManager.Register<RestartEvent>(RestartLevelHandler);
		Service.eventManager.Register<LoadLevelEvent>(LoadNextLevelHandler);
		StartCoroutine(WaitToChangeCamera(waitTime));

		Service.audioManager.PlayAmbient("Ambient", 1f, 1f);
	}
	void Update(){
		if(Input.GetButtonDown("Reset"))
		{
			Fire_RestartLevel_Event();
		}
		if(ReadyOff){
			timer += Time.deltaTime;
			Service.ActiveDirLight.GetComponent<Light>().intensity = lights_Off_Curve.Evaluate(timer);
		}
		if(Input.GetButtonDown("Next_Level")){
			Service.eventManager.ClearList();
			SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + (int)Input.GetAxis("Next_Level")));
		}

	}
	protected void Fire_RestartLevel_Event(){
		RestartEvent e = new RestartEvent();
		Service.eventManager.FireEvent(e);
	}
	protected void RestartLevelHandler(Kevin_Event.Event e){
		// RestartEvent tempEvent = e as RestartEvent;
		Service.eventManager.ClearList();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	protected void LoadNextLevelHandler(Kevin_Event.Event e){
		Service.eventManager.ClearList();
		LoadLevelEvent tempEvent = e as LoadLevelEvent;
		SceneManager.LoadScene(tempEvent.NextLevelIndex);
	}
	protected void EndOff_All_Light(Kevin_Event.Event e){
		StartCoroutine(WaitToTurnOffLight(0.0f));
	}
	IEnumerator WaitToChangeCamera(float _waitTime){
		// yield return new WaitForSeconds(_waitTime);
		for(float i = 0.0f; i < 1.0f; i += 0.1f/_waitTime){
			backGround.GetComponentInChildren<UnityEngine.UI.Image>().color = Color.Lerp(Color.black, Color.clear,i);
			yield return null;
		}

		yield return new WaitForSeconds(Camera_WaitTime);

		if(Camera.main.GetComponent<CameraManager>())
			Camera.main.GetComponent<CameraManager>().CameraAnimationTrigger();
		yield return null;
	}
	IEnumerator WaitToTurnOffLight(float _waitTime){
		yield return new WaitForSeconds(_waitTime);
		ReadyOff = true;

		yield return null;
	}
}
