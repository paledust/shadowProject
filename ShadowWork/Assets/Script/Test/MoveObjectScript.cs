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

	public float timeToMove = 5;

	private float timeSpentMoving = 0;

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
			timeSpentMoving = 0;
			currentPos = objectToMove.transform.position;
			keyHit2 = false;
			keyHit = true;
		}
		if (Input.GetKeyDown(KeyCode.O)){
			timeSpentMoving = 0;
			currentPos = objectToMove.transform.position;
			keyHit = false;
			keyHit2 = true;
		}
		if(keyHit == true){
			LerpObjectScript.Instance.LerpObject(objectToMove, currentPos, endPos1, timeToMove, timeSpentMoving);
			timeSpentMoving += Time.deltaTime;
		}
		if(keyHit2 == true){
			LerpObjectScript.Instance.LerpObject(objectToMove, currentPos, endPos2, timeToMove, timeSpentMoving);
			timeSpentMoving += Time.deltaTime;
		}
	}

//	void LerpObject(GameObject obj, Vector3 pos1, Vector3 pos2, float lerpTime, float currentLerpTime){
//		currentLerpTime += Time.deltaTime;
//		if(currentLerpTime >= lerpTime){
//			currentLerpTime = lerpTime;
//		}
//		float perc = currentLerpTime/lerpTime;
//		obj.transform.position = Vector3.Lerp(pos1, pos2, perc);
//	}
}
