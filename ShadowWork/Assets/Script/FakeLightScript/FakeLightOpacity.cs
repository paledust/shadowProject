using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLightOpacity : MonoBehaviour {
	public float internsity = 1.0f;
	private Renderer ObjRenderer;
	// Use this for initialization
	void Start () {
		ObjRenderer = GetComponent<Renderer>();
		ObjRenderer.material.SetFloat("_Alpha", internsity);	
	}
	
	// Update is called once per frame
	void Update () {
		ObjRenderer.material.SetFloat("_Alpha", internsity);
	}
}
