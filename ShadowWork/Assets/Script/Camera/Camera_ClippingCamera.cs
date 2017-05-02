using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ClippingCamera : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		transform.rotation = Camera.main.transform.rotation;
		transform.position = Camera.main.transform.position;
	}
}
