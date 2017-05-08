using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using CS_Kevin;
using Kevin_Event;
public class DetectInShadow_Indie : DetectInShadow {
	// Use this for initialization
	protected override void Start () {
		_directions = new List<DIRECTION>();
		Service.eventManager.Register<UpdateDir_Event>(UpdateDir_Handler);
		moveObject = GetComponentInParent<MoveObject>();
		ray = new Ray(transform.position, Service.ActiveDirLight.transform.rotation * Vector3.back);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f,layerMask);

		Service.eventManager.Register<EndGame_Event>(End_Fade);
	}
	
	// Update is called once per frame
	protected override void Update () {
		
	}

	protected override void UpdateDir_Handler(Kevin_Event.Event e){
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f);
		
		CLEAR_DIRECTION();
		foreach (RaycastHit _rayhit in rayHits){
			if(_rayhit.collider.gameObject.GetComponent<AllowDirection>()){
				AllowDirection _AllowDir = _rayhit.collider.gameObject.GetComponent<AllowDirection>();
				_directions.AddRange(_AllowDir.GET_DIRECTION_LIST());
			}
		}
		if(rayHits.Length>0){
			SetDirection();
			if(!moveObject.IF_MOVING){
				moveObject.SetStatus(MOVESTATE.MOVEABLE);
			}
		}
	}
}
