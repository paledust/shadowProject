using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight_Raycast : RaycastBase {
	public GameObject new_light_Object;
	private GameObject old_light_Object;
	private Light new_light{
		get{
			return new_light_Object.GetComponent<Light>();
		}
	}
	private Light old_light{
		get{
			return old_light_Object.GetComponent<Light>();
		}
	}
	private SwitchLighTask switchLightTask;
	private bool ifTrigger = false;

	// Use this for initialization
	override protected void Start () {
		base.Start();
		old_light_Object = KeyObjCollect.Instance.ActiveDirLight;
		switchLightTask = new SwitchLighTask(new_light, old_light);
	}
	override protected void AddTask()
	{
		if(!ifTrigger)
		{
			taskManager.AddTask(switchLightTask);
			ifTrigger = true;
		}
	}
}
