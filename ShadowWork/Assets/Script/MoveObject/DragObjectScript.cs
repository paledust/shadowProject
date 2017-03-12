using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

namespace CS_Kevin
{
	public class AvailableDir
	{
		public bool ifX;
		public bool ifY;
		public bool ifZ;
	}
}

public class DragObjectScript : MonoBehaviour {
	public AvailableDir direction;
	public float DragSpeed = 5;
	public bool ifDrag = false;
	private Vector3 dragStartPosition;
	private float dragStartDistance;
	private float MouseDir = 0.0f;

	void Start()
	{
		direction = new AvailableDir();

		direction.ifX = false;
		direction.ifY = false;
		direction.ifZ = false;
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
		//ifDrag = false;
	}

	//For Dragging things Here and There
	void OnMouseDrag()
	{
		//ifDrag = true;

		if(!GetComponent<ObjectStateManager>().objectState.ifFrozen)
		{
			if(!GetComponent<ObjectStateManager>().objectState.ifMoving)
				GetComponent<ObjectStateManager>().objectState.SetStatus(MovingState.Moving);
			Vector3 tempVec = Vector3.zero;

			if(MouseDir == 0)
			{
				MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
				MouseDir = Mathf.Abs(MouseDir*Mathf.Rad2Deg);
			}

			if(MouseDir > 45 && MouseDir < 135 && direction.ifY)
			{
				tempVec = Vector3.forward;
				transform.position += tempVec * Input.GetAxis("Mouse Y")*DragSpeed;			
			}
			else if(MouseDir >= 0 && direction.ifX)
			{
				tempVec = Vector3.right;
				transform.position += tempVec * Input.GetAxis("Mouse X")*DragSpeed;		
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
		{
			transform.position = tempPos;
		}

		if(transform.position == tempPos)
		{
			ifDrag = false;
		}
	}

	public void SetDirectionX(bool ifAvaliable){direction.ifX = ifAvaliable;}
	public void SetDirectionY(bool ifAvaliable){direction.ifY = ifAvaliable;}
	public void SetDirectionZ(bool ifAvaliable){direction.ifZ = ifAvaliable;}
}
