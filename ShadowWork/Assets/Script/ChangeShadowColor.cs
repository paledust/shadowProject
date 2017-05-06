using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShadowColor : MonoBehaviour {
	private Color OriColor;
	private float OriHeight;
	// Use this for initialization
	void Start () {
		OriHeight = transform.position.y;
		OriColor = GetComponent<Projector>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Projector>().material.color = Color.Lerp(OriColor, Color.yellow, (transform.position.y - OriHeight)/100);
	}
}
