using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Off_Particle_Miasma : MonoBehaviour {
	private ParticleSystem _Particle;	
	// Use this for initialization
	void Start () {
		_Particle = GetComponent<ParticleSystem>();
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(End_TurnOff_Particle);
	}
	
	void End_TurnOff_Particle(Kevin_Event.Event e){
		StartCoroutine(End_Particle());
	}
	IEnumerator End_Particle(){
		Color startColor = GetComponent<ParticleSystemRenderer>().material.GetColor("_TintColor");
		for(float timer = 0.0f; timer< 1.0f; timer += Time.deltaTime){
			GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor",Color.Lerp(startColor, new Color(0,0,0,0), timer));
			yield return null;
		}
	}
}
