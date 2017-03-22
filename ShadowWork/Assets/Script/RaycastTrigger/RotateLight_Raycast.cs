using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight_Raycast : RaycastBase {
	public Vector3 Dir_TargetRotationEuler;
	public float lerpTime;
	private RotationInfo dirRotateInfo; 
	private bool ifTrigger = false;
	// Use this for initialization
	override protected void Start () {
		base.Start();
		dirRotateInfo = new RotationInfo();
		dirRotateInfo.startEularAngle = KeyObjCollect.Instance.ActiveDirLight.transform.rotation.eulerAngles;
		dirRotateInfo.endEularAngle = Dir_TargetRotationEuler;
		dirRotateInfo.lerpTime = lerpTime;
		dirRotateInfo.MethodIndex = 0;
	}
	override protected void AddTask()
	{
		if(!ifTrigger)
		{
			FirechangeDirLightEvent();
			ifTrigger = true;
		}
	}

	void FirechangeDirLightEvent()
	{
		dirRotateInfo.startEularAngle = KeyObjCollect.Instance.ActiveDirLight.transform.rotation.eulerAngles;
		
		changeDirLightEvent tempEvent = new changeDirLightEvent();
		tempEvent.dirRotationInfo = dirRotateInfo;
		EventManager.Instance.FireEvent(tempEvent);
	}
}
