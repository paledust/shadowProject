using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Off_Particle : MonoBehaviour {
	private ParticleSystem _Particle;	
	// Use this for initialization
	void Start () {
		_Particle = GetComponent<ParticleSystem>();
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(End_TurnOff_Particle);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void End_TurnOff_Particle(Kevin_Event.Event e){
		Kevin_Event.EndGame_Event tempEvent = e as Kevin_Event.EndGame_Event;
	}
}
