using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectScript : MonoBehaviour {

	float distance = 10;
	public float dragSpeed = 1;

		
	void OnMouseDrag(){

		transform.parent.Translate(dragSpeed,0,0);
	}
}
