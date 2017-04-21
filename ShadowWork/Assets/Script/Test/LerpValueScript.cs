using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpValueScript : MonoBehaviour {

	public GameObject dirLight1;
	public float min1;
	public float max1;
	public GameObject dirLight2;
	public float min2;
	public float max2;

	float t = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dirLight1.GetComponent<Light>().intensity = Mathf.Lerp(min1, max1, t);
		dirLight2.GetComponent<Light>().intensity = Mathf.Lerp(max2, min2, t);
		t += 0.5f * Time.deltaTime;
			
	}
		
}
