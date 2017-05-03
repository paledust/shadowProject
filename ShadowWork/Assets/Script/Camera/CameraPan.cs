using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class CameraPan : MonoBehaviour {
	[SerializeField] float Subtle_Move;
	[SerializeField] float Subtle_Pan;
	private Transform HeroBox;
	private Quaternion OriginRotation;
	private Quaternion tempRotation;

	// Update is called once per frame
	void Start(){
		HeroBox = GameObject.Find("MovingBox").transform;
	}
	void FixedUpdate () {
		RotateControl(GET_CAMERA_ANGLE().x, GET_CAMERA_ANGLE().y, 2.0f);
		CameraFollow();
	}
	void RotateControl(float AngleX, float AngleY, float _Speed){
		Quaternion CamRotationAngle = FromAxisToRotate(AngleX, Vector3.up) * FromAxisToRotate(AngleY, Vector3.right);
		tempRotation = Quaternion.Lerp(tempRotation, OriginRotation * CamRotationAngle, Time.deltaTime * _Speed);
		if(Quaternion.Angle(tempRotation, CamRotationAngle) <= 0.01f)
			tempRotation = CamRotationAngle;

		transform.rotation = Quaternion.Euler(tempRotation.eulerAngles.x, tempRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
	}
	void CameraFollow(){
		MoveObject moveOBJ = HeroBox.GetComponent<MoveObject>();
		switch (moveOBJ.dir)
		{
			case DIRECTION.LEFT:
				transform.position += Vector3.left * Subtle_Move;
				return;
			case DIRECTION.RIGHT:
				transform.position += Vector3.right * Subtle_Move;
				return;
			case DIRECTION.UP:
				transform.position += Vector3.up * Subtle_Move;
				return;
			case DIRECTION.DOWN:
				transform.position += Vector3.down * Subtle_Move;
				return;
			case DIRECTION.FORWARD:
				transform.position += Vector3.forward * Subtle_Move;
				return;
			case DIRECTION.BACK:
				transform.position += Vector3.back * Subtle_Move;
				return;
			default:
				return;
		}
	}

	Vector2 GET_CAMERA_ANGLE(){
		MoveObject moveOBJ = HeroBox.GetComponent<MoveObject>();
		switch (moveOBJ.dir)
		{
			case DIRECTION.LEFT:
				return new Vector2(-1,0) * Subtle_Pan;
			case DIRECTION.RIGHT:
				return new Vector2(1,0) * Subtle_Pan;
			case DIRECTION.UP:
				return new Vector2(0,1) * Subtle_Pan;
			case DIRECTION.DOWN:
				return new Vector2(0,-1) * Subtle_Pan;
			case DIRECTION.FORWARD:
				return new Vector2(-1,-1).normalized * Subtle_Pan;
			case DIRECTION.BACK:
				return new Vector2(1,1).normalized * Subtle_Pan;
			default:
				return Vector2.zero;
		}
	}

	protected Quaternion FromAxisToRotate(float AngleAxis, Vector3 RotateAxis){
		return Quaternion.AngleAxis(AngleAxis,RotateAxis);
	}

	public void SET_OriRotation(Quaternion _OriRotation){
		OriginRotation = _OriRotation;
		tempRotation = OriginRotation;
	}
}
