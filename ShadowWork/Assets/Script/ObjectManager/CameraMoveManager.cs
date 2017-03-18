using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveManager : MonoBehaviour {
	public bool ifMove;
	private float timer = 0.0f;
	private CameraMoveInfo camMoveInfo;
	// Use this for initialization
	void Start () {
		ifMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(ifMove)
		{
			moveCamera(camMoveInfo.MethodIndex);
			if(Camera.main.gameObject.transform.position == camMoveInfo.endPos)
			{
				ResetToUnMove();
			}
		}
	}

	private void moveCamera(int methodIndex)
	{
		switch (methodIndex)
		{
			case 0:
				LerpObjectScript.Instance.LerpObject(Camera.main.gameObject, camMoveInfo, timer);
				break;
			case 1:
				LerpObjectScript.Instance.SmoothOutLerp(Camera.main.gameObject, camMoveInfo, timer);
				break;
			default:
				break;
		}
		timer += Time.deltaTime;
	}

	public void ResetToUnMove()
	{
		//Debug.Log("Reset Camera To Unmove");
		ifMove = false;
		camMoveInfo = null;
		timer = 0.0f;
	}

	public void SetMoveInfo(CameraMoveInfo m_camMoveInfo)
	{
		camMoveInfo = m_camMoveInfo;
	}
	public bool ifMoveToEndPos()
	{
		return Camera.main.gameObject.transform.position == camMoveInfo.endPos;
	}

}
