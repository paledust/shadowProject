using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionOption
{
	X,
	Y,
	Z
}
public class TrailMove : MonoBehaviour {
	public DirectionOption dirOption;
	// Use this for initialization
	void OnTriggerStay(Collider collider)
	{
		DragObjectScript dragObject;
		if(collider.GetComponent<DragObjectScript>() && !collider.GetComponent<DragObjectScript>().ifDrag)
		{
			dragObject = collider.GetComponent<DragObjectScript>();
			switch (dirOption)
			{
				case DirectionOption.X:
					dragObject.direction.ifX = true;
					break;
				case DirectionOption.Y:
					dragObject.direction.ifY = true;
					break;
				case DirectionOption.Z:
					dragObject.direction.ifZ = true;
					break;
				default:
					break;
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		DragObjectScript dragObject;
		if(collider.gameObject.GetComponent<DragObjectScript>())
		{
			dragObject = collider.gameObject.GetComponent<DragObjectScript>();
			switch (dirOption)
			{
				case DirectionOption.X:
					dragObject.direction.ifX = false;
					break;
				case DirectionOption.Y:
					dragObject.direction.ifY = false;
					break;
				case DirectionOption.Z:
					dragObject.direction.ifZ = false;
					break;
				default:
					break;
			}
		}
	}
}
