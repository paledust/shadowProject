using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class CameraPan : MonoBehaviour {
	[SerializeField] float SubtleMove;
	[SerializeField] Transform HeroBox;
	private Quaternion OriginRotation;
	private Quaternion tempRotation;

	// Update is called once per frame
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
				transform.position += Vector3.left * SubtleMove;
				return;
			case DIRECTION.RIGHT:
				transform.position += Vector3.right * SubtleMove;
				return;
			case DIRECTION.UP:
				transform.position += Vector3.up * SubtleMove;
				return;
			case DIRECTION.DOWN:
				transform.position += Vector3.down * SubtleMove;
				return;
			case DIRECTION.FORWARD:
				transform.position += Vector3.forward * SubtleMove;
				return;
			case DIRECTION.BACK:
				transform.position += Vector3.back * SubtleMove;
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
				return new Vector2(-1,0);
			case DIRECTION.RIGHT:
				return new Vector2(1,0);
			case DIRECTION.UP:
				return new Vector2(0,1);
			case DIRECTION.DOWN:
				return new Vector2(0,-1);
			case DIRECTION.FORWARD:
				return new Vector2(-1,-1).normalized;
			case DIRECTION.BACK:
				return new Vector2(1,1).normalized;
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
