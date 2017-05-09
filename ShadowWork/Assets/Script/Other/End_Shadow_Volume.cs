using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Shadow_Volume : MonoBehaviour {
	private Renderer render;
	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer>();
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(Do_End_Fade);
	}

	void Do_End_Fade(Kevin_Event.Event e){
		StartCoroutine(End_Fade());
	}
	IEnumerator End_Fade(){
		float StartHeight = render.material.GetFloat("_shadowheight");
		for(float timer = 0.0f; timer < 1.0f; timer += Time.deltaTime){
			render.material.SetFloat("_shadowheight", Mathf.Lerp(StartHeight, 10.0f, timer));
			yield return null;
		}
	}
}
