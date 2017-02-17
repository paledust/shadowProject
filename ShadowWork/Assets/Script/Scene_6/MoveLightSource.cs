using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLightSource : MonoBehaviour {
	public float miniMove;
	public Transform lightDirection;
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1"))
		{
			if(Input.GetAxis("Mouse X") > 0)
			{
				lightDirection.rotation = Quaternion.Euler(lightDirection.rotation.eulerAngles.x,
															lightDirection.rotation.eulerAngles.y + miniMove,
															lightDirection.rotation.eulerAngles.z);
			}

			if(Input.GetAxis("Mouse X") < 0)
			{
				lightDirection.rotation = Quaternion.Euler(lightDirection.rotation.eulerAngles.x,
															lightDirection.rotation.eulerAngles.y - miniMove,
															lightDirection.rotation.eulerAngles.z);
			}
		}
	}
}
