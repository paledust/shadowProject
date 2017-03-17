using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLightRotationManager : MonoBehaviour {
	public bool ifRotate;
	private float timer;
	private RotationInfo rotationInfo;
	void Update () {
		if(ifRotate)
		{
			rotateLight(rotationInfo.MethodIndex);
			timer += Time.deltaTime;
			if(transform.rotation.eulerAngles == rotationInfo.endEularAngle)
			{
				ResetToUnRotate();
			}
		}
	}

	private void rotateLight(int methodIndex)
	{
		switch (methodIndex)
		{
			case 0:
				LerpObjectScript.Instance.RotateObject(gameObject, rotationInfo, timer);
				break;
			case 1:
				LerpObjectScript.Instance.SmoothRotateObject(gameObject, rotationInfo, timer);
				break;
			default:
				break;
		}
		timer += Time.deltaTime;
	}
	public void ResetToUnRotate()
	{
		Debug.Log("Reset Camera To Unmove");
		ifRotate = false;
		rotationInfo = null;
		timer = 0.0f;
	}

	public void setRotateInfo(RotationInfo m_rotationInfo)
	{
		rotationInfo = m_rotationInfo;
	}
	public bool ifRotateToEndEular()
	{
		return transform.rotation.eulerAngles == rotationInfo.endEularAngle;
	}
}
