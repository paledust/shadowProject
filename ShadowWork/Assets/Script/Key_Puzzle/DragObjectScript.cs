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
		ifDrag = false;
	}

	//For Dragging things Here and There
	void OnMouseDrag()
	{
		ifDrag = true;
		Vector3 tempVec = Vector3.zero;
		// dragStartPosition = transform.position;
		// dragStartDistance = (Camera.main.transform.position - transform.position).magnitude;
		// Vector3 worldDragTo = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
		// 																	Input.mousePosition.y,
		// 																	dragStartDistance));

		Debug.Log("HitObject");
		if(MouseDir == 0)
		{
			MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
			MouseDir = Mathf.Abs(MouseDir*Mathf.Rad2Deg);
		}

		if(MouseDir > 45 && MouseDir < 135 && direction.ifY)
		{
			tempVec = Vector3.forward;
			//transform.position = new Vector3(dragStartPosition.x, dragStartPosition.y, worldDragTo.z);
			transform.position += tempVec * Input.GetAxis("Mouse Y")*DragSpeed;			
		}
		else if(MouseDir >= 0 && direction.ifX)
		{
			tempVec = Vector3.right;
			//transform.position = new Vector3(worldDragTo.x, dragStartPosition.y, dragStartPosition.z);	
			transform.position += tempVec * Input.GetAxis("Mouse X")*DragSpeed;		
		}
	}

	public void moveTo(Vector3 MoveToPos)
	{
		Debug.Log("Moving");
		Vector3 tempPos = new Vector3(MoveToPos.x, transform.position.y, MoveToPos.z);
		transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime * 3);
		// ifDrag = true;

		if((transform.position - tempPos).magnitude <= 0.1)
		{
			transform.position = tempPos;
		}

		if(transform.position == tempPos)
		{
			ifDrag = false;
		}
	}
}
