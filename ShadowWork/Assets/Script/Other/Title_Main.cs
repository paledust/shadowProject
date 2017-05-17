using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Main : MonoBehaviour {
	[SerializeField] Animator box_animator;
	[SerializeField] Text startText;
	[SerializeField] Image BlackScreenImage;
	private bool LoadLevel = false;
	protected GameObject backGround;
	// Use this for initialization
	void Awake () {
		backGround = Instantiate<GameObject>(Service.prefebList.BackGround);

		if(!AudioManagerScript.Instance)
			Instantiate(Service.prefebList.AudioManager);
		Service.eventManager = new EventManager();
		LoadLevel = false;
	}

	void Start(){
		StartCoroutine(FadeIn(2.0f));
		Service.audioManager.PlayAmbient("StartAmbient", 0.75f, 4f);
	}
	IEnumerator FadeIn(float _waitTime){
		// yield return new WaitForSeconds(_waitTime);
		for(float i = 0.0f; i < 1.0f; i += 0.1f/_waitTime){
			backGround.GetComponentInChildren<UnityEngine.UI.Image>().color = Color.Lerp(Color.black, Color.clear,i);
			yield return null;
		}
		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start") && !LoadLevel){
			LoadLevel = true;
			Service.audioManager.PlaySound2D("StartBox", 0.80f);
			Service.audioManager.PlaySound2D("IntroWiggle", 0.85f);
			//Service.audioManager.PlaySound2D("IntroWhoosh");
			StartCoroutine(LoadLevelTask());

		}
	}

	IEnumerator LoadLevelTask(){
		if(!box_animator.GetBool("Start"))
			box_animator.SetBool("Start",true);
		
		yield return new WaitForSeconds(1.0f);

		for(float i = 0.0f; i <= 2.0f; i += Time.deltaTime){
			startText.color = Color.Lerp(startText.color, new Color(1,1,1,0), i/2.0f);
			BlackScreenImage.color = Color.Lerp(BlackScreenImage.color, Color.black, i/2.0f);
			yield return null;
		}
		
		Service.eventManager.ClearList();
		SceneManager.LoadScene(1);
	}
}
