using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Off_Shadow : MonoBehaviour {
	[SerializeField] Projector project;
	private Color startColor;
	private Color endColor;
	private bool ReadyOff = false;
	private float timer = 0.0f;
	// Use this for initialization
	void Start () {
		ReadyOff = false;
		timer = 0.0f;
		project = GetComponentInChildren<Projector>();
		startColor = project.material.color;
		endColor = new Color(startColor.r,startColor.g,startColor.b,0.0f);
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(End_TurnOff_Shadow);
	}

	void Update(){

		if(ReadyOff){
			timer += Time.deltaTime;
			if(timer >= 0.5f){
				project.material.color = Color.Lerp(startColor, endColor, timer);
			}
		}
	}
	void End_TurnOff_Shadow(Kevin_Event.Event e){
		Kevin_Event.EndGame_Event tempEvent = e as Kevin_Event.EndGame_Event;
		ReadyOff = true;
	}
}
