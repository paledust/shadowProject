using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;
using Kevin_Event;

public class OldMoveObject : MonoBehaviour {
	public DirectionCheck direction;
	public List<DIRECTION> availableDir;
	public bool ifDrag = false;
	public int MoveUnit = 6;
	public MOVESTATE moveState;
	public DIRECTION dir;
	public bool IF_FROZEN{get{return moveState == MOVESTATE.FROZEN;}}
	public bool IF_MOVEABLE{get{return moveState == MOVESTATE.MOVEABLE;}}
	public bool IF_MOVING{get{return moveState == MOVESTATE.MOVING;}}
	[SerializeField] int CoolDownTime = 5;
	private bool canMove = true, moving = false;
	private int speed = 7, buttonCooldown = 0;
	private Vector3 pos;
	private MoveToTask moveToTask;
	private Task_Manager taskManager = new Task_Manager();

	void Start() {
		moveState = MOVESTATE.FROZEN;
		direction = new DirectionCheck();
		availableDir = new List<DIRECTION>();
		moveToTask = new MoveToTask(transform, transform.position,speed);
	}
	void Update() {
		taskManager.Update();
		buttonCooldown --;
		UpdateDir_Event tempEvent = new UpdateDir_Event(); 
		switch (moveState)
		{
			case MOVESTATE.MOVEABLE:
				pos = transform.position;
				move();
				break;
			case MOVESTATE.MOVING:
				if(transform.position == pos) {
					Service.eventManager.FireEvent(tempEvent);
					moveState = MOVESTATE.FROZEN;
					move();
				}
				break;
			case MOVESTATE.FROZEN:
				Debug.Log("FROZEN");
				Service.eventManager.FireEvent(tempEvent);
				break;
			default:
				break;
		}
	}

