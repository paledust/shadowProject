using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using CS_Kevin;
using Kevin_Event;

public class DetectInShadow : MonoBehaviour {
#region Parameter_Zone
	protected List<DIRECTION> _directions;
	protected bool ifDrag = false;
	protected bool IF_Light_On = false;
	protected bool IFEnd = false;
	protected MoveObject moveObject;
	protected RaycastHit[] rayHits;
	protected Ray ray;
	protected RaycastHit rayhit;
	[SerializeField] protected Color DeactiveColor = Color.black;
	[SerializeField] protected Color ActivateColor = Color.blue;
	[SerializeField] protected LayerMask layerMask;
	public FACING_DIRECTIOM facingDir;
	public bool IfActive{get{return rayHits.Length > 0;}}
	public List<DIRECTION> GET_Direction{get{return _directions;}}
#endregion Parameter_Zone

#region Function_Zone
	protected virtual void Start() {
		_directions = new List<DIRECTION>();
		Service.eventManager.Register<UpdateDir_Event>(UpdateDir_Handler);
		moveObject = GetComponentInParent<MoveObject>();
		ray = new Ray(transform.position, Service.ActiveDirLight.transform.rotation * Vector3.back);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f,layerMask);

		Service.eventManager.Register<EndGame_Event>(End_Fade);
		Service.eventManager.Register<PullBox_Event>(StartChangeDot);
	}
	protected virtual void Update(){
		ray = new Ray(transform.position, Service.ActiveDirLight.transform.rotation * Vector3.back);
		rayHits = Physics.RaycastAll(ray.origin,ray.direction,500.0f,layerMask);
		if(!IFEnd){
			if(rayHits.Length > 0){
				GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color,
																		ActivateColor,Time.deltaTime * 10.0f);
			}
			else{
				GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color,
																		DeactiveColor,Time.deltaTime * 10.0f);
			}
		}
		// FACE_ACTIVE();
	}
	void StartChangeDot(Kevin_Event.Event e){
		IFEnd = true;
		StartCoroutine(ChangeDot());
	}
	IEnumerator ChangeDot(){
		Material tempRender = GetComponent<Renderer>().material;
		Color tempColor = tempRender.color;
		for(float timer = 0.0f; timer < 1.0f; timer += Time.deltaTime){
			tempRender.color = Color.Lerp(tempColor, Color.white, timer);
			yield return null;
		}
		yield return null;
	}

	protected virtual void FACE_ACTIVE(){
		Camera.main.GetComponent<CustomCursor>().Cursor_Raycast(out rayhit);
		if(rayhit.collider != null && rayhit.collider.gameObject == gameObject && Input.GetButtonDown("Fire1")){
			ifDrag = true;		
		} 	
		if(ifDrag){
			GetComponentInParent<MoveObject>().Set_DragFace(facingDir);	
		}
		if(Input.GetButtonUp("Fire1")){
			ifDrag = false;
		}
	}
	protected virtual void UpdateDir_Handler(Kevin_Event.Event e) {
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
	protected void CLEAR_DIRECTION(){_directions.Clear();}
	protected DIRECTION CalculateDirection(DIRECTION direction) {
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
	protected bool IF_IN_TRAIL() {
		return (Vector3.Dot(GET_FACE_VECTOR(),ray.direction) >= 0.05f);
	}
	protected Vector3 GET_FACE_VECTOR(){
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
	protected void SetDirection(){
		if(_directions.Contains(DIRECTION.UP)){moveObject.AddDirection(CalculateDirection(DIRECTION.UP));} 

		if(_directions.Contains(DIRECTION.DOWN)){moveObject.AddDirection(CalculateDirection(DIRECTION.DOWN));} 

		if(_directions.Contains(DIRECTION.LEFT)){moveObject.AddDirection(CalculateDirection(DIRECTION.LEFT));} 

		if(_directions.Contains(DIRECTION.RIGHT)){moveObject.AddDirection(CalculateDirection(DIRECTION.RIGHT));} 

		if(_directions.Contains(DIRECTION.FORWARD)){moveObject.AddDirection(CalculateDirection(DIRECTION.FORWARD));} 

		if(_directions.Contains(DIRECTION.BACK)){moveObject.AddDirection(CalculateDirection(DIRECTION.BACK));} 

	}
	protected void End_Fade(Kevin_Event.Event e){
		IFEnd = true;
		StartCoroutine(Fade());
	}
	protected IEnumerator Fade(){
		yield return new WaitForSeconds(1.0f);
		Color startColor = GetComponent<Renderer>().material.color;
		for(float timer = 0.0f; timer < 2.0f; timer += Time.deltaTime){
			GetComponent<Renderer>().material.color = Color.Lerp(startColor, Color.black,Easing.BackEaseInOut(timer/2));
			yield return null;
		}
	}
	public bool IF_CONTAIN(DIRECTION direction){
		return _directions.Contains(direction);
	}
#endregion 
}
