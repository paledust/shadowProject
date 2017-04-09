using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManagerScript : MonoBehaviour {

	public AudioClip ambientA;
	public AudioClip ambientB;

	void Start(){
		AudioManagerScript.instance.PlayAmbient(ambientA,2);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			AudioManagerScript.instance.PlayAmbient (ambientB, 2);
		}
	}
}
