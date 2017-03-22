using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTask : Task {
	private GameObject RotateObject;
	private RotationInfo rotationInfo;
	private float timer = 0.0f;
	public RotateTask(RotationInfo m_rotationInfo, GameObject m_RotateObject)
	{
		RotateObject = m_RotateObject;
		rotationInfo = m_rotationInfo;
		SetStatus(TaskStatus.Detached);
	}
	override internal void TUpdate()
	{
		switch (rotationInfo.MethodIndex)
		{
			case 0:
				LerpObjectScript.Instance.RotateObject(RotateObject, rotationInfo, timer);
				break;
			case 1:
				LerpObjectScript.Instance.SmoothRotateObject(RotateObject, rotationInfo, timer);
				break;
			default:
				break;
		}
		timer += Time.deltaTime;
		if(ifRotateDone())
		{
			Debug.Log("RotateTask Done");
			SetStatus(TaskStatus.Success);
		}
	}
	override protected void CleanUp()
	{
		rotationInfo = null;
		timer = 0.0f;
	}
	protected bool ifRotateDone()
	{
		return RotateObject.transform.rotation.eulerAngles == rotationInfo.endEularAngle;
	}
}
