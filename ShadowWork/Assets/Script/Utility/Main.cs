﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject root{get {return gameObject;}}
	[SerializeField] float waitTime = 1.0f;
	// Use this for initialization
	void Awake () {
		Service.eventManager = new EventManager();	
		Service.SetNewActiveDirLight(GameObject.Find("Directional Light"));
	}
	void Start(){
		Service.eventManager.Register<RestartEvent>(RestartLevelHandler);
		Service.eventManager.Register<LoadLevelEvent>(LoadNextLevelHandler);
		StartCoroutine(WaitToChangeCamera(waitTime));
		// Service.audioManager.PlayAmbient("Ambient");
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.P))
		{
			Fire_RestartLevel_Event();
		}
	}
	private void Fire_RestartLevel_Event(){
		RestartEvent e = new RestartEvent();
		Service.eventManager.FireEvent(e);
	}
	private void RestartLevelHandler(Event e){
		// RestartEvent tempEvent = e as RestartEvent;
		Service.eventManager.ClearList();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	private void LoadNextLevelHandler(Event e){
		Service.eventManager.ClearList();
		LoadLevelEvent tempEvent = e as LoadLevelEvent;
		SceneManager.LoadScene(tempEvent.NextLevelIndex);
	}
	IEnumerator WaitToChangeCamera(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Camera.main.GetComponent<CameraManager>().CameraAnimationTrigger();
		yield return null;
	}
}
