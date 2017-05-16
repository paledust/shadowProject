using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InviWall_Particle : MonoBehaviour {
	public bool IF_ON{get; private set;}
	[SerializeField] float MaxAlph = 1.0f;
	[SerializeField] float FadeInSpeed = 3.0f;
	[SerializeField] float FadeOutSpeed = 0.5f;
	private SpriteRenderer sprite;
	private IEnumerator Fade;
	void Start(){
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = Service.invis_Color;
		Fade = FadeIn();
		IF_ON = false;
	}
	public void SHOW_UP(){
		StopCoroutine(Fade);
		Fade = FadeIn();
		StartCoroutine(Fade);
	}
	public void Clear_And_Show(){
		sprite.color = Service.invis_Color;
		StopCoroutine(Fade);
		Fade = FadeIn();
		StartCoroutine(Fade);
	}

	IEnumerator FadeIn(){
		ON_FADE();
		for(float alpha = sprite.color.a; alpha < MaxAlph; alpha += Easing.BackEaseOutIn(Time.deltaTime * FadeInSpeed)){
			sprite.color = new Color(1,1,1,alpha);
			yield return null;
		}
		yield return new WaitForSeconds(0.3f);
		for(float alpha = sprite.color.a; alpha > 0.0f; alpha -= Time.deltaTime * FadeOutSpeed){
			sprite.color = new Color(1,1,1,alpha);
			yield return null;
		}
		EXIT_FADE();
	}

	private void ON_FADE(){
		IF_ON = true;
	}
	private void EXIT_FADE(){
		IF_ON = false;
	}
}
