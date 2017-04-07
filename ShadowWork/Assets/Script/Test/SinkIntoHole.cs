using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkIntoHole : MonoBehaviour {


	GameObject lMovingBox;
	GameObject lHole;

	public float speed;

	public int isPass;



	void Start ()
	{
		isPass = 0;

		lMovingBox = GameObject.Find ("MovingBox");
		lHole = GameObject.Find ("Hole");


	}
	

	void Update () 
	{
		float step = speed * Time.deltaTime;

		if (isPass == 1)
		{
			//lMovingBox.transform.position = Vector3.MoveTowards (transform.position, lHole.transform.position, step);
			//lMovingBox.transform.Translate(Vector3.up * Time.deltaTime *10, Space.Self);

			lMovingBox.transform.position = Vector3.Lerp (lMovingBox.transform.position, lHole.transform.position, step);

		}

	}
}
