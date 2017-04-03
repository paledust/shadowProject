using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectScriptDanVersion : MonoBehaviour{


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

	private float MouseDir = 0.0f;



	void OnMouseDrag () 
	{

		Vector3 tempVec = Vector3.zero;

		MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		MouseDir = MouseDir*Mathf.Rad2Deg;

		if (MouseDir < 0) {
			MouseDir += 360;
		}
			
		if ((MouseDir > minAngleForward && MouseDir <= maxAngleForward) || (MouseDir > minAngleBack && MouseDir <= maxAngleBack)) {
			tempVec = Vector3.forward;
			transform.position += tempVec * (Input.GetAxis ("Mouse Y")) * DragSpeed;			
		} else if ((MouseDir > minAngleRight && MouseDir <= maxAngleRight) || (MouseDir > minAngleLeft && MouseDir <= maxAngleLeft)) {
			tempVec = Vector3.right;
			transform.position += tempVec * Input.GetAxis ("Mouse X") * DragSpeed;		
		} else if ((MouseDir > minAngleUp && MouseDir <= maxAngleUp) || (MouseDir > minAngleDown && MouseDir <= maxAngleDown)) {
			
			tempVec = Vector3.up;
			transform.position += tempVec * Input.GetAxis ("Mouse Y") * DragSpeed;	
		}
		Debug.Log ("Direction: " + MouseDir);

	}

}
