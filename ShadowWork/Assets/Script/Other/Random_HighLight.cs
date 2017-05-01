using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_HighLight : MonoBehaviour {
	private Animator anime;
	private float timer;
	private float TriggerTime;
	// Use this for initialization
	void Start () {
		TriggerTime = 1.0f;
		anime = GetComponent<Animator>();
	}
	void Update(){
		timer += Time.deltaTime;
		if(timer >= TriggerTime){
			anime.SetBool("Play", true);
			timer = 0.0f;
			TriggerTime = Random.Range(5.0f,8.0f);
		}
	}

	public void SetFalse(){
		anime.SetBool("Play", false);
	}
}
