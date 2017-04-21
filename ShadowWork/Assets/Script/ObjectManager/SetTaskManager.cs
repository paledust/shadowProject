using UnityEngine;

public class SetTaskManager : MonoBehaviour {
	public Task_Manager taskManager{
		get; private set;
	}
	void Awake()
	{
		taskManager = new Task_Manager();
	}

}
