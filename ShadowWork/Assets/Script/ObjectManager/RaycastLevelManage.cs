using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLevelManage : MonoBehaviour {
	public Transform ActiveDirectionalLight;
	public Vector3 startPos;
	public Vector3 endPos;
	public float lerpTime;
	public int NextLevelIndex;
	public Vector3 Dir_TargetRotationEuler;
	private Quaternion Dir_TargetRotation;
	LevelCompleteHandler _levelCompleteHandler;
	Ray ray;
	RaycastHit rayhit;
	// Use this for initialization
	void Start () {
		Dir_TargetRotation = Quaternion.Euler(Dir_TargetRotationEuler);
		_levelCompleteHandler = GetComponent<LevelCompleteHandler>();
		_levelCompleteHandler.RegisterFunction();

		transform.rotation = KeyObjCollect.Instance.ActiveDirLight.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = KeyObjCollect.Instance.ActiveDirLight.transform.rotation;
		ray = new Ray(transform.position, transform.rotation * Vector3.forward);

		if(Physics.Raycast(ray.origin,ray.direction, out rayhit, 500.0f))
		{
			if(rayhit.collider.gameObject.name == "MovingBox" && positionCheck(rayhit.collider.transform))
			{
				// CompleteEvent e = new CompleteEvent();
				// e.NextLevelIndex = NextLevelIndex;
				Fire_CameraMove_Event();
				Fire_DirectionLightChange_Event();
			}
		}
		
		if(Input.GetKeyDown(KeyCode.R))
		{
			Fire_RestartLevel_Event();
		}

		//Debug.DrawLine(ray.origin,ray.direction * 500 + ray.origin, Color.green);
	}

	private bool positionCheck(Transform box_Trans)
	{
		Vector3 temp = rayhit.point - box_Trans.position;
		temp.y = 0.0f;
		if(temp.magnitude <= 10.0f)
		{
			return true;
		}
		return false;
	}
	private void SetCamMoveInfo(CameraMoveInfo camMoveInfo)
	{	
		camMoveInfo.startPos = Camera.main.gameObject.transform.position;
		camMoveInfo.endPos = endPos;
		camMoveInfo.lerpTime = lerpTime;
		camMoveInfo.lerpSpeed = 1.0f;
		camMoveInfo.MethodIndex = 0;
	}
	private void SetDirLightRotateInfo(RotationInfo dirRotateInfo)
	{
		dirRotateInfo.startEularAngle = KeyObjCollect.Instance.ActiveDirLight.transform.rotation.eulerAngles;
		dirRotateInfo.endEularAngle = Dir_TargetRotationEuler;
		dirRotateInfo.lerpTime = lerpTime;
		dirRotateInfo.lerpSpeed = 1.0f;
		dirRotateInfo.MethodIndex = 0;
	}

	private void Fire_CameraMove_Event()
	{
		CameraMoveEvent e = new CameraMoveEvent();
		SetCamMoveInfo(e.camMoveInfo);
		EventManager.Instance.FireEvent(e);
	}
	private void Fire_DirectionLightChange_Event()
	{
		changeDirLightEvent q = new changeDirLightEvent();
		SetDirLightRotateInfo(q.dirRotationInfo);
		EventManager.Instance.FireEvent(q);
	}
	private void Fire_RestartLevel_Event()
	{
		RestartEvent e = new RestartEvent();
		EventManager.Instance.FireEvent(e);
	}
}
