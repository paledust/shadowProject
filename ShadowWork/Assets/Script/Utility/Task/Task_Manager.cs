using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Manager{
	public List<Task> tasks = new List<Task>();
	public void Update()
	{
		for (int i = tasks.Count-1; i >= 0;i--)
		{
			Task task = tasks[i];
			if(task.ifPending) 
				task.SetStatus(Task.TaskStatus.Working);

			if(task.ifFinished) 
				HandleCompletion(task,i);
			else
			{
				task.TUpdate();
				if(task.ifFinished) HandleCompletion(task,i);
			}
		}
	}

	void HandleCompletion(Task task, int index)
	{
		if(task.NextTask != null && task.ifSuccess)
			AddTask(task.NextTask);
		tasks.RemoveAt(index);
		task.SetStatus(Task.TaskStatus.Detached);
	}

	public void AddTask(Task task)
	{
		Debug.Assert(task.ifDetached);
		tasks.Add(task);
		task.SetStatus(Task.TaskStatus.Pending);
	} 
}
