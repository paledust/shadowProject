using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class DetectInShadow : MonoBehaviour {
	public AvailableDir setDirection;
	RaycastHit rayHit;
	Ray ray;
	
	void Start()
	{
		setDirection = new AvailableDir();
		setDirection.ifX = true;
		setDirection.ifY = true;
		setDirection.ifZ = true;
	}
	// Update is called once per frame
	void Update () {
		ray = new Ray(transform.position, transform.rotation * Vector3.up);

		if(Physics.Raycast(ray.origin,ray.direction,100.0f) && GetComponentInParent<ObjectStateManager>().objectState.ifFrozen)
		{
			GetComponentInParent<ObjectStateManager>().objectState.SetStatus(MovingState.Moveable);
		}

		Debug.DrawLine(ray.origin,ray.direction * 100 + ray.origin, Color.white);
	}
}
