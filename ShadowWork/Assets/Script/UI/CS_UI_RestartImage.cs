using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_UI_RestartImage : MonoBehaviour {
	SpriteRenderer sprite;
	Animator anime;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		anime = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start")){
			anime.SetBool("Start", true);
			StopAllCoroutines();
			StartCoroutine(StartAnime());
		}
		if(Input.GetButtonUp("Start")){
			anime.SetBool("Start", false);
			anime.Play("New State");
			StopAllCoroutines();
			StartCoroutine(FadeOutAnime());
		}
	}

	IEnumerator StartAnime(){
		Debug.Log("Hellow");
		Color tempColor = sprite.color;

		for(float timer = 0.0f; timer < 1f; timer += Time.deltaTime * 3){
			sprite.color = Color.Lerp(tempColor, Color.white,timer);
			yield return null;
		}

		yield return null;
	}

	IEnumerator FadeOutAnime(){
		Color tempColor = sprite.color;

		for(float timer = 0.0f; timer < 1f; timer += Time.deltaTime * 3){
			sprite.color = Color.Lerp(tempColor, new Color(1,1,1,0), timer);
			yield return null;
		}

		anime.SetBool("Start", false);
		yield return null;
	}
}
