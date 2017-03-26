using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaycastBase : MonoBehaviour {
	Ray ray;
	RaycastHit rayhit;
	protected Task_Manager taskManager;
	
	virtual protected void Start()
	{
		taskManager = GetComponent<SetTaskManager>().taskManager;
	}
	// Update is called once per frame
	protected void Update () {
		ray = new Ray(transform.position, transform.rotation * Vector3.forward);

		if(Physics.Raycast(ray.origin,ray.direction, out rayhit, 500.0f))
		{
			if(rayhit.collider.gameObject.name == "MovingBox" && positionCheck(rayhit.collider.transform) && 
				ifTaskDetached())
			{
				AddTask();
			}
		}

		taskManager.Update();
	}
	protected bool positionCheck(Transform box_Trans)
	{
		Vector3 temp = rayhit.point - box_Trans.position;
		temp.y = 0.0f;
		if(temp.magnitude <= 10.0f)
		{
			return true;
		}
		return false;
	}
	virtual protected bool ifTaskDetached()
	{
		return true;
	}
	virtual protected void AddTask()
	{

	}
}
