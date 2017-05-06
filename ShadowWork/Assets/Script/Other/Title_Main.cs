using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Main : MonoBehaviour {
	[SerializeField] Animator box_animator;
	[SerializeField] Text startText;
	[SerializeField] Image TitleImage;
	[SerializeField] Image BlackScreenImage;
	private bool LoadLevel = false;
	// Use this for initialization
	void Awake () {
		Service.eventManager = new EventManager();
		LoadLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start") && !LoadLevel){
			LoadLevel = true;
			StartCoroutine(LoadLevelTask());
		}
	}

	IEnumerator LoadLevelTask(){
		if(!box_animator.GetBool("Start"))
			box_animator.SetBool("Start",true);
		
		yield return new WaitForSeconds(1.0f);

		for(float i = 0.0f; i <= 3.0f; i += Time.deltaTime){
			startText.color = Color.Lerp(startText.color, new Color(1,1,1,0), i/2.0f);
			BlackScreenImage.color = Color.Lerp(BlackScreenImage.color, Color.black, i/2.0f);
			TitleImage.color = Color.Lerp(TitleImage.color,  new Color(1,1,1,0), i/2.0f);
			yield return null;
		}
		
		Service.eventManager.ClearList();
		SceneManager.LoadScene(1);
	}
}
