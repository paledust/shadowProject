using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_High_Light : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(TurnOffHighLit);
	}
	void TurnOffHighLit(Kevin_Event.Event e){
		gameObject.SetActive(false);
	}
}
