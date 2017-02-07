using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThePillar : MonoBehaviour {

	public Transform followingPillar;

	private Vector3 rotateAngle;
	// Use this for initialization
	void Start () {
		rotateAngle = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		rotateAngle = transform.rotation.eulerAngles;
		Debug.Log(rotateAngle);
		followingPillar.rotation = Quaternion.Euler(rotateAngle.x, -rotateAngle.y, rotateAngle.z);
	}
}
