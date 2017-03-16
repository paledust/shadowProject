using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class DetectInShadow : MonoBehaviour {
	//Contain all the ShadowTrail this ShadowWheel Get.
	public DirectionOption directionOPT;
	public List<ShadowTrail> shadowTrailList;
	[SerializeField] Color DeactiveColor = Color.black;
	[SerializeField] Color ActivateColor = Color.blue;

	RaycastHit[] rayHits;
	Ray ray;
	
	void Start()
	{
		shadowTrailList = new List<ShadowTrail>();
	}

	// Update Function Start-----------------------------------------------------------
	void Update () {
		ray = new Ray(transform.position, KeyObjCollect.Instance.ActiveDirLight.transform.rotation * Vector3.back);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f);

		//Check and change player's possible moving Direction only when the object is Frozen, 
		//and then change its state to movable.
		// if(GetComponentInParent<ObjectStateManager>().objectState.ifFrozen)
		if(CheckIfInTrail())
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
					GetComponentInParent<DragObjectScript>().SetDirection(CalculateDir(_shadowTrail.directionOption),true);
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
	//Update Function End---------------------------------------------------------------
	

	//Add ShadowTrail into the list, if already added then skip it.
	void AddShadowTrail(ShadowTrail _shadowtrail)
	{
		if(!shadowTrailList.Contains(_shadowtrail))
			shadowTrailList.Add(_shadowtrail);
	}
	
	//Clear all the avaiable Direction and Clear all the shadowTraillist
	void ClearShadowTrailList()
	{
		foreach(ShadowTrail _shadowTrail in shadowTrailList)
		{
			GetComponentInParent<DragObjectScript>().SetDirection(CalculateDir(_shadowTrail.directionOption),false);
		}
		shadowTrailList.Clear();
	}

	//Based on the ShadowTrail, it will calculate the final available direction for ShadowBox, but it's actually wrong!!!!!!!!!!!!!!!!!!
	DirectionOption CalculateDir(DirectionOption _direction)
	{
		switch (directionOPT)
		{
			case DirectionOption.x:
				if(_direction == DirectionOption.x)
					_direction = DirectionOption.y;

				if(_direction == DirectionOption.right)
					_direction = DirectionOption.up;
				if(_direction == DirectionOption.left)
					_direction = DirectionOption.down;
				break;
			case DirectionOption.y:
				if(_direction == DirectionOption.y)
					_direction = DirectionOption.z;

				if(_direction == DirectionOption.up)
					_direction = DirectionOption.forward;
				if(_direction == DirectionOption.down)
					_direction = DirectionOption.back;
				break;
			case DirectionOption.z:
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

	//Check whether it's shadow trail or it's just completely inside the shadow
	bool CheckIfInTrail()
	{
		Vector3 DetectDirection = Vector3.zero;
		switch (directionOPT)
		{
			case DirectionOption.x:
				DetectDirection = Vector3.left;
				break;
			case DirectionOption.y:
				DetectDirection = Vector3.up;
				break;
			case DirectionOption.z:
				DetectDirection = Vector3.back;
				break;
			default:
				DetectDirection = Vector3.zero;
				break;
		}
		//Debug.Log(Vector3.Dot(DetectDirection,ray.direction));
		return (Vector3.Dot(DetectDirection,ray.direction) >= 0.05f);
	}
}
