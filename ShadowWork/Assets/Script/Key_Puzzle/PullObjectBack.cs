using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObjectBack : MonoBehaviour {
	void OnTriggerStay(Collider collider)
	{
		Debug.Log(collider.gameObject.name);
		DragObjectScript dragObject;
		if(collider.gameObject.GetComponent<DragObjectScript>())
		{
			dragObject = collider.gameObject.GetComponent<DragObjectScript>();

			if(!dragObject.ifDrag && !CheckPos(collider.gameObject, gameObject))
			{
				collider.GetComponent<DragObjectScript>().moveTo(transform.position);
			}
		}
	}

	bool CheckPos(GameObject A, GameObject B)
	{
		if(A.transform.position.x == B.transform.position.x &&
			A.transform.position.z == B.transform.position.z)
		{
			return true;
		}
		return false;
	}

}
