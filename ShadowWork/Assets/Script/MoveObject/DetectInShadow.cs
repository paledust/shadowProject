using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class DetectInShadow : MonoBehaviour {
	//Contain all the ShadowTrail this ShadowWheel Get.
	public DirectionOption directionOPT;
	public List<ShadowTrail> shadowTrailList;
	[SerializeField] Transform DirectionalLight;

	RaycastHit[] rayHits;
	Ray ray;
	
	void Start()
	{
		shadowTrailList = new List<ShadowTrail>();
	}

	// Update Function Start-----------------------------------------------------------
	void Update () {
		ray = new Ray(transform.position, DirectionalLight.rotation * Vector3.back);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f);

		//Check and change player's possible moving Direction only when the object is Frozen, 
		//and then change its state to movable.
		// if(GetComponentInParent<ObjectStateManager>().objectState.ifFrozen)
		{
			ClearShadowTrailList();
			foreach (RaycastHit _rayhit in rayHits)
			{
				//Debug.Log(_rayhit.collider.gameObject.name);
				if(_rayhit.collider.gameObject.GetComponent<ShadowTrail>())
				{
					ShadowTrail _shadowTrail = _rayhit.collider.gameObject.GetComponent<ShadowTrail>();
					AddShadowTrail(_shadowTrail);
					GetComponentInParent<DragObjectScript>().SetDirection(CalculateDir(_shadowTrail.directionOption),true);
				}
			}
			//Set the State to movable State
			if(!GetComponentInParent<ObjectStateManager>().objectState.ifMoveable && rayHits.Length>0)
				GetComponentInParent<ObjectStateManager>().objectState.SetStatus(MovingState.Moveable);
		}

		Debug.DrawLine(ray.origin,ray.direction * 500 + ray.origin, Color.white);
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
	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! The calculation condition Need to be fixed, right now, it didn't consider light direction.
	DirectionOption CalculateDir(DirectionOption _direction)
	{
		switch (directionOPT)
		{
			case DirectionOption.x:
				if(_direction == DirectionOption.x)
					_direction = DirectionOption.y;
				break;
			case DirectionOption.y:
				if(_direction == DirectionOption.y)
					_direction = DirectionOption.z;
				break;
			case DirectionOption.z:
				if(_direction == DirectionOption.z)
					_direction = DirectionOption.y;
				break;
			default:
				break;
		}

		return _direction;
	}
}
