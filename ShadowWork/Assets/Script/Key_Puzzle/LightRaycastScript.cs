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

		if(Physics.SphereCast(transform.position, radius, (forward), out hit, 100)){
			distanceToObject = hit.distance;
			//print (distanceToObject + " " + hit.collider.gameObject.name);

			if(!lastRotatableObjectHit == null && !hit.collider.gameObject == lastRotatableObjectHit){
				lastRotatableObjectHit.GetComponent<RotateObjectScript>().lightIsHitting = false;
				print (lastRotatableObjectHit + " Check");
			}
			if(hit.collider.tag == "Rotatable"){
				lastRotatableObjectHit = hit.collider.gameObject;
				lastRotatableObjectHit.GetComponent<RotateObjectScript>().lightIsHitting = true;


			} else{
				lastRotatableObjectHit.GetComponent<RotateObjectScript>().lightIsHitting = false;
				print ("false");
				//lastRotatableObjectHit = null;
			}
		}
	}
}
