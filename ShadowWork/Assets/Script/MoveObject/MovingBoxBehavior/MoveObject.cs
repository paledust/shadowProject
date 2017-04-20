using System.Collections.Generic;
using UnityEngine;
using Kevin_Event;
using CS_Kevin;

public class MoveObject : MonoBehaviour {
	[SerializeField] float DragSpeed;
	public float DRAG_SPEED{get{return DragSpeed;}}
	// 0 degrees is right, 90 degrees is up, 180 is left, 270 is down
	//floats for setting angle degrees for each arc
	private float minAngleForward = 120f; //110
	private float maxAngleForward = 180f; //160

	private float minAngleBack = 300f; //290
	private float maxAngleBack = 360f; //340

	private float minAngleRight = 0f; //340-360
	private float maxAngleRight = 60f; //0-40

	private float minAngleLeft = 180f; //160
	private float maxAngleLeft = 240f; //220

	private float minAngleUp = 60f; //70
	private float maxAngleUp = 120f; //110

	private float minAngleDown = 240f; //250
	private float maxAngleDown = 300f; //290
	[SerializeField] int MoveUnit = 6;
	public FACING_DIRECTIOM DragFace{get; private set;}
	//Snap
	public List<DIRECTION> availableDir{get; private set;}
	public DIRECTION dir{get; private set;}
	public int MOVE_UNITE{get; private set;}
	public MOVESTATE moveState{get; private set;}
	public bool IF_FROZEN{get{return moveState == MOVESTATE.FROZEN;}}
	public bool IF_MOVEABLE{get{return moveState == MOVESTATE.MOVEABLE;}}
	public bool IF_MOVING{get{return moveState == MOVESTATE.MOVING;}}
	public bool IF_PULLING{get{return moveState == MOVESTATE.PULLING;}}
	private Vector3 Nextpos;
	private Vector3 originPos;
	private MoveToTask moveToTask;
	private Task_Manager taskManager = new Task_Manager();

