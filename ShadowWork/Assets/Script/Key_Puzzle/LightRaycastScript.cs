using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRaycastScript : MonoBehaviour {

	public GameObject lastRotatableObjectHit;

	public float radius = 10;

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		float distanceToObject;

		Vector3 forward = transform.TransformDirection (Vector3.forward) * 100;
		Debug.DrawRay(transform.position, forward, Color.green);

		if(Physics.Raycast(transform.position, (forward), out hit, 100)){
			distanceToObject = hit.distance;
			if(lastRotatableObjectHit != null)
			{
				if(hit.collider.gameObject != lastRotatableObjectHit){
					lastRotatableObjectHit.GetComponent<RotateObjectScript>().lightIsHitting = false;
					print (lastRotatableObjectHit + " Check");
				}
				if(hit.collider.tag == "Rotatable"){
					lastRotatableObjectHit.GetComponent<RotateObjectScript>().lightIsHitting = false;
					lastRotatableObjectHit = hit.collider.gameObject;
					lastRotatableObjectHit.GetComponent<RotateObjectScript>().lightIsHitting = true;
				} 
				else
				{
					lastRotatableObjectHit.GetComponent<RotateObjectScript>().lightIsHitting = false;
					print ("false");
					//lastRotatableObjectHit = null;
				}
			}
			else if(hit.collider.tag == "Rotatable")
			{
				lastRotatableObjectHit = hit.collider.gameObject;
				lastRotatableObjectHit.GetComponent<RotateObjectScript>().lightIsHitting = true;
			}
		}
	}
}
