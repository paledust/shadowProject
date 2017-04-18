using UnityEngine;

public class AnimeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float timeValue;
		timeValue = Mathf.Ceil(Time.time % 16);
		GetComponent<Renderer>().material.SetFloat("_TimeValue", timeValue);
	}
}
