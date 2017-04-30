using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_To_Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(End_Destroy);
	}
	
	void End_Destroy(Kevin_Event.Event e){
		StartCoroutine(KillObject());
	}
	IEnumerator KillObject(){
		yield return new WaitForSeconds(1.0f);
		Destroy(gameObject);
	}
}
