using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveInfo{
	public Vector3 startPos;
	public Vector3 endPos;
	public float lerpTime;
	public float lerpSpeed;
	public int MethodIndex;
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
	public void LerpObject(GameObject obj, CameraMoveInfo camMoveInfo, float currentLerpTime){
		//currentLerpTime += Time.deltaTime;
		if(currentLerpTime >= camMoveInfo.lerpTime){
			currentLerpTime = camMoveInfo.lerpTime;
		}
		float perc = currentLerpTime/camMoveInfo.lerpTime;
		obj.transform.position = Vector3.Lerp(camMoveInfo.startPos, camMoveInfo.endPos, perc);
	}

	public void LerpObject(GameObject obj, Vector3 startPos, Vector3 endPos, float lerpTime, float currentLerpTime){
		//currentLerpTime += Time.deltaTime;
		if(currentLerpTime >= lerpTime){
			currentLerpTime = lerpTime;
		}
		float perc = currentLerpTime/lerpTime;
		obj.transform.position = Vector3.Lerp(startPos, endPos, perc);
	}

	public void SmoothOutLerp(GameObject obj, CameraMoveInfo camMoveInfo)
	{
		obj.transform.position = Vector3.Lerp(camMoveInfo.startPos, camMoveInfo.endPos, camMoveInfo.lerpSpeed * Time.deltaTime);
		if((obj.transform.position - camMoveInfo.endPos).magnitude <= 0.01f)
		{
			obj.transform.position = camMoveInfo.endPos;
		}
	}
	public void SmoothOutLerp(GameObject obj, Vector3 startPos, Vector3 endPos, float lerpSpeed)
	{
		obj.transform.position = Vector3.Lerp(startPos, endPos, lerpSpeed * Time.deltaTime);
		if((obj.transform.position - endPos).magnitude <= 0.01f)
		{
			obj.transform.position = endPos;
		}
	}
}
