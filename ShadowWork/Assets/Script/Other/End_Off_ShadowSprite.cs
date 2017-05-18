using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Off_ShadowSprite : MonoBehaviour {
	[SerializeField] float _FadeTime = 1.0f;
	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(StartFade);
	}
	
	// Update is called once per frame
	void StartFade(Kevin_Event.Event e){
		StartCoroutine(FadeOut());
	}
	IEnumerator FadeOut(){
		Color tempColor = sprite.color;

		for(float timer = 0.0f; timer < 1.0f; timer += Time.deltaTime/_FadeTime){
			sprite.color = Color.Lerp(tempColor, Color.clear, timer);
			yield return null;
		}

		yield return null;
	}
}
