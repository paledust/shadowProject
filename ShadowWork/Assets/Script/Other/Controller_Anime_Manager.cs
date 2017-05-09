using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Anime_Manager : MonoBehaviour {
	private SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(1,1,1,0);
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(DoFadeOut);
	}

	public void DoFadeIn(){
		StartCoroutine(FadeIn());
	}
	public void DoFadeOut(Kevin_Event.Event e){
		StartCoroutine(FadeOut());
	}
	IEnumerator FadeIn(){
		GetComponent<Animator>().SetBool("StartAnimation", true);
		yield return null;
	}
	IEnumerator FadeOut(){
		Color originColor = sprite.color;
		GetComponent<Animator>().SetBool("StartAnimation", false);
		for(float timer = 0.0f; timer <= 1.0f; timer += Time.deltaTime){
			sprite.color = Color.Lerp(originColor, new Color(1,1,1,0f), timer);
			yield return null;
		}
	}
}
