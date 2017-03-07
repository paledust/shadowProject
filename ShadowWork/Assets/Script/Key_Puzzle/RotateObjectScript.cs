using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectScript : MonoBehaviour {

	public float rotateSpeed = 20;

	public bool lightIsHitting = false;

	public bool dragging = false;

	public bool rotateOnX = false;
	public bool rotateOnY = false;
	public bool rotateOnZ = false;

	public bool rotateAround = false;

	public GameObject pivot;

	void OnMouseOver(){
		if(Input.GetMouseButton(1) && dragging == false){
			dragging = true;
			Debug.Log ("mouseover");
		}
	}

	void Update(){
		if(Input.GetMouseButtonUp(1)){
			dragging = false;
		}
		if(lightIsHitting == true){
			if(dragging == true && rotateAround == true){
				if(rotateOnX == true){
					RotateAround(Vector3.right);
				}
				if(rotateOnY == true){
					RotateAround(Vector3.up);
				}
				if(rotateOnZ == true){
					RotateAround(Vector3.forward);
				}
			}else if(dragging == true){
				if(rotateOnX == true){
					Rotate(Vector3.right);
				}
				if(rotateOnY == true){
					Rotate(Vector3.up);
				}
				if(rotateOnZ == true){
					Rotate(Vector3.forward);
				}
			}
		}
	}
		
	void RotateAround(Vector3 axis){
		float rotX = Input.GetAxis("Mouse X")*rotateSpeed*Mathf.Deg2Rad;
		float rotY = Input.GetAxis("Mouse Y")*rotateSpeed*Mathf.Deg2Rad;

		transform.RotateAround(pivot.transform.position, axis, -rotX);
		transform.RotateAround(pivot.transform.position, axis, -rotY);

	}

	void Rotate(Vector3 axis){
		float rotX = Input.GetAxis("Mouse X")*rotateSpeed*Mathf.Deg2Rad;
		float rotY = Input.GetAxis("Mouse Y")*rotateSpeed*Mathf.Deg2Rad;

		pivot.transform.Rotate(axis * -rotX);
		pivot.transform.Rotate(axis * -rotY);

	}
		
}
