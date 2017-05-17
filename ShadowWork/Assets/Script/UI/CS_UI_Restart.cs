using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_UI_Restart : MonoBehaviour {
	Text _text;
	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
		_text.color = new Color(1,1,1,0);
	}

	public void StartFade(){
		StartCoroutine(FadeIn());
	}
	public void StartFadeOut(){
		StartCoroutine(FadeOut());
	}
	IEnumerator FadeIn(){
		Color tempColor = _text.color;
		for(float timer = 0.0f; timer < 1.0f; timer += Time.deltaTime){
			_text.color = Color.Lerp(tempColor, new Color(1,1,1,1), timer);
			yield return null;
		}
	}
	IEnumerator FadeOut(){
		Color tempColor = _text.color;
		for(float timer = 1.0f; timer > 0.0f; timer -= Time.deltaTime){
			_text.color = Color.Lerp(tempColor, new Color(1,1,1,0), timer);
			yield return null;
		}		
	}
}
