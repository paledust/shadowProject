using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class MoveObject : MonoBehaviour {
	public List<DIRECTION> availableDir;
	public DIRECTION dir;
	public int MoveUnit = 6;
	public MOVESTATE moveState;
	public bool IF_FROZEN{get{return moveState == MOVESTATE.FROZEN;}}
	public bool IF_MOVEABLE{get{return moveState == MOVESTATE.MOVEABLE;}}
	public bool IF_MOVING{get{return moveState == MOVESTATE.MOVING;}}
	public bool IF_PULLING{get{return moveState == MOVESTATE.PULLING;}}
	[SerializeField] int CoolDownTime = 5;
	[SerializeField] int speed = 7;
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
					ClearDirection();
					EventManager.Instance.FireEvent(tempEvent);
					moveState = MOVESTATE.FROZEN;
					move();
				}
				break;
			case MOVESTATE.FROZEN:
				Debug.Log("FROZEN");
				ClearDirection();
				EventManager.Instance.FireEvent(tempEvent);
				break;
			default:
				break;
		}
	}

	//This Function will add one direction into the availableDir list
	public void AddDirection(DIRECTION direction){
		if(!availableDir.Contains(direction))
		{
			availableDir.Add(direction);
		}
	}

	//This Function will Remove one direction into the availableDir list
	public void DisableDirection(DIRECTION direction){
		if(availableDir.Contains(direction))
		{
			availableDir.Remove(direction);
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

	//This way is actually a little bit clunky, I could have used one function in moveTo, but I use a task System instead.
	//What it did is to add a move task to taskmanager to let the box move.(Why not directly let the box move!!!!, because 
	//I'm over concerned about some other task might be implemented into this system)
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

	//This is a MoveTask, it has its own Update Function which will be called in Task Manager that I create within this class
	//Task Manager will handle it for us, What it did is to move one object to a specific place
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