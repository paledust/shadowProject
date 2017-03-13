﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class DragObjectScript : MonoBehaviour {
	public DirectionCheck direction;
	public float DragSpeed = 5;
	public bool ifDrag = false;
	private Vector3 dragStartPosition;
	private float dragStartDistance;
	private float MouseDir = 0.0f;

	void Start()
	{
		direction = new DirectionCheck();
	}

	void OnMouseDown()
	{
		MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		MouseDir = Mathf.Abs(MouseDir*Mathf.Rad2Deg);
	}

	void OnMouseUp()
	{
		if(!GetComponent<ObjectStateManager>().objectState.ifFrozen)
			GetComponent<ObjectStateManager>().objectState.SetStatus(MovingState.Frozen);
	}

	//For Dragging things Here and There
	void OnMouseDrag()
	{
		Debug.Log("MOUSE DRAG");
		//Check whether it's Frozen, if it is, the object can't move
		if(!GetComponent<ObjectStateManager>().objectState.ifFrozen && !GetComponent<ObjectStateManager>().objectState.ifPulled)
		{
			//Check whether set to movable state then Set the State from moveable to moving
			if(GetComponent<ObjectStateManager>().objectState.ifMoveable)
				GetComponent<ObjectStateManager>().objectState.SetStatus(MovingState.Moving);
			Vector3 tempVec = Vector3.zero;

			if(MouseDir == 0)
			{
				MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
				MouseDir = Mathf.Abs(MouseDir*Mathf.Rad2Deg);
			}

			if(MouseDir > 30 && MouseDir < 60 && direction.ifZ)
			{
				tempVec = Vector3.forward;
				transform.position += tempVec * Input.GetAxis("Mouse Y")*DragSpeed;			
			}
			else if(MouseDir >= 0 && MouseDir <=30 && direction.ifX)
			{
				tempVec = Vector3.right;
				transform.position += tempVec * Input.GetAxis("Mouse X")*DragSpeed;		
			}
			else if(direction.ifY && MouseDir >= 60 && MouseDir <= 90)
			{
				tempVec = Vector3.up;
				transform.position += tempVec * Input.GetAxis("Mouse X")*DragSpeed;	
			}
		}
	}

	void Update()
	{
		if(!GetComponent<ObjectStateManager>().objectState.ifFrozen && !GetComponent<ObjectStateManager>().objectState.ifPulled)
		{
			//Check whether set to movable state then Set the State from moveable to moving
			if(GetComponent<ObjectStateManager>().objectState.ifMoveable)
				GetComponent<ObjectStateManager>().objectState.SetStatus(MovingState.Moving);
			Vector3 tempVec = Vector3.zero;

			if(MouseDir == 0)
			{
				MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
				MouseDir = Mathf.Abs(MouseDir*Mathf.Rad2Deg);
			}

			if(Input.GetButton("Vertical") && direction.ifZ)
			{
				tempVec = Vector3.forward;
				transform.position += tempVec * Input.GetAxis("Vertical")*DragSpeed;			
			}
			else if(Input.GetButton("Horizontal") && direction.ifX)
			{
				tempVec = Vector3.right;
				transform.position += tempVec * Input.GetAxis("Horizontal")*DragSpeed;		
			}
			else if(Input.GetButton("Height") && direction.ifY)
			{
				tempVec = Vector3.up;
				transform.position += tempVec * Input.GetAxis("Height")*DragSpeed;	
			}
		}
	}

	//Forced Set The Object into some Place
	public void moveTo(Vector3 MoveToPos)
	{
		Debug.Log("Moving");
		Vector3 tempPos = new Vector3(MoveToPos.x, transform.position.y, MoveToPos.z);
		transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime * 3);

		if((transform.position - tempPos).magnitude <= 0.1) 
			transform.position = tempPos;

		if(transform.position == tempPos) 
			ifDrag = false;
	}

	public void SetDirection(DirectionOption dirOption, bool ifAvaliable){
		switch (dirOption)
		{
			case DirectionOption.left: direction.ifLeft = ifAvaliable; break;
			case DirectionOption.right: direction.ifRight = ifAvaliable; break;
			case DirectionOption.up: direction.ifUp = ifAvaliable; break;
			case DirectionOption.down: direction.ifDown = ifAvaliable; break;
			case DirectionOption.forward: direction.ifForward = ifAvaliable; break;
			case DirectionOption.back: direction.ifBack = ifAvaliable; break;
			case DirectionOption.x: direction.ifLeft = ifAvaliable; direction.ifRight = ifAvaliable;Debug.Log("X");  break;
			case DirectionOption.y: direction.ifUp = ifAvaliable; direction.ifDown = ifAvaliable; Debug.Log("Y"); break;
			case DirectionOption.z: direction.ifForward = ifAvaliable; direction.ifBack = ifAvaliable; Debug.Log("Z"); break;
			default: break;
		}
	}
}
