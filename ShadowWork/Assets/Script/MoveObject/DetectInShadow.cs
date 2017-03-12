using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class DetectInShadow : MonoBehaviour {
	//Contain all the ShadowTrail this ShadowWheel Get.
	public List<ShadowTrail> shadowTrailList;
	RaycastHit[] rayHits;
	Ray ray;
	
	void Start()
	{
		shadowTrailList = new List<ShadowTrail>();
	}
	void Update () {
		ray = new Ray(transform.position, transform.rotation * Vector3.up);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,100.0f);
		
		//Check and change player's possible moving Direction only when the object is Frozen, 
		//and then change its state to movable.
		if(GetComponentInParent<ObjectStateManager>().objectState.ifFrozen)
		{
			ClearShadowTrailList();
			foreach (RaycastHit _rayhit in rayHits)
			{
				if(_rayhit.collider.gameObject.GetComponent<ShadowTrail>())
				{
					ShadowTrail _shadowTrail = _rayhit.collider.gameObject.GetComponent<ShadowTrail>();
					AddShadowTrail(_shadowTrail);
					GetComponentInParent<DragObjectScript>().SetDirection(_shadowTrail.directionOption,true);
				}
			}
			//Set the State to movable State
			if(!GetComponentInParent<ObjectStateManager>().objectState.ifMoveable)
				GetComponentInParent<ObjectStateManager>().objectState.SetStatus(MovingState.Moveable);
		}

		Debug.DrawLine(ray.origin,ray.direction * 100 + ray.origin, Color.white);
	}

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
			GetComponentInParent<DragObjectScript>().SetDirection(_shadowTrail.directionOption,false);
		}
		shadowTrailList.Clear();
	}
}
