using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour {
	[SerializeField] float Ratio;
	private float Height;
	private float Ori_Size;
	private Projector project;
	// Use this for initialization
	void Start () {
		Height = transform.position.y;
		project = GetComponent<Projector>();
		Ori_Size = project.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		project.orthographicSize = Ori_Size - 1 * (transform.position.y - Height)/Ratio;
	}
}
