using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectScript : MonoBehaviour {


	bool dragEnabled = false;
	Vector3 dragStartPosition;
	float dragStartDistance;

	void OnMouseDown()
	{
		dragEnabled = true;
		dragStartPosition = transform.parent.position;
		dragStartDistance = (Camera.main.transform.position - transform.parent.position).magnitude;
	}
	void Update()
	{
		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			dragEnabled = false;
		}
	}
	void OnMouseDrag()
	{
		if (dragEnabled)
		{
			Vector3 worldDragTo = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragStartDistance));
			transform.parent.position = new Vector3(worldDragTo.x, dragStartPosition.y, dragStartPosition.z);
		}
	}


}
