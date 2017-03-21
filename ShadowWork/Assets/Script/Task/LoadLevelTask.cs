using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTask : Task {
	private int LevelIndex;
	public LoadLevelTask(int m_LevelIndex)
	{
		LevelIndex = m_LevelIndex;
		SetStatus(TaskStatus.Detached);
	}
	// Use this for initialization
	override protected void Init()
	{
		LoadLevelEvent tempEvent = new LoadLevelEvent();
		tempEvent.NextLevelIndex = LevelIndex;
		EventManager.Instance.FireEvent(tempEvent);
	}
}
