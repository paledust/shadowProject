using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLightRotationManager : MonoBehaviour {
	public bool ifRotate;
	private float timer;
	// Use this for initialization
	// void Update () {
	// 	if(ifMove)
	// 	{
	// 		LerpObjectScript.Instance.LerpObject(Camera.main.gameObject, camMoveInfo, timer);
	// 		timer += Time.deltaTime;
	// 		if(Camera.main.gameObject.transform.position == camMoveInfo.endPos)
	// 		{
	// 			ResetToUnMove();
	// 		}
	// 	}
	// }

	// public void ResetToUnMove()
	// {
	// 	Debug.Log("Reset Camera To Unmove");
	// 	ifMove = false;
	// 	camMoveInfo = null;
	// 	timer = 0.0f;
	// }

	// public void SetMoveInfo(CameraMoveInfo m_camMoveInfo)
	// {
	// 	camMoveInfo = m_camMoveInfo;
	// }
}
