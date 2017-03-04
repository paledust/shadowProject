using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectScript : MonoBehaviour {

	public float rotateSpeed = 20;

	public bool dragging = false;

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
		if(dragging == true){
			float rotX = Input.GetAxis("Mouse X")*rotateSpeed*Mathf.Deg2Rad;
			float rotY = Input.GetAxis("Mouse Y")*rotateSpeed*Mathf.Deg2Rad;

			transform.RotateAround(transform.parent.position, Vector3.forward, -rotX);
			transform.RotateAround(transform.parent.position, Vector3.forward, -rotY);
		}

	}
		
}
