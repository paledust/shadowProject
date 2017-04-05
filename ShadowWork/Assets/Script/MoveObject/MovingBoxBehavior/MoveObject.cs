using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class MoveObject : MonoBehaviour {
	public float DragSpeed = 5f;

	// 0 degrees is right, 90 degrees is up, 180 is left, 270 is down
	//floats for setting angle degrees for each arc
	public float minAngleForward = 120f; //110
	public float maxAngleForward = 180f; //160

	public float minAngleBack = 300f; //290
	public float maxAngleBack = 360f; //340

	public float minAngleRight = 0f; //340-360
	public float maxAngleRight = 60f; //0-40

	public float minAngleLeft = 180f; //160
	public float maxAngleLeft = 240f; //220

	public float minAngleUp = 60f; //70
	public float maxAngleUp = 120f; //110

	public float minAngleDown = 240f; //250
	public float maxAngleDown = 300f; //290

	public List<DIRECTION> availableDir;
	public DIRECTION dir;
	public int MoveUnit = 6;
	public MOVESTATE moveState;
	public bool IF_FROZEN{get{return moveState == MOVESTATE.FROZEN;}}
	public bool IF_MOVEABLE{get{return moveState == MOVESTATE.MOVEABLE;}}
	public bool IF_MOVING{get{return moveState == MOVESTATE.MOVING;}}
	public bool IF_PULLING{get{return moveState == MOVESTATE.PULLING;}}
	[SerializeField] int speed = 7;
	private int CoolDownTime = 5;
	private int buttonCooldown = 0;
	private Vector3 pos;
	private MoveToTask moveToTask;
	private Task_Manager taskManager = new Task_Manager();
	void Start() {
		moveState = MOVESTATE.FROZEN;
		availableDir = new List<DIRECTION>();
		moveToTask = new MoveToTask(transform, transform.position,speed);
	}
	void Update() {
		Debug.Log(pos);
		taskManager.Update();
		buttonCooldown --;

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
			default:
				break;
		}
	}
	void FROZEN_Update(){
		UpdateDir_Event updateDir_Event = new UpdateDir_Event();
		ClearDirection();
		EventManager.Instance.FireEvent(updateDir_Event);
	}
	void MOVEABLE_Update(){
		pos = transform.position;
		moveCheck();
	}
	void MOVING_Update(){
		UpdateDir_Event updateDir_Event = new UpdateDir_Event(); 
		if(transform.position == pos) {
			ClearDirection();
			moveState = MOVESTATE.FROZEN;
			EventManager.Instance.FireEvent(updateDir_Event);

			//transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
			//move();
		}
		else{
			Mouse_Move();
			//moveTo(pos);
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed * 100);
		}	
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

	//This Function will handle the movement of the Box
	private void move(){
		//First Check if the input in cool time, Between each input, there is a window that receive no input
		//to make sure they are not conflicting with each other
		//Inside it, is to detect whether player input in this direction and whether this direction is allowed.
		if(buttonCooldown <= 0 && Input.GetButton("Fire1"))
		{
			if(CanMoveThisDirection(DIRECTION.FORWARD) && availableDir.Contains(DIRECTION.FORWARD))
			{
				if(dir != DIRECTION.FORWARD)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.FORWARD;
				}
				else
				{
					pos += Vector3.forward*MoveUnit;
					moveTo(pos);
					moveState = MOVESTATE.MOVING;
				}
			}
			else if(CanMoveThisDirection(DIRECTION.BACK) && availableDir.Contains(DIRECTION.BACK))
			{
				if(dir != DIRECTION.BACK)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.BACK;
				}
				else
				{
					pos += Vector3.back*MoveUnit;
					moveTo(pos);
					moveState = MOVESTATE.MOVING;
				}
			}						
			else if(CanMoveThisDirection(DIRECTION.LEFT) && availableDir.Contains(DIRECTION.LEFT))
			{
				if(dir != DIRECTION.LEFT)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.LEFT;
				}
				else
				{
					pos += Vector3.left*MoveUnit;
					moveTo(pos);
					moveState = MOVESTATE.MOVING;
				}
			}					
			else if(CanMoveThisDirection(DIRECTION.RIGHT) && availableDir.Contains(DIRECTION.RIGHT))
			{
				if(dir != DIRECTION.RIGHT)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.RIGHT;
				}
				else
				{
					pos += Vector3.right*MoveUnit;
					moveTo(pos);
					moveState = MOVESTATE.MOVING;
				}
			}					
			else if(CanMoveThisDirection(DIRECTION.UP) && availableDir.Contains(DIRECTION.UP))
			{
				if(dir != DIRECTION.UP)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.UP;
				}
				else
				{
					pos += Vector3.up*MoveUnit;
					moveTo(pos);
					moveState = MOVESTATE.MOVING;
				}
			}						
			else if(CanMoveThisDirection(DIRECTION.DOWN) && availableDir.Contains(DIRECTION.DOWN))
			{
				if(dir != DIRECTION.DOWN)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.DOWN;
				}
				else
				{
					pos += Vector3.down*MoveUnit;
					moveTo(pos);
					moveState = MOVESTATE.MOVING;
				}
			}
		}
	}
	private void moveCheck(){
		if(buttonCooldown <= 0)
		{
			if(CanMoveThisDirection(DIRECTION.FORWARD) && availableDir.Contains(DIRECTION.FORWARD))
			{
				if(dir != DIRECTION.FORWARD)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.FORWARD;
				}
				else
				{
					pos += Vector3.forward*MoveUnit;
					moveState = MOVESTATE.MOVING;
				}
			}
			else if(CanMoveThisDirection(DIRECTION.BACK) && availableDir.Contains(DIRECTION.BACK))
			{
				if(dir != DIRECTION.BACK)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.BACK;
				}
				else
				{
					pos += Vector3.back*MoveUnit;
					moveState = MOVESTATE.MOVING;
				}
			}						
			else if(CanMoveThisDirection(DIRECTION.LEFT) && availableDir.Contains(DIRECTION.LEFT))
			{
				if(dir != DIRECTION.LEFT)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.LEFT;
				}
				else
				{
					pos += Vector3.left*MoveUnit;
					moveState = MOVESTATE.MOVING;
				}
			}					
			else if(CanMoveThisDirection(DIRECTION.RIGHT) && availableDir.Contains(DIRECTION.RIGHT))
			{
				if(dir != DIRECTION.RIGHT)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.RIGHT;
				}
				else
				{
					pos += Vector3.right*MoveUnit;
					moveState = MOVESTATE.MOVING;
				}
			}					
			else if(CanMoveThisDirection(DIRECTION.UP) && availableDir.Contains(DIRECTION.UP))
			{
				if(dir != DIRECTION.UP)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.UP;
				}
				else
				{
					pos += Vector3.up*MoveUnit;
					moveState = MOVESTATE.MOVING;
				}
			}						
			else if(CanMoveThisDirection(DIRECTION.DOWN) && availableDir.Contains(DIRECTION.DOWN))
			{
				if(dir != DIRECTION.DOWN)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.DOWN;
				}
				else
				{
					pos += Vector3.down*MoveUnit;
					moveState = MOVESTATE.MOVING;
				}
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


	private void moveTo(Vector3 endPos){
		if(moveToTask.ifDetached)
			taskManager.AddTask(moveToTask);
		moveToTask.SetEndPos(endPos);
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
		if(Mouse_Check() == (DIRECTION.FORWARD) && availableDir.Contains(DIRECTION.FORWARD)) {
			tempVec = Vector3.forward;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y")*6);
		} else if(Mouse_Check() == (DIRECTION.BACK)&& availableDir.Contains(DIRECTION.BACK)) {
			tempVec = Vector3.forward;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y")*6);
		} else if (Mouse_Check() == (DIRECTION.RIGHT) && availableDir.Contains(DIRECTION.RIGHT)) {
			tempVec = Vector3.right;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse X")*6);
		} else if(Mouse_Check() == (DIRECTION.LEFT) && availableDir.Contains(DIRECTION.LEFT)) {
			tempVec = Vector3.right;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse X")*6);
		} else if (Mouse_Check() == (DIRECTION.UP) && availableDir.Contains(DIRECTION.UP)) {
			tempVec = Vector3.up;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y")*6);
		} else if(Mouse_Check() == (DIRECTION.DOWN) && availableDir.Contains(DIRECTION.DOWN)) {
			tempVec = Vector3.up;
			transform.position += tempVec * (int)(Input.GetAxis("Mouse Y")*6);
		} else {
		}
	}
	DIRECTION Mouse_Check(){
		Vector3 tempVec = Vector3.zero;
		float MouseDir = 0.0f;
		MouseDir = Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		MouseDir = MouseDir*Mathf.Rad2Deg;
		if (MouseDir < 0) {
			MouseDir += 360;
		}
		if(MouseDir > minAngleForward && MouseDir <= maxAngleForward) {
			return DIRECTION.FORWARD;
		} else if(MouseDir > minAngleBack && MouseDir <= maxAngleBack) {
			return DIRECTION.BACK;
		} else if (MouseDir > minAngleRight && MouseDir <= maxAngleRight) {
			return DIRECTION.RIGHT;
		} else if(MouseDir > minAngleLeft && MouseDir <= maxAngleLeft) {
			return DIRECTION.LEFT;
		} else if ((MouseDir > minAngleUp && MouseDir <= maxAngleUp)) {
			return DIRECTION.UP;
		} else if(MouseDir > minAngleDown && MouseDir <= maxAngleDown) {
			return DIRECTION.DOWN;
		} else {
			return DIRECTION.EMPTY;
		}
	}
	//This is a MoveTask, it has its own Update Function which will be called in Task Manager that I create within this class
	//Task Manager will handle it for us, What it did is to move one object to a specific place
	public class MoveToTask:Task{
		private Transform moveTrans;
		private Vector3 startPos;
		private Vector3 endPos;
		private float timer;
		private int speed;
		public MoveToTask(Transform m_Trans, Vector3 m_endPos, int m_speed){
			moveTrans = m_Trans;
			startPos = moveTrans.position;
			endPos = m_endPos;
			speed = m_speed;
		}
		protected override void Init(){
			timer = 0.0f;
		}
		internal override void TUpdate(){
			timer += Time.deltaTime;
			moveTrans.position = Vector3.Lerp(startPos, endPos, Easing.BackEaseInOut(timer * speed));

			if(moveTrans.position == endPos)
				SetStatus(TaskStatus.Success);
		}
		public void SetEndPos(Vector3 m_endPos){
			startPos = moveTrans.position;
			endPos = m_endPos;
		}
		public void SetSpeed(int m_Speed){
			speed = m_Speed;
		}
		public Vector3 GET_ENDPOS(){
			return endPos;
		}
	}
}