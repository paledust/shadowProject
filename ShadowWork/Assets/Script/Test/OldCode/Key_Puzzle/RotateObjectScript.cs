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

	private Vector3 Axis;

	public GameObject pivot;

	void OnMouseOver(){
		if(Input.GetMouseButton(1) && !dragging){
			dragging = true;
			Debug.Log ("mouseover");
		}
	}
	void Start()
	{
		if(pivot == null)
		{
			pivot = gameObject;
		}

		if(rotateOnX == true){
			Axis = Vector3.right;
		}
		if(rotateOnY == true){
			Axis = Vector3.up;
		}
		if(rotateOnZ == true){
			Axis = Vector3.forward;
		}
	}

	void Update(){
		if(Input.GetMouseButtonUp(1)){
			dragging = false;
		}
		if(lightIsHitting == true){
			if(dragging == true && rotateAround == true){
				if(rotateOnX == true){
					Axis = Vector3.right;
					RotateAround(Axis);
				}
				if(rotateOnY == true){
					Axis = Vector3.up;
					RotateAround(Vector3.up);
				}
				if(rotateOnZ == true){
					Axis = Vector3.forward;
					RotateAround(Vector3.forward);
				}
			}else if(dragging == true){
				if(rotateOnX == true){
					Axis = Vector3.right;
					Rotate(Vector3.right);
				}
				if(rotateOnY == true){
					Axis = Vector3.up;
					Rotate(Vector3.up);
				}
				if(rotateOnZ == true){
					Axis = Vector3.forward;
					Rotate(Vector3.forward);
				}
			}
		}
		if(!dragging)
		{
			// RotateSmooth();
		}
	}
		
	void RotateAround(Vector3 axis){
		float rotX = Input.GetAxis("Mouse X")*rotateSpeed*Mathf.Rad2Deg;
		float rotY = Input.GetAxis("Mouse Y")*rotateSpeed*Mathf.Rad2Deg;

		transform.RotateAround(pivot.transform.position, axis, -rotX);
		transform.RotateAround(pivot.transform.position, axis, -rotY);
	}

	void Rotate(Vector3 axis){
		float rotX = Input.GetAxis("Mouse X")*rotateSpeed*Mathf.Rad2Deg;
		float rotY = Input.GetAxis("Mouse Y")*rotateSpeed*Mathf.Rad2Deg;

		pivot.transform.Rotate(axis * -rotX);
		pivot.transform.Rotate(axis * -rotY);
	}	

	void RotateSmooth()
	{
		Vector3 to = new Vector3 (-90,90,0);
		Quaternion toRotation = Quaternion.Euler(to);
		transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime);
	}
}
