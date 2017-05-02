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
	void Start () {
		LoadLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			LoadLevel = true;
		}

		if(LoadLevel){
			if(!box_animator.GetBool("Start"))
				box_animator.SetBool("Start",true);
			startText.color = Color.Lerp(startText.color, new Color(1,1,1,0), Time.deltaTime*1.5f);
			BlackScreenImage.color = Color.Lerp(BlackScreenImage.color, Color.black, Time.deltaTime*1.5f);
			TitleImage.color = Color.Lerp(TitleImage.color,  new Color(1,1,1,0), Time.deltaTime*1.5f);

			if(TitleImage.color.a <= 0.05f)
				SceneManager.LoadScene(0);
		}
	}
}
