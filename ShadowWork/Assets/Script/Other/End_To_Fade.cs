using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_To_Fade : MonoBehaviour {
	private float timer = 0.0f;
	private bool IfFade = false;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
		IfFade = false;
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(End_Fade);
	}
	void Update(){
		if(IfFade){
			timer += Time.deltaTime;
			float _alpha = Mathf.Lerp(1,0,(timer*1.5f));
			GetComponent<MeshRenderer>().material.SetFloat("_Alpha",_alpha);
		}
	}

	void End_Fade(Kevin_Event.Event e){
		IfFade = true;
	}

}
