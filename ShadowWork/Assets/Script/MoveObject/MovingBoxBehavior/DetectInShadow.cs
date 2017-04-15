﻿using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class DetectInShadow : MonoBehaviour {
	public FACING_DIRECTIOM facingDir;
	public List<DIRECTION> directions;
	[SerializeField] Color DeactiveColor = Color.black;
	[SerializeField] Color ActivateColor = Color.blue;
	MoveObject moveObject;
	RaycastHit[] rayHits;
	Ray ray;
	RaycastHit rayhit;
	private bool ifDrag = false;
	
	void Start() {
		//Register UpdateDir_Handler Function to UpdateDir_Event
		//Whenever UpdateDir_Event Fired, UpdateDir_Handler Function will be Called once
		Service.eventManager.Register<UpdateDir_Event>(UpdateDir_Handler);
		moveObject = GetComponentInParent<MoveObject>();
	}
	void Update(){
		ray = new Ray(transform.position, Service.ActiveDirLight.transform.rotation * Vector3.back);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f);
		if(rayHits.Length>0){
			GetComponent<Renderer>().material.color = ActivateColor;
		}
		else
			GetComponent<Renderer>().material.color = DeactiveColor;
		FACE_ACTIVE();
	}
	void FACE_ACTIVE(){
		Camera.main.GetComponent<CustomCursor>().Cursor_Raycast(out rayhit);
		if(rayhit.collider != null && rayhit.collider.gameObject == gameObject && Input.GetButtonDown("Fire1")){
			ifDrag = true;		
		} 	
		if(ifDrag){
			// Vector3 cursorPos = Camera.main.WorldToScreenPoint(transform.position);
			// Camera.main.GetComponent<CustomLockCursor>().SetCursor(cursorPos);
			// Debug.Log("Hello");
			GetComponentInParent<MoveObject>().Set_DragFace(facingDir);	
		}
		if(Input.GetButtonUp("Fire1")){
			ifDrag = false;
		}
	}

	//This Function will only be called once when UpdateDir_Event is fired once somewhere!
	void UpdateDir_Handler(Event e) {
		//Create a Ray to Detect whether it's in shadow and in the shadow of What kind of Thing
		//FOR EXAMPLE: Some Thing allow Box move along X Direction, Another Thing allow Box move along Y Direction
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f);
		
		//Before Add any DIRECTION into the Avaliable List, First Clear All the old Direction it stores.
		CLEAR_DIRECTION();
		
		//For each Thing it casted, get the information from its ShadowTrail to find out what direction it allow
		foreach (RaycastHit _rayhit in rayHits){
			if(_rayhit.collider.gameObject.GetComponent<AllowDirection>()){
				AllowDirection _AllowDir = _rayhit.collider.gameObject.GetComponent<AllowDirection>();
				//Add This Direction into the DirectionList
				directions.AddRange(_AllowDir.GET_DIRECTION_LIST());
			}
		}
		//If the Length is > 0, that means it hit something, meaning the dot is in ShadowTrail, Thus we need to Update The Direction
		if(rayHits.Length>0){
			//Update The Direction
			SetDirection();
			//And meanwhile, since the box has Avaliable Direction, we should set it to Moveable.
			if(moveObject.IF_FROZEN)
				moveObject.SetStatus(MOVESTATE.MOVEABLE);
		}
	}

	//Clear The list Of Directions
	void CLEAR_DIRECTION(){directions.Clear();}

	//Because of the projection, Some direction need to be recalculate
	//FOR EXAMPLE: Imaging a Vertical Bar casting shadow on the ground, if the Dot facing Y direction,
	//And the eular Angle of Light is (45,0,0), then if dots facing UP, it will become Z direction,
	//But if dots facing BACK, it will become Y direction.
	DIRECTION CalculateDirection(DIRECTION direction) {
		DIRECTION NewDirection = direction;
		switch (facingDir)
		{
			case FACING_DIRECTIOM.X:
				if(NewDirection == DIRECTION.RIGHT)
					NewDirection = DIRECTION.UP;
				else if(NewDirection == DIRECTION.LEFT)
					NewDirection = DIRECTION.DOWN;
				break;
			case FACING_DIRECTIOM.Y:
				if(NewDirection == DIRECTION.UP)
					NewDirection = DIRECTION.FORWARD;
				else if(NewDirection == DIRECTION.DOWN)
					NewDirection = DIRECTION.BACK;
				break;
			case FACING_DIRECTIOM.Z:
				if(NewDirection == DIRECTION.FORWARD)
					NewDirection = DIRECTION.UP;
				else if(NewDirection == DIRECTION.BACK)
					NewDirection = DIRECTION.DOWN;
				else if(NewDirection == DIRECTION.UP)
				{
					NewDirection = DIRECTION.UP;
				}
				break;
			default:
				break;
		}
		return NewDirection;
	}

	//Detect Whether this dot is in "Shadow Trail" Or the Whole Face is in Shadow
	//It's actually not quite helpful right now, 
	bool IF_IN_TRAIL() {
		return (Vector3.Dot(GET_FACE_VECTOR(),ray.direction) >= 0.05f);
	}

	//This Function will Transfer The Direction of the dot facing from DIRECTION type into Vector3 Type
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

	//This Function will Set the Box Avaliable Direction
	void SetDirection(){
		if(directions.Contains(DIRECTION.UP)){moveObject.AddDirection(CalculateDirection(DIRECTION.UP));} 

		if(directions.Contains(DIRECTION.DOWN)){moveObject.AddDirection(CalculateDirection(DIRECTION.DOWN));} 

		if(directions.Contains(DIRECTION.LEFT)){moveObject.AddDirection(CalculateDirection(DIRECTION.LEFT));} 

		if(directions.Contains(DIRECTION.RIGHT)){moveObject.AddDirection(CalculateDirection(DIRECTION.RIGHT));} 

		if(directions.Contains(DIRECTION.FORWARD)){moveObject.AddDirection(CalculateDirection(DIRECTION.FORWARD));} 

		if(directions.Contains(DIRECTION.BACK)){moveObject.AddDirection(CalculateDirection(DIRECTION.BACK));} 

	}
}