	public void SetDirection(DirectionOption dirOption, bool ifAvaliable){
		switch (dirOption)
		{
			case DirectionOption.left: direction.ifLeft = ifAvaliable; break;
			case DirectionOption.right: direction.ifRight = ifAvaliable; break;
			case DirectionOption.up: direction.ifUp = ifAvaliable; break;
			case DirectionOption.down: direction.ifDown = ifAvaliable; break;
			case DirectionOption.forward: direction.ifForward = ifAvaliable; break;
			case DirectionOption.back: direction.ifBack = ifAvaliable; break;
			case DirectionOption.x: direction.ifLeft = ifAvaliable; direction.ifRight = ifAvaliable;  break;
			case DirectionOption.y: direction.ifUp = ifAvaliable; direction.ifDown = ifAvaliable; break;
			case DirectionOption.z: direction.ifForward = ifAvaliable; direction.ifBack = ifAvaliable; break;
			default: break;
		}
	}
	public void AddDirection(DIRECTION direction){
		if(!availableDir.Contains(direction))
		{
			availableDir.Add(direction);
		}
	}
	public void DisableDirection(DIRECTION direction){
		if(availableDir.Contains(direction))
		{
			availableDir.Remove(direction);
		}
	}
	public void SetStatus(MOVESTATE m_moveState){
		moveState = m_moveState;
	}
	private void move(){
		if(buttonCooldown <= 0)
		{
			if(Input.GetAxis("Vertical")>0 && availableDir.Contains(DIRECTION.FORWARD))
			{
				if(dir != DIRECTION.FORWARD)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.FORWARD;
				}
				else
				{
					moveState = MOVESTATE.MOVING;
					pos += Vector3.forward*MoveUnit;
					moveTo(pos);
				}
			}
			else if(Input.GetAxis("Vertical")<0 && availableDir.Contains(DIRECTION.BACK))
			{
				if(dir != DIRECTION.BACK)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.BACK;
				}
				else
				{
					moveState = MOVESTATE.MOVING;
					pos += Vector3.back*MoveUnit;
					moveTo(pos);
				}
			}						
			else if(Input.GetAxis("Horizontal")<0 && availableDir.Contains(DIRECTION.LEFT))
			{
				if(dir != DIRECTION.LEFT)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.LEFT;
				}
				else
				{
					moveState = MOVESTATE.MOVING;
					pos += Vector3.left*MoveUnit;
					moveTo(pos);
				}
			}					
			else if(Input.GetAxis("Horizontal")>0 && availableDir.Contains(DIRECTION.RIGHT))
			{
				if(dir != DIRECTION.RIGHT)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.RIGHT;
				}
				else
				{
					moveState = MOVESTATE.MOVING;
					pos += Vector3.right*MoveUnit;
					moveTo(pos);
				}
			}					
			else if(Input.GetAxis("Height")>0 && availableDir.Contains(DIRECTION.UP))
			{
				if(dir != DIRECTION.UP)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.UP;
				}
				else
				{
					moveState = MOVESTATE.MOVING;
					pos += Vector3.up*MoveUnit;
					moveTo(pos);
				}
			}						
			else if(Input.GetAxis("Height")<0 && availableDir.Contains(DIRECTION.DOWN))
			{
				if(dir != DIRECTION.DOWN)
				{
					buttonCooldown = CoolDownTime;
					dir = DIRECTION.DOWN;
				}
				else
				{
					moveState = MOVESTATE.MOVING;
					pos += Vector3.down*MoveUnit;
					moveTo(pos);
				}
			}						
		}
	}
	private void moveTo(Vector3 endPos){
		if(moveToTask.ifDetached)
			taskManager.AddTask(moveToTask);
		moveToTask.SetEndPos(endPos);
	}
	void UpdateMove(){
		Vector3 tempVec = Vector3.zero;
		if(Input.GetAxis("Vertical")>0)
		{
			if(direction.ifForward)
			{
				tempVec = Vector3.forward;
				transform.position += tempVec * Input.GetAxis("Vertical")*speed;
			}
			else
			{
				ButtonEvent_Forward tempEvent = new ButtonEvent_Forward();
				Service.eventManager.FireEvent(tempEvent);
			}			
		}
		if(Input.GetAxis("Vertical")<0)
		{
			if(direction.ifBack)
			{
				tempVec = Vector3.forward;
				transform.position += tempVec * Input.GetAxis("Vertical")*speed;	
			}
			else
			{
				ButtonEvent_Back tempEvent = new ButtonEvent_Back();
				Service.eventManager.FireEvent(tempEvent);
			}		
		}
		if(Input.GetAxis("Horizontal")>0)
		{
			if(direction.ifRight)
			{
				tempVec = Vector3.right;
				transform.position += tempVec * Input.GetAxis("Horizontal")*speed;	
			}
			else
			{
				ButtonEvent_Right tempEvent = new ButtonEvent_Right();
				Service.eventManager.FireEvent(tempEvent);
			}
		}
		if(Input.GetAxis("Horizontal")<0)
		{	
			if(direction.ifLeft)
			{
				tempVec = Vector3.right;
				transform.position += tempVec * Input.GetAxis("Horizontal")*speed;	
			}
			else
			{
				ButtonEvent_Left tempEvent = new ButtonEvent_Left();
				Service.eventManager.FireEvent(tempEvent);
			}	
		}
		if(Input.GetAxis("Height")>0)
		{		
			if(direction.ifUp)
			{
				tempVec = Vector3.up;
				transform.position += tempVec * Input.GetAxis("Height")*speed;		
			}
			else
			{
				ButtonEvent_Up tempEvent = new ButtonEvent_Up();
				Service.eventManager.FireEvent(tempEvent);
			}				
		}
		if(Input.GetAxis("Height")<0)
		{	
			if(direction.ifDown)
			{
				tempVec = Vector3.up;
				transform.position += tempVec * Input.GetAxis("Height")*speed;	
			}
			else
			{
				ButtonEvent_Down tempEvent = new ButtonEvent_Down();
				Service.eventManager.FireEvent(tempEvent);
			}		
		}
	}
	void MovementCheck(){
		if(transform.position.y < 7.5f)
		{
			transform.position = new Vector3(transform.position.x,7.5f, transform.position.z);
		}
	}
	public class MoveToTask:Task{
		private Transform moveTrans;
		private Vector3 startPos;
		private Vector3 endPos;
		private float timer;
		private int speed;
		public MoveToTask(Transform m_Trans, Vector3 m_endPos, int m_speed)
		{
			moveTrans = m_Trans;
			startPos = moveTrans.position;
			endPos = m_endPos;
			speed = m_speed;
		}
		protected override void Init()
		{
			timer = 0.0f;
		}
		internal override void TUpdate()
		{
			timer += Time.deltaTime;
			moveTrans.position = Vector3.Lerp(startPos, endPos, (timer)*speed);

			if(moveTrans.position == endPos)
				SetStatus(TaskStatus.Success);
		}
		public void SetEndPos(Vector3 m_endPos)
		{
			startPos = moveTrans.position;
			endPos = m_endPos;
		}
	}
}