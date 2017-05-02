using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Bar_Lit : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(LitUp);
	}
	void LitUp(Kevin_Event.Event e){
		GetComponent<Animator>().SetBool("End", true);
	}
}
