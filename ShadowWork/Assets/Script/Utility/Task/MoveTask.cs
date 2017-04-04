using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTask : Task {
	public GameObject movingObject;
	private MoveInfo moveInfo;
	private float timer = 0.0f;
	public MoveTask(MoveInfo m_moveInfo, GameObject m_movingObject)
	{
		moveInfo = m_moveInfo;
		movingObject = m_movingObject;
		SetStatus(TaskStatus.Detached);
	}
	override internal void TUpdate()
	{
		switch (moveInfo.MethodIndex)
		{
			case 0:
				LerpObjectScript.Instance.LerpObject(movingObject, moveInfo, timer);
				break;
			case 1:
				LerpObjectScript.Instance.SmoothOutLerp(movingObject, moveInfo, timer);
				break;
			default:
				break;
		}
		timer += Time.deltaTime;
		if(ifMoveDone())
		{
			SetStatus(TaskStatus.Success);
		}
	}
	override protected void CleanUp()
	{
		moveInfo = null;
		timer = 0.0f;
	}
	public void SetEndPos(Vector3 m_startPos, Vector3 m_endPos)
	{
		moveInfo.startPos = m_startPos;
		moveInfo.endPos = m_endPos;
	}
	public void SetMoveInfo(MoveInfo m_moveInfo)
	{
		moveInfo = m_moveInfo;
	}
	protected bool ifMoveDone()
	{
		return movingObject.transform.position == moveInfo.endPos;
	}
}
