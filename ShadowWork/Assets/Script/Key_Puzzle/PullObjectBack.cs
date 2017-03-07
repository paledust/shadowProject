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
			
			if(!dragObject.ifMoveing && !CheckPos(collider.gameObject,gameObject))
			{
				Debug.Log("What");
				collider.transform.position = new Vector3(transform.position.x, 
																	collider.transform.position.y, 
																	transform.position.z);
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
