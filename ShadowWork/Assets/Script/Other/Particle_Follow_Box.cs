using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Follow_Box : MonoBehaviour {
	Transform followTrans;
	Vector3 offset;
	// Use this for initialization
	void Start () {
		followTrans = GameObject.Find("MovingBox").transform;
		offset = transform.position - followTrans.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = followTrans.position + offset;
	}
}
