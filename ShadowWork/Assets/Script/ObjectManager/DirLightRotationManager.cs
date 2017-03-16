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
			LerpObjectScript.Instance.RotateObject(gameObject, rotationInfo, timer);
			timer += Time.deltaTime;
			if(transform.rotation.eulerAngles == rotationInfo.endEularAngle)
			{
				ResetToUnRotate();
			}
		}
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
}
