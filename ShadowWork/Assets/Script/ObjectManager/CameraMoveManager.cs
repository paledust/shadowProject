using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveManager : MonoBehaviour {
	public bool ifMove;
	private float timer;
	private CameraMoveInfo camMoveInfo;
	// Use this for initialization
	void Start () {
		ifMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(ifMove)
		{
			LerpObjectScript.Instance.LerpObject(Camera.main.gameObject, camMoveInfo, timer);
			timer += Time.deltaTime;
			if(Camera.main.gameObject.transform.position == camMoveInfo.endPos)
			{
				ResetToUnMove();
			}
		}
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
}
