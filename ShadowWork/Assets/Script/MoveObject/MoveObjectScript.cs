using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectScript : MonoBehaviour {
	public GameObject objectToMove;
	public GameObject endPoint1;
	public GameObject endPoint2;

	private Vector3 currentPos;
	private Vector3 endPos1;
	private Vector3 endPos2;

	public float lerpTime = 5;

	private float currentLerpTime = 0;

	private bool keyHit = false;
	private bool keyHit2 = false;

	// Use this for initialization
	void Start () {
		currentPos = objectToMove.transform.position;
		endPos1 = endPoint1.transform.position;
		endPos2 = endPoint2.transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)){
			currentLerpTime = 0;
			keyHit2 = false;
			keyHit = true;
		}
		if (Input.GetKeyDown(KeyCode.O)){
			currentLerpTime = 0;
			keyHit = false;
			keyHit2 = true;
		}
		if(keyHit == true){
			LerpObject(currentPos, endPos1);
		}
		if(keyHit2 == true){
			LerpObject(currentPos, endPos2);
		}
	}

	void LerpObject(Vector3 pos1, Vector3 pos2){
		currentLerpTime += Time.deltaTime;
		if(currentLerpTime >= lerpTime){
			currentLerpTime = lerpTime;
		}
		float perc = currentLerpTime/lerpTime;
		objectToMove.transform.position = Vector3.Lerp(pos1, pos2, perc);
		currentPos = objectToMove.transform.position;
	}
}
