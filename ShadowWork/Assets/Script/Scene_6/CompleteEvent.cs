using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteEvent : MonoBehaviour {
	public Transform directionLight;
	public float CompleteTime;
	public float targetAngle = 0.0f;
	private float timer;
	private bool doEvent = false;
	void Start()
	{
		doEvent = false;
		timer = 0.0f;
	}
	// Update is called once per frame
	void Update () {
		Debug.Log(Mathf.Abs(directionLight.eulerAngles.y - targetAngle));
		if(Mathf.Abs(directionLight.eulerAngles.y) <= targetAngle)
		{
			Debug.Log("Great!");
			timer += Time.deltaTime;
		}
		else
		{
			timer = 0.0f;
		}

		if(timer >= CompleteTime && !doEvent)
		{
			doEvent = true;
		}

		if(doEvent)
		{
			Debug.Log("Now!");
			transform.position += Vector3.up * 2 * Time.deltaTime;
		}
	}
}
