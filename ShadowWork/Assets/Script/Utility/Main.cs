using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
	public GameObject root{get {return gameObject;}}
	[SerializeField] AudioLibraryScript audioLibrary;

	// Use this for initialization
	void Awake () {
		Service.eventManager = new EventManager();	
		Service.SetNewActiveDirLight(GameObject.Find("Directional Light"));
		audioLibrary.InitialAudioLibrary();

		if(!GetComponent<AudioManagerScript>())
			Service.audioManager = gameObject.AddComponent<AudioManagerScript>();
		else
			Service.audioManager = GetComponent<AudioManagerScript>();

		Service.audioManager.InitialAudio(audioLibrary);
	}
	void Start(){
		Service.eventManager.Register<RestartEvent>(RestartLevelHandler);
		Service.eventManager.Register<LoadLevelEvent>(LoadNextLevelHandler);

		Service.audioManager.PlayAmbient("Ambient");
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
}
