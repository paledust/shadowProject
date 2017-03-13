using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour {
	public List<ShadowTrail> shadowTrailList;
	[SerializeField] Transform DirectionalLight;

	RaycastHit[] rayHits;
	Ray ray;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		ray = new Ray(transform.position, DirectionalLight.rotation * Vector3.back);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f);	
		
		Debug.Log(rayHits.Length);
	}
}
