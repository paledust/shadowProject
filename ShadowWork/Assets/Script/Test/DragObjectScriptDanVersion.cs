﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class DragObjectScriptDanVersion : MonoBehaviour{
	public List<DIRECTION> availableDir; 
	public float DragSpeed = 5f;

	// 0 degrees is right, 90 degrees is up, 180 is left, 270 is down
	//floats for setting angle degrees for each arc
	public float minAngleForward = 120f; //110
	public float maxAngleForward = 180f; //160

	public float minAngleBack = 300f; //290
	public float maxAngleBack = 360f; //340

	public float minAngleRight = 0f; //340-360
	public float maxAngleRight = 60f; //0-40

	public float minAngleLeft = 180f; //160
	public float maxAngleLeft = 240f; //220

	public float minAngleUp = 60f; //70
	public float maxAngleUp = 120f; //110

	public float minAngleDown = 240f; //250
	public float maxAngleDown = 300f; //290

	public FACING_DIRECTIOM activeFace;

	void Update(){
		if(Input.GetButton("Fire1"))
			Mouse_Move();
		if(Input.GetButtonUp("Fire1"))
			OnMouse_Up();
		// Vector3 rayPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
		// ray = Camera.main.ScreenPointToRay(rayPos);
		// Physics.Raycast(ray.origin,ray.direction,out rayhit, 300.0f, layerZ);
		// Debug.Log(rayhit.point.z.ToString() + rayhit.collider.name);
	}
	void Mouse_Move () {
		Vector3 tempVec = Vector3.zero;
		float MouseDir = 0.0f;
		MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		MouseDir = MouseDir*Mathf.Rad2Deg;
		if (MouseDir < 0) {
			MouseDir += 360;
		}
		if(Mouse_Check() == (DIRECTION.FORWARD) && availableDir.Contains(DIRECTION.FORWARD)) {
			tempVec = Vector3.forward;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y") * DragSpeed);
		} else if(Mouse_Check() == (DIRECTION.BACK)&& availableDir.Contains(DIRECTION.BACK)) {
			tempVec = Vector3.forward;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y") * DragSpeed);
		} else if (Mouse_Check() == (DIRECTION.RIGHT) && availableDir.Contains(DIRECTION.RIGHT)) {
			tempVec = Vector3.right;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse X") * DragSpeed);
		} else if(Mouse_Check() == (DIRECTION.LEFT) && availableDir.Contains(DIRECTION.LEFT)) {
			tempVec = Vector3.right;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse X") * DragSpeed);
		} else if (Mouse_Check() == (DIRECTION.UP) && availableDir.Contains(DIRECTION.UP)) {
			tempVec = Vector3.up;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y") * DragSpeed);
		} else if(Mouse_Check() == (DIRECTION.DOWN) && availableDir.Contains(DIRECTION.DOWN)) {
			tempVec = Vector3.up;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y") * DragSpeed);
		} else {
		}
	}
	DIRECTION Mouse_Check(){
		Vector3 tempVec = Vector3.zero;
		float MouseDir = 0.0f;
		MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		MouseDir = MouseDir*Mathf.Rad2Deg;
		if (MouseDir < 0) {
			MouseDir += 360;
		}
		if(MouseDir > minAngleForward && MouseDir <= maxAngleForward && (activeFace == (FACING_DIRECTIOM.Y))) {
			return DIRECTION.FORWARD;
		} else if(MouseDir > minAngleBack && MouseDir <= maxAngleBack && (activeFace == (FACING_DIRECTIOM.Y))) {
			return DIRECTION.BACK;
		} else if (MouseDir > minAngleRight && MouseDir <= maxAngleRight && (activeFace == (FACING_DIRECTIOM.Z) || activeFace == (FACING_DIRECTIOM.Y))) {
			return DIRECTION.RIGHT;
		} else if(MouseDir > minAngleLeft && MouseDir <= maxAngleLeft && (activeFace == (FACING_DIRECTIOM.Z) || activeFace == (FACING_DIRECTIOM.Y))) {
			return DIRECTION.LEFT;
		} else if ((MouseDir > minAngleUp && MouseDir <= maxAngleUp) && (activeFace == (FACING_DIRECTIOM.Z))) {
			return DIRECTION.UP;
		} else if(MouseDir > minAngleDown && MouseDir <= maxAngleDown && (activeFace == (FACING_DIRECTIOM.Z))) {
			return DIRECTION.DOWN;
		} else {
			return DIRECTION.EMPTY;
		}
	}
	void OnMouse_Up(){
		activeFace = FACING_DIRECTIOM.EMPTY;
	}
	public void ClearActiveFace(){
		activeFace = FACING_DIRECTIOM.EMPTY;
	}
	public void AddFaceDir(FACING_DIRECTIOM face_Direction){
		if(activeFace != (face_Direction) && activeFace == FACING_DIRECTIOM.EMPTY)
			activeFace = face_Direction;
	}
}
