using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLevelManage : MonoBehaviour {
	public Transform ActiveDirectionalLight;
	public Vector3 startPos;
	public Vector3 endPos;
	public float lerpTime;
	public int NextLevelIndex;
	public Vector3 DirRotationEuler;
	private Quaternion DirRotation;
	LevelCompleteHandler _levelCompleteHandler;
	Ray ray;
	RaycastHit rayhit;
	// Use this for initialization
	void Start () {
		DirRotation = Quaternion.Euler(DirRotationEuler);
		_levelCompleteHandler = GetComponent<LevelCompleteHandler>();
		_levelCompleteHandler.RegisterFunction();

		transform.rotation = ActiveDirectionalLight.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = ActiveDirectionalLight.rotation;
		ray = new Ray(transform.position, transform.rotation * Vector3.forward);

		if(Physics.Raycast(ray.origin,ray.direction, out rayhit, 500.0f))
		{
			if(rayhit.collider.gameObject.name == "MovingBox" && positionCheck(rayhit.collider.transform))
			{
				// CompleteEvent e = new CompleteEvent();
				// e.NextLevelIndex = NextLevelIndex;
				startPos = Camera.main.gameObject.transform.position;
				CameraMoveEvent e = new CameraMoveEvent();
				SetCamMoveInfo(e.camMoveInfo);
				EventManager.Instance.FireEvent(e);

				changeDirLightEvent q = new changeDirLightEvent();
				q.dirLightTransform = ActiveDirectionalLight;
				q.rotation = DirRotation;
				EventManager.Instance.FireEvent(q);
			}
		}

		Debug.DrawLine(ray.origin,ray.direction * 500 + ray.origin, Color.green);

		if(Input.GetKeyDown(KeyCode.R))
		{
			RestartEvent e = new RestartEvent();
			EventManager.Instance.FireEvent(e);
		}
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
		camMoveInfo.startPos = startPos;
		camMoveInfo.endPos = endPos;
		camMoveInfo.lerpTime = lerpTime;
		camMoveInfo.lerpSpeed = 0.0f;
		camMoveInfo.MethodIndex = 0;
	}
}
