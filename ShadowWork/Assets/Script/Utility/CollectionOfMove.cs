﻿using UnityEngine;

public abstract class TransInfo{
	public float lerpTime;
	public int MethodIndex;
}
public class MoveInfo: TransInfo{
	public MoveInfo(){}
	public MoveInfo(Vector3 _startPos, Vector3 _endPos, float _lerpTime, int _MethodIndex)
	{
		startPos = _startPos;
		endPos = _endPos;
		lerpTime = _lerpTime;
		MethodIndex = _MethodIndex;
	}
	public Vector3 startPos;
	public Vector3 endPos;
}

public class RotationInfo: TransInfo{
	public Vector3 startEularAngle;
	public Vector3 endEularAngle;
}

public class LerpObjectScript {
	static private LerpObjectScript instance;
	static public LerpObjectScript Instance{
		get{
			if(instance == null)
				instance = new LerpObjectScript();
			return instance;
		}
	}

	public void LerpObject(GameObject obj, MoveInfo moveInfo, float currentLerpTime){

		float perc = currentLerpTime/moveInfo.lerpTime;
		obj.transform.position = Vector3.Lerp(moveInfo.startPos, moveInfo.endPos, perc);
	}

	public void LerpObject(GameObject obj, Vector3 startPos, Vector3 endPos, float lerpTime, float currentLerpTime){
		//currentLerpTime += Time.deltaTime;
		if(currentLerpTime >= lerpTime){
			currentLerpTime = lerpTime;
		}
		float perc = currentLerpTime/lerpTime;
		obj.transform.position = Vector3.Lerp(startPos, endPos, perc);
	}

	public void SmoothOutLerp(GameObject obj, MoveInfo camMoveInfo, float currentLerpTime)
	{
		obj.transform.position = Vector3.Lerp(obj.transform.position, camMoveInfo.endPos, Time.deltaTime * 1.0f/camMoveInfo.lerpTime);
	}
	public void SmoothOutLerp(GameObject obj, Vector3 startPos, Vector3 endPos, float lerpTime, float currentLerpTime){
		obj.transform.position = Vector3.Lerp(obj.transform.position, endPos, Time.deltaTime * 1.0f/lerpTime);
		if((obj.transform.position - endPos).magnitude <= 0.01f)
		{
			obj.transform.position = endPos;
		}
	}

	public void RotateObject(GameObject obj, RotationInfo rotateInfo, float currentLerpTime){
		if(currentLerpTime >= rotateInfo.lerpTime){
			currentLerpTime = rotateInfo.lerpTime;
		}

		float perc = currentLerpTime/rotateInfo.lerpTime;
		obj.transform.rotation = Quaternion.Euler(Vector3.Lerp(rotateInfo.startEularAngle, rotateInfo.endEularAngle, perc));
	}

	public void SmoothRotateObject(GameObject obj, RotationInfo rotateInfo, float currentLerpTime){
		obj.transform.rotation = Quaternion.Euler(Vector3.Lerp(obj.transform.rotation.eulerAngles, rotateInfo.endEularAngle, Time.deltaTime * 1.0f/rotateInfo.lerpTime));
		if(Quaternion.Angle(obj.transform.rotation, Quaternion.Euler(rotateInfo.endEularAngle)) <= 0.1f)
		{
			obj.transform.rotation = Quaternion.Euler(rotateInfo.endEularAngle);
		}
	}
}
