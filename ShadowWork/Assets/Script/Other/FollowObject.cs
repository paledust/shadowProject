using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
	[SerializeField] Transform trans;
	private Vector3 currentOffset;
	// Use this for initialization
	void Start () {
		currentOffset = transform.position - trans.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = trans.position + currentOffset;
	}
}
