using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel_Raycast : MonoBehaviour {
	Ray ray;
	RaycastHit rayhit;
	LoadLevelTask loadLevelTask;
	Task_Manager taskManager;
	public int levelIndex;
	void Start()
	{
		loadLevelTask = new LoadLevelTask(levelIndex);
		taskManager = GetComponent<Task_Manager>();
	}
	// Update is called once per frame
	void Update () {
		ray = new Ray(transform.position, transform.rotation * Vector3.forward);

		if(Physics.Raycast(ray.origin,ray.direction, out rayhit, 500.0f))
		{
			if(rayhit.collider.gameObject.name == "MovingBox" && positionCheck(rayhit.collider.transform) && 
				loadLevelTask.ifDetached)
			{
				Debug.Log("Set Work");
				taskManager.AddTask(loadLevelTask);
			}
		}
	}

	private bool positionCheck(Transform box_Trans)
	{
		Vector3 temp = rayhit.point - box_Trans.position;
		temp.y = 0.0f;
		if(temp.magnitude <= 10.0f)
		{
			return true;
		}
		return false;
	}
}
