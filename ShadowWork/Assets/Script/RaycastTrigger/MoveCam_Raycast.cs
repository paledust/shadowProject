using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam_Raycast : RaycastBase {
	public Transform camPosTransform;
	public float lerpTime;
	private Vector3 endPos{
		get{
			return camPosTransform.position;
		}
	}
	private MoveInfo camMoveInfo;
	private bool ifTrigger = false;
	// Use this for initialization
	override protected void Start () {
		base.Start();
				
		camMoveInfo = new MoveInfo();
		camMoveInfo.startPos = Camera.main.gameObject.transform.position;
		camMoveInfo.endPos = endPos;
		camMoveInfo.lerpTime = lerpTime;
		camMoveInfo.MethodIndex = 0;
	}
	override protected void AddTask()
	{
		if(!ifTrigger)
		{
			FireCameraMoveEvent();
			ifTrigger = true;
		}
	}

	void FireCameraMoveEvent()
	{
		camMoveInfo.startPos = Camera.main.gameObject.transform.position;
		
		CameraMoveEvent tempEvent = new CameraMoveEvent();
		tempEvent.camMoveInfo = camMoveInfo;
		Service.eventManager.FireEvent(tempEvent);
	}
}
