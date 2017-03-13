using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLevelManage : MonoBehaviour {
	LevelCompleteHandler _levelCompleteHandler;
	public Transform ActiveDirectionalLight;
	Ray ray;
	RaycastHit rayhit;
	// Use this for initialization
	void Start () {
		_levelCompleteHandler = GetComponent<LevelCompleteHandler>();
		_levelCompleteHandler.RegisterFunction();

		transform.rotation = ActiveDirectionalLight.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		ray = new Ray(transform.position, transform.rotation * Vector3.forward);
		if(Physics.Raycast(ray.origin,ray.direction, out rayhit, 500.0f))
		{
			if(rayhit.collider.gameObject.name == "MovingBox" && positionCheck(rayhit.collider.transform))
			{
				CompleteEvent e = new CompleteEvent();
				EventManager.Instance.FireEvent(e);
			}
		}

		Debug.DrawLine(ray.origin,ray.direction * 500 + ray.origin, Color.green);
		if(Input.GetKeyDown(KeyCode.R))
		{
			RestartEvent e = new RestartEvent();
			EventManager.Instance.FireEvent(e);
		}
	}

	bool positionCheck(Transform box_Trans)
	{
		Vector3 temp = rayhit.point - box_Trans.position;
		temp.y = 0.0f;
		if(temp.magnitude <= 1.0f)
		{
			return true;
		}
		return false;
	}
}
