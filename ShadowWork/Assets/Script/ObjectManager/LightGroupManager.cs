using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGroupManager : MonoBehaviour {
	private SwitchLighTask switchLightTask; 
	private Task_Manager taskManager;
	// Use this for initialization
	void Start () {
		taskManager = new Task_Manager();
	}

	private void SwitchLightHandler(Event e)
	{
		swithDirLightEvent tempEvent = e as swithDirLightEvent;
		switchLightTask = new SwitchLighTask(tempEvent.light_Start, tempEvent.light_End);

		taskManager.AddTask(switchLightTask);
	}
}
