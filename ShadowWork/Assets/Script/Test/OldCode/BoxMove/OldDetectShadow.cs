using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class OldDetectShadow : MonoBehaviour {
	//Contain all the ShadowTrail this ShadowWheel Get.
	public enum FACING_DIRECTIOM{
		X,
		Y,
		Z
	};
	public FACING_DIRECTIOM facingDir;
	public List<DIRECTION> directions;
	[SerializeField] Color DeactiveColor = Color.black;
	[SerializeField] Color ActivateColor = Color.blue;
	OldMoveObject moveObject;
	RaycastHit[] rayHits;
	Ray ray;
	public DirectionOption directionOPT;
	public List<ShadowTrail> shadowTrailList;
	
	void Start() {
		moveObject = GetComponentInParent<OldMoveObject>();
	}
	void Update () {
		ray = new Ray(transform.position, Service.ActiveDirLight.transform.rotation * Vector3.back);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f);

		if(IF_IN_TRAIL())
		{
			ClearShadowTrailList();
			foreach (RaycastHit _rayhit in rayHits)
			{
				//Debug.Log(_rayhit.collider.gameObject.name);
				if(_rayhit.collider.gameObject.GetComponent<ShadowTrail>())
				{
					GetComponent<Renderer>().material.color = ActivateColor;
					ShadowTrail _shadowTrail = _rayhit.collider.gameObject.GetComponent<ShadowTrail>();
					AddShadowTrail(_shadowTrail);
					if(_shadowTrail.ifEnd)
					{
						if(PoseCheck(_rayhit))
							moveObject.SetDirection(CalculateDir(_shadowTrail.directionOption),true);
						else
							moveObject.SetDirection(CalculateDir(_shadowTrail.getUpperDirection()),true);
					}
					else
					{
						moveObject.SetDirection(CalculateDir(_shadowTrail.directionOption),true);	
					}
				}
			}
			//Set the State to movable State
			if(rayHits.Length>0)
			{
				if(GetComponentInParent<ObjectStateManager>().objectState.ifFrozen)
					GetComponentInParent<ObjectStateManager>().objectState.SetStatus(MovingState.Moveable);
			}
			else
				GetComponent<Renderer>().material.color = DeactiveColor;
		}
		else
		{
			GetComponent<Renderer>().material.color = DeactiveColor;
		}
		//Debug.DrawLine(ray.origin,ray.direction * 500 + ray.origin, Color.white);
	}
	//Add ShadowTrail into the list, if already added then skip it.
	void AddShadowTrail(ShadowTrail _shadowtrail)
	{
		if(!shadowTrailList.Contains(_shadowtrail))
			shadowTrailList.Add(_shadowtrail);
	}
	bool PoseCheck(RaycastHit rayhit)
	{
		return (rayhit.point - rayhit.collider.transform.position).magnitude<=0.1f;
	}
	//Clear all the avaiable Direction and Clear all the shadowTraillist
	void ClearShadowTrailList()
	{
		foreach(ShadowTrail _shadowTrail in shadowTrailList)
		{
			moveObject.SetDirection(CalculateDir(_shadowTrail.directionOption),false);
		}
		shadowTrailList.Clear();
	}
	//Based on the ShadowTrail, it will calculate the final available direction for ShadowBox, but it's actually wrong!!!!!!!!!!!!!!!!!!
	DirectionOption CalculateDir(DirectionOption _direction)
	{
		switch (facingDir)
		{
			case FACING_DIRECTIOM.X:
				if(_direction == DirectionOption.x)
					_direction = DirectionOption.y;

				if(_direction == DirectionOption.right)
					_direction = DirectionOption.up;
				if(_direction == DirectionOption.left)
					_direction = DirectionOption.down;
				break;
			case FACING_DIRECTIOM.Y:
				if(_direction == DirectionOption.y)
					_direction = DirectionOption.z;

				if(_direction == DirectionOption.up)
					_direction = DirectionOption.forward;
				if(_direction == DirectionOption.down)
					_direction = DirectionOption.back;
				break;
			case FACING_DIRECTIOM.Z:
				if(_direction == DirectionOption.z)
					_direction = DirectionOption.y;

				if(_direction == DirectionOption.forward)
					_direction = DirectionOption.up;
				if(_direction == DirectionOption.back)
					_direction = DirectionOption.down;
				break;
			default:
				break;
		}

		return _direction;
	}

	bool IF_IN_TRAIL() {
		return (Vector3.Dot(GET_FACE_VECTOR(),ray.direction) >= 0.05f);
	}
	Vector3 GET_FACE_VECTOR(){
		Vector3 tempVec = Vector3.zero;
		switch (facingDir)
		{
			case FACING_DIRECTIOM.X:
				return Vector3.right;
			case FACING_DIRECTIOM.Y:
				return Vector3.up;
			case FACING_DIRECTIOM.Z:
				return Vector3.forward;
			default:
				return Vector3.zero;
		}
	}
}
