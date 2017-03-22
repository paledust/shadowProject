using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel_Raycast : RaycastBase {
	LoadLevelTask loadLevelTask;
	public int levelIndex;
	override protected void Start()
	{
		base.Start();
		loadLevelTask = new LoadLevelTask(levelIndex);
	}

	override protected bool ifTaskDetached()
	{
		return loadLevelTask.ifDetached;
	}

	override protected void AddTask()
	{
		taskManager.AddTask(loadLevelTask);
	}

}