	void Start() {
		availableDir = new List<DIRECTION>();
		DragFace = FACING_DIRECTIOM.EMPTY;
		moveState = MOVESTATE.FROZEN;
		moveToTask = new MoveToTask(transform, transform.position, (int)DragSpeed);
		Nextpos = transform.position;
		SetStatus(MOVESTATE.PENDING);
	}
	void Update() {
		taskManager.Update();

		switch (moveState)
		{
			case MOVESTATE.MOVEABLE:
				MOVEABLE_Update();
				break;
			case MOVESTATE.MOVING:
				MOVING_Update();
				break;
			case MOVESTATE.FROZEN:
				FROZEN_Update();
				break;
			case MOVESTATE.PULLING:
				PULLING_Update();
				break;
			case MOVESTATE.PENDING:
				PENDING_Update();
				break;
			default:
				break;
		}
	}
	void FROZEN_Update(){
		UpdateDir_Event updateDir_Event = new UpdateDir_Event();
		ClearDirection();
		Service.eventManager.FireEvent(updateDir_Event);
		if(Input.GetButtonUp("Fire1")){
			OnMouse_Up();
		}
	}
	void MOVEABLE_Update(){
		moveCheck();
		if(Input.GetButtonUp("Fire1")){
			OnMouse_Up();
		}
	}
	void MOVING_Update(){
		UpdateDir_Event updateDir_Event = new UpdateDir_Event(); 
		
		if(transform.position == Nextpos) {
			originPos = transform.position;
			ClearDirection();	
			moveState = MOVESTATE.FROZEN;
			Service.eventManager.FireEvent(updateDir_Event);
			moveTo(Nextpos);
		}
		else{
			// if(Input.GetButton("Fire1"))
			// 	Mouse_Move();
			// moveTo(Nextpos);
		}
		if((transform.position-Nextpos).magnitude >= MoveUnit){
			Nextpos += (transform.position - Nextpos).normalized * MoveUnit;

			ClearDirection();	
			moveState = MOVESTATE.FROZEN;
			Service.eventManager.FireEvent(updateDir_Event);
		}
		if(Input.GetButtonUp("Fire1")){
			OnMouse_Up();
		}
	}
	void PULLING_Update(){
		if(transform.position == Nextpos){
			SetStatus(MOVESTATE.FROZEN);
		}
	}
	void PENDING_Update(){

	}
	//This Function will add one direction into the availableDir list
	public void AddDirection(DIRECTION direction){
		if(!availableDir.Contains(direction)){
			availableDir.Add(direction);
		}
	}
	//This Function will Clear The Whole availableDir List
	public void ClearDirection(){
		availableDir.Clear();
	}
	//This Function will Set the State of the Box
	public void SetStatus(MOVESTATE m_moveState){
		moveState = m_moveState;
	}
	void OnMouse_Up(){
		if(transform.position != Nextpos){
			moveTo(Nextpos);
		}
		DragFace = FACING_DIRECTIOM.EMPTY;
	}
	private void moveCheck(){
		if(CanMoveThisDirection(DIRECTION.FORWARD) && availableDir.Contains(DIRECTION.FORWARD))
		{
			if(dir != DIRECTION.FORWARD)
			{
				dir = DIRECTION.FORWARD;
			}
			else
			{
				originPos = transform.position;
				Nextpos += Vector3.forward*MoveUnit;
				moveState = MOVESTATE.MOVING;
				moveTo(Nextpos);
			}
		}
		else if(CanMoveThisDirection(DIRECTION.BACK) && availableDir.Contains(DIRECTION.BACK))
		{
			if(dir != DIRECTION.BACK)
			{
				dir = DIRECTION.BACK;
			}
			else
			{
				originPos = transform.position;
				Nextpos += Vector3.back*MoveUnit;
				moveState = MOVESTATE.MOVING;
				moveTo(Nextpos);
			}
		}						
		else if(CanMoveThisDirection(DIRECTION.LEFT) && availableDir.Contains(DIRECTION.LEFT))
		{
			if(dir != DIRECTION.LEFT)
			{
				dir = DIRECTION.LEFT;
			}
			else
			{
				originPos = transform.position;
				Nextpos += Vector3.left*MoveUnit;
				moveState = MOVESTATE.MOVING;
				moveTo(Nextpos);
			}
		}					
		else if(CanMoveThisDirection(DIRECTION.RIGHT) && availableDir.Contains(DIRECTION.RIGHT))
		{
			if(dir != DIRECTION.RIGHT)
			{
				dir = DIRECTION.RIGHT;
			}
			else
			{
				originPos = transform.position;
				Nextpos += Vector3.right*MoveUnit;
				moveState = MOVESTATE.MOVING;
				moveTo(Nextpos);
			}
		}					
		else if(CanMoveThisDirection(DIRECTION.UP) && availableDir.Contains(DIRECTION.UP))
		{
			if(dir != DIRECTION.UP)
			{
				dir = DIRECTION.UP;
			}
			else
			{
				originPos = transform.position;
				Nextpos += Vector3.up*MoveUnit;
				moveState = MOVESTATE.MOVING;
				moveTo(Nextpos);
			}
		}						
		else if(CanMoveThisDirection(DIRECTION.DOWN) && availableDir.Contains(DIRECTION.DOWN))
		{
			if(dir != DIRECTION.DOWN)
			{
				dir = DIRECTION.DOWN;
			}
			else
			{
				originPos = transform.position;
				Nextpos += Vector3.down*MoveUnit;
				moveState = MOVESTATE.MOVING;
				moveTo(Nextpos);
			}
		}
	}
	//This way is actually a little bit clunky, I could have used one function in moveTo, but I use a task System instead.
	//What it did is to add a move task to taskmanager to let the box move.(Why not directly let the box move!!!!, because 
	//I'm over concerned about some other task might be implemented into this system)
	private bool CanMoveThisDirection(DIRECTION checkDirection) {
		switch (checkDirection)
		{
			case DIRECTION.UP:
				return  (Input.GetButton("Fire1") && Mouse_Check() == checkDirection) || Input.GetAxis("Height")>0;
			case DIRECTION.DOWN:
				return  (Input.GetButton("Fire1") && Mouse_Check() == checkDirection) || Input.GetAxis("Height")<0;
			case DIRECTION.LEFT:
				return  (Input.GetButton("Fire1") && Mouse_Check() == checkDirection) || Input.GetAxis("Horizontal")<0;
			case DIRECTION.RIGHT:
				return  (Input.GetButton("Fire1") && Mouse_Check() == checkDirection) || Input.GetAxis("Horizontal")>0;
			case DIRECTION.FORWARD:
				return  (Input.GetButton("Fire1") && Mouse_Check() == checkDirection) || Input.GetAxis("Vertical")>0;
			case DIRECTION.BACK:
				return  (Input.GetButton("Fire1") && Mouse_Check() == checkDirection) || Input.GetAxis("Vertical")<0;
			default:
				return false;
		}
	}
	public void MoveBack(){
		if(Nextpos != originPos){
			Nextpos = originPos;
			moveTo(Nextpos, 3.0f);
		}
	}
	public void moveTo(Vector3 endPos){
		if(moveToTask.ifDetached){
			taskManager.AddTask(moveToTask);
			moveToTask.SetSpeed(DragSpeed);
			moveToTask.SetEndPos(endPos);
		}
		else{
			moveToTask.SetSpeed(DragSpeed);
			moveToTask.SetEndPos(endPos);
		}
		// Service.audioManager.PlaySound("Drag", transform.position);
	}
	public void moveTo(Vector3 endPos, float move_Speed){
		if(moveToTask.ifDetached){
			taskManager.AddTask(moveToTask);
			moveToTask.SetSpeed(move_Speed);
			moveToTask.SetEndPos(endPos);
		}
		else{
			moveToTask.SetSpeed(move_Speed);
			moveToTask.SetEndPos(endPos);
		}
		Service.audioManager.PlaySound("Drag", transform.position);
	}
	//Move Check will Check whether the Box on the ground.
	void MovementCheck(){
		if(transform.position.y < 7.5f)
		{
			transform.position = new Vector3(transform.position.x,7.5f, transform.position.z);
		}
	}
	void Mouse_Move(){
		Vector3 tempVec = Vector3.zero;
		float MouseDir = 0.0f;
		MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		MouseDir = MouseDir*Mathf.Rad2Deg;
		if (MouseDir < 0) {
			MouseDir += 360;
		}
		// if(Mouse_Check() == (DIRECTION.FORWARD) && availableDir.Contains(DIRECTION.FORWARD)) {
		// 	int MoveUnit = (int)Mathf.Min(1.0f, Mathf.Abs(Input.GetAxis("Mouse Y")));
		// 	transform.position += Vector3.forward * MoveUnit * DragSpeed;
		// } else if(Mouse_Check() == (DIRECTION.BACK)&& availableDir.Contains(DIRECTION.BACK)) {
		// 	int MoveUnit = (int)Mathf.Min(1.0f, Mathf.Abs(Input.GetAxis("Mouse Y")));
		// 	transform.position += Vector3.back * MoveUnit * DragSpeed;
		// } else if (Mouse_Check() == (DIRECTION.RIGHT) && availableDir.Contains(DIRECTION.RIGHT)) {
		// 	int MoveUnit = (int)Mathf.Min(1.0f, Mathf.Abs(Input.GetAxis("Mouse X")));
		// 	transform.position += Vector3.right * MoveUnit * DragSpeed;
		// } else if(Mouse_Check() == (DIRECTION.LEFT) && availableDir.Contains(DIRECTION.LEFT)) {
		// 	int MoveUnit = (int)Mathf.Min(1.0f, Mathf.Abs(Input.GetAxis("Mouse X")));
		// 	transform.position += Vector3.left * MoveUnit * DragSpeed;
		// } else if (Mouse_Check() == (DIRECTION.UP) && availableDir.Contains(DIRECTION.UP)) {
		// 	int MoveUnit = (int)Mathf.Min(1.0f, Mathf.Abs(Input.GetAxis("Mouse Y")));
		// 	transform.position += Vector3.up * MoveUnit * DragSpeed;
		// } else if(Mouse_Check() == (DIRECTION.DOWN) && availableDir.Contains(DIRECTION.DOWN)) {
		// 	int MoveUnit = (int)Mathf.Min(1.0f, Mathf.Abs(Input.GetAxis("Mouse Y")));
		// 	transform.position += Vector3.down * MoveUnit * DragSpeed;
		// } else {
		// }

		if(Mouse_Check() == (DIRECTION.FORWARD) && availableDir.Contains(DIRECTION.FORWARD)) {
			tempVec = Vector3.forward;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y") * DragSpeed);
		} else if(Mouse_Check() == (DIRECTION.BACK)&& availableDir.Contains(DIRECTION.BACK)) {
			tempVec = Vector3.forward;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y") * DragSpeed);
		} else if (Mouse_Check() == (DIRECTION.RIGHT) && availableDir.Contains(DIRECTION.RIGHT)) {
			tempVec = Vector3.right;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse X") * DragSpeed);
		} else if(Mouse_Check() == (DIRECTION.LEFT) && availableDir.Contains(DIRECTION.LEFT)) {
			tempVec = Vector3.right;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse X") * DragSpeed);
		} else if (Mouse_Check() == (DIRECTION.UP) && availableDir.Contains(DIRECTION.UP)) {
			tempVec = Vector3.up;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y") * DragSpeed);
		} else if(Mouse_Check() == (DIRECTION.DOWN) && availableDir.Contains(DIRECTION.DOWN)) {
			tempVec = Vector3.up;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y") * DragSpeed);
		} else {
		}
	}
	DIRECTION Mouse_Check(){
		Vector2 mouseVec = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		if(mouseVec.magnitude >= 7.0f){
			Vector3 tempVec = Vector3.zero;
			float MouseDir = 0.0f;
			MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
			MouseDir = MouseDir*Mathf.Rad2Deg;
			if (MouseDir < 0) {
				MouseDir += 360;
			}
			if(MouseDir > minAngleForward && MouseDir <= maxAngleForward && (DragFace == (FACING_DIRECTIOM.Y))) {
				return DIRECTION.FORWARD;
			} else if(MouseDir > minAngleBack && MouseDir <= maxAngleBack && (DragFace == (FACING_DIRECTIOM.Y))) {
				return DIRECTION.BACK;
			} else if (MouseDir > minAngleRight && MouseDir <= maxAngleRight && (DragFace == (FACING_DIRECTIOM.Z) || DragFace == (FACING_DIRECTIOM.Y))) {
				return DIRECTION.RIGHT;
			} else if(MouseDir > minAngleLeft && MouseDir <= maxAngleLeft && (DragFace == (FACING_DIRECTIOM.Z) || DragFace == (FACING_DIRECTIOM.Y))) {
				return DIRECTION.LEFT;
			} else if ((MouseDir > minAngleUp && MouseDir <= maxAngleUp) && (DragFace == (FACING_DIRECTIOM.Z))) {
				return DIRECTION.UP;
			} else if(MouseDir > minAngleDown && MouseDir <= maxAngleDown && (DragFace == (FACING_DIRECTIOM.Z))) {
				return DIRECTION.DOWN;
			} else {
				return DIRECTION.EMPTY;
			}
		}
		else
			return DIRECTION.EMPTY;
	}
	float MouseSpeed(DIRECTION DragDir){
		switch (DragDir)
		{
			case DIRECTION.FORWARD:
				return Input.GetAxis("Mouse X")*Mathf.Sin(Mathf.Deg2Rad * maxAngleForward) + Input.GetAxis("Mouse Y")*Mathf.Cos(Mathf.Deg2Rad * maxAngleForward);			
			case DIRECTION.BACK:
				return Input.GetAxis("Mouse X")*Mathf.Sin(Mathf.Deg2Rad * maxAngleBack) + Input.GetAxis("Mouse Y")*Mathf.Cos(Mathf.Deg2Rad * maxAngleBack);			
			case DIRECTION.UP:
				return Input.GetAxis("Mouse X")*Mathf.Sin(Mathf.Deg2Rad * maxAngleUp) + Input.GetAxis("Mouse Y")*Mathf.Cos(Mathf.Deg2Rad * maxAngleUp);			
			case DIRECTION.DOWN:
				return Input.GetAxis("Mouse X")*Mathf.Sin(Mathf.Deg2Rad * maxAngleDown) + Input.GetAxis("Mouse Y")*Mathf.Cos(Mathf.Deg2Rad * maxAngleDown);			
			case DIRECTION.LEFT:
				return Input.GetAxis("Mouse X")*Mathf.Sin(Mathf.Deg2Rad * maxAngleLeft) + Input.GetAxis("Mouse Y")*Mathf.Cos(Mathf.Deg2Rad * maxAngleLeft);							
			case DIRECTION.RIGHT:
				return Input.GetAxis("Mouse X")*Mathf.Sin(Mathf.Deg2Rad * maxAngleRight) + Input.GetAxis("Mouse Y")*Mathf.Cos(Mathf.Deg2Rad * maxAngleRight);					
			default:
				return 0.0f;

		}
	}
	public void Set_DragFace(FACING_DIRECTIOM m_DragFace){
		if(DragFace != (m_DragFace) && DragFace == FACING_DIRECTIOM.EMPTY)
			DragFace = m_DragFace;
	}
	//This is a MoveTask, it has its own Update Function which will be called in Task Manager that I create within this class
	//Task Manager will handle it for us, What it did is to move one object to a specific place
}
public class MoveToTask:Task {
	private Transform moveTrans;
	private Vector3 startPos;
	private Vector3 endPos;
	private float timer;
	private float speed;
	public MoveToTask(Transform m_Trans, Vector3 m_endPos, int m_speed){
		moveTrans = m_Trans;
		startPos = moveTrans.position;
		endPos = m_endPos;
		speed = m_speed;
	}
	protected override void Init(){
		timer = 0.0f;
		startPos = moveTrans.position;
	}
	internal override void TUpdate(){
		timer += Time.deltaTime;
		moveTrans.position = Vector3.Lerp(startPos, endPos,timer * speed);
		if(moveTrans.position == endPos){
			SetStatus(TaskStatus.Success);
		}
	}
	public void SetEndPos(Vector3 m_endPos){
		startPos = moveTrans.position;
		endPos = m_endPos;
	}
	public void SetSpeed(float m_Speed){
		speed = Mathf.Min(5.0f, m_Speed);
	}
	public Vector3 GET_ENDPOS(){
		return endPos;
	}
}
public class SetBoxStatus: Task{
	MOVESTATE targetState;
	MoveObject Context;
	
	public SetBoxStatus(MOVESTATE _tarState, MoveObject _Context){
		targetState = _tarState;
		Context = _Context;
	}
	protected override void Init(){
		Context.SetStatus(targetState);
		SetStatus(TaskStatus.Success);
	}
}