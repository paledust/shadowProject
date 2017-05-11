// #define Debug
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Kevin_Event;
using CS_Kevin;

public class MoveObject : MonoBehaviour {
#region Parameter_Zone
	[SerializeField] protected float DragSpeed;
	public float DRAG_SPEED{get{return DragSpeed;}}
	// 0 degrees is right, 90 degrees is up, 180 is left, 270 is down
	//floats for setting angle degrees for each arc
	protected float minAngleForward = 120f; //110
	protected float maxAngleForward = 180f; //160

	protected float minAngleBack = 300f; //290
	protected float maxAngleBack = 360f; //340

	protected float minAngleRight = 0f; //340-360
	protected float maxAngleRight = 60f; //0-40

	protected float minAngleLeft = 180f; //160
	protected float maxAngleLeft = 240f; //220

	protected float minAngleUp = 60f; //70
	protected float maxAngleUp = 120f; //110

	protected float minAngleDown = 240f; //250
	protected float maxAngleDown = 300f; //290
	protected bool IfAudioPlay = false;
	[SerializeField] protected int MoveUnit = 6;
	[SerializeField] protected MOVESTATE moveState;

	#region TWO_DOTS_INFORM
		[SerializeField] protected DetectInShadow Dots_High;
		[SerializeField] protected DetectInShadow Dots_Hor;
		private bool Dots_High_ACTIVE{get{return Dots_High.gameObject.activeSelf && Dots_High.IfActive;}}
		private bool Dots_Hor_ACTIVE{get{return Dots_Hor.gameObject.activeSelf && Dots_Hor.IfActive;}}
		[SerializeField] protected bool If_Two_Joystick;
	#endregion TWO_DOTS_INFORM
		public FACING_DIRECTIOM DragFace{get; private set;}

	public List<DIRECTION> availableDir{get; private set;}
	public DIRECTION dir{get; private set;}
	public int MOVE_UNITE{get; private set;}
	private MOVESTATE _moveState{get{
		if(IF_FROZEN)
			return MOVESTATE.FROZEN;
		if(IF_MOVEABLE)
			return MOVESTATE.MOVEABLE;
		if(IF_MOVING)
			return MOVESTATE.MOVING;
		if(IF_PENDING)
			return MOVESTATE.PENDING;
		if(IF_PULLING)
			return MOVESTATE.PULLING;
		else
			return MOVESTATE.FROZEN;
	}}
	public bool IF_PLAY_DRAG_SOUND{get{return dir != DIRECTION.EMPTY;}}
	public bool IF_FROZEN{get{return _fsm.IF_IN_THE_STATE<FROZEN>();}}
	public bool IF_MOVEABLE{get{return _fsm.IF_IN_THE_STATE<MOVEABLE>();}}
	public bool IF_MOVING{get{return _fsm.IF_IN_THE_STATE<MOVING>();}}
	public bool IF_PULLING{get{return _fsm.IF_IN_THE_STATE<PULLING>();}}
	public bool IF_PENDING{get{return _fsm.IF_IN_THE_STATE<PENDING>();}}
	protected Vector3 _NextPos;
	protected Vector3 _OriginPos;
	public Vector3 Nextpos{
		get{return _NextPos;}
		set{
			_NextPos = new Vector3(Mathf.RoundToInt(value.x),Mathf.RoundToInt(value.y),Mathf.RoundToInt(value.z));}}
	public Vector3 originPos{
		get{return _OriginPos;}
		set{
			_OriginPos = new Vector3(Mathf.RoundToInt(value.x),Mathf.RoundToInt(value.y),Mathf.RoundToInt(value.z));}}
	protected MoveToTask moveToTask;
	protected Task_Manager taskManager = new Task_Manager();
	public FSM<MoveObject> _fsm{get; protected set;}

	#if Debug
		protected float timer;
		protected bool pushButton = false;
		protected Vector3 testPos;
		protected Vector3 StartPos;
	#endif
#endregion Parameter_Zone

#region Function_zone
	protected virtual void Start() {
		_fsm = new FSM<MoveObject>(this);
		_fsm.TransitionTo<PENDING>();
		dir = DIRECTION.EMPTY;
		availableDir = new List<DIRECTION>();
		DragFace = FACING_DIRECTIOM.EMPTY;

		moveToTask = new MoveToTask(transform, transform.position, (int)DragSpeed);
		Nextpos = transform.position;
		originPos = transform.position;

		foreach(DetectInShadow dot in GetComponentsInChildren<DetectInShadow>()){
			if(dot.facingDir == FACING_DIRECTIOM.Y){
				Dots_Hor = dot;
			}
			if(dot.facingDir == FACING_DIRECTIOM.Z){
				Dots_High = dot;
			}
		}

		#if Debug
			StartPos = transform.position;
			timer = 0.0f;
			testPos = transform.position;
		#endif
	}
	protected void Update() {	
		taskManager.Update();
		_fsm.Update();
		moveState = _moveState;
	#if Debug
		timer += Time.deltaTime;
		if(pushButton){
			testPos = Vector3.Lerp(StartPos, StartPos + Vector3.right * 42.0f, timer * 5.0f * 6.0f/42.0f);
		}

		Test(testPos);

		if(Input.GetAxis("Horizontal")>0.0f && !pushButton){
			timer = 0.0f;
			pushButton = true;	
		}
	#endif
	}
	private void Test(Vector3 testPos){
		if(transform.position.x != testPos.x){
			Debug.Log("BoxPos: " + transform.position.x + "Off: " + (testPos.x - transform.position.x).ToString());
		}
	}

	public void AddDirection(DIRECTION direction){
		if(!availableDir.Contains(direction)){
			availableDir.Add(direction);
		}
	}
	public void ClearDirection(){
		availableDir.Clear();
	}
	public void SetStatus(MOVESTATE m_moveState){
		switch (m_moveState)
		{
			case MOVESTATE.FROZEN:
				_fsm.TransitionTo<FROZEN>();
				break;
			case MOVESTATE.MOVEABLE:
				_fsm.TransitionTo<MOVEABLE>();
				break;
			case MOVESTATE.MOVING:
				_fsm.TransitionTo<MOVING>();
				break;
			case MOVESTATE.PULLING:
				_fsm.TransitionTo<PULLING>();
				break;
			case MOVESTATE.PENDING:
				_fsm.TransitionTo<PENDING>();
				break;
			default:
				break;
		}
	}
	protected void moveCheck(){
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
				
				if(!IF_MOVING)
					_fsm.TransitionTo<MOVING>();
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

				if(!IF_MOVING)
					_fsm.TransitionTo<MOVING>();

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

				if(!IF_MOVING)
					_fsm.TransitionTo<MOVING>();

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

				if(!IF_MOVING)
					_fsm.TransitionTo<MOVING>();

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

				if(!IF_MOVING)
					_fsm.TransitionTo<MOVING>();

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

				if(!IF_MOVING)
					_fsm.TransitionTo<MOVING>();

				moveTo(Nextpos);
			}
		}
		else{
			dir = DIRECTION.EMPTY;
			if(availableDir.Count>0){
				_fsm.TransitionTo<MOVEABLE>();
			}
			else{
				_fsm.TransitionTo<FROZEN>();
			}
		}
	}
	protected bool CanMoveThisDirection(DIRECTION checkDirection) {
		switch (checkDirection)
		{
			case DIRECTION.UP:
				return Input.GetAxis("Height")>0.1f;
			case DIRECTION.DOWN:
				return Input.GetAxis("Height")<-0.1f;
			case DIRECTION.LEFT:
				if(!If_Two_Joystick)
					return Input.GetAxis("Horizontal")<-0.0f;
				else
					return (Input.GetAxis("Horizontal_Left") < -0.0f && Dots_Hor_ACTIVE && Dots_Hor.IF_CONTAIN(DIRECTION.LEFT)) ||
							(Input.GetAxis("Horizontal_Right") < -0.0f && Dots_High_ACTIVE && Dots_High.IF_CONTAIN(DIRECTION.LEFT));
			case DIRECTION.RIGHT:
				if(!If_Two_Joystick)
					return Input.GetAxis("Horizontal")>0.0f;
				else
					return (Input.GetAxis("Horizontal_Left") > 0.0f && Dots_Hor_ACTIVE && Dots_Hor.IF_CONTAIN(DIRECTION.RIGHT)) ||
							(Input.GetAxis("Horizontal_Right") > 0.0f && Dots_High_ACTIVE && Dots_High.IF_CONTAIN(DIRECTION.RIGHT)); 
				
			case DIRECTION.FORWARD:
				return Input.GetAxis("Vertical")>0.1f;
			case DIRECTION.BACK:
				return Input.GetAxis("Vertical")<-0.1f;
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
			moveToTask.SetSpeed(DragSpeed);
			moveToTask.SetEndPos(endPos);
			taskManager.AddTask(moveToTask);
		}
		else{
			moveToTask.SetSpeed(DragSpeed);
			moveToTask.SetEndPos(endPos);
		}
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
	}
	public bool AtNextPoint(){
		return transform.position == Nextpos;
	}
	public bool OverNextPoint(){
		return (transform.position - Nextpos).magnitude > MoveUnit;
	}
	public void RoundNextPoint(){
		Nextpos += (transform.position - Nextpos).normalized * MoveUnit;
	}

#region MouseControl
	/*
	void OnMouse_Up(){
		if(transform.position != Nextpos){
			moveTo(Nextpos);
		}
		Service.audioManager.StopPlaying("");
		DragFace = FACING_DIRECTIOM.EMPTY;
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
	 */
#endregion MouseControl

	public void Set_DragFace(FACING_DIRECTIOM m_DragFace){
		if(DragFace != (m_DragFace) && DragFace == FACING_DIRECTIOM.EMPTY)
			DragFace = m_DragFace;
	}
	
	protected void Delay_TransToState<T>(float _Seconds) where T: ObjectState{
		StartCoroutine(TransintoState<T>(_Seconds));
	}
	IEnumerator TransintoState<T>(float _Seconds) where T: ObjectState{
		yield return new WaitForSeconds(_Seconds);
		_fsm.TransitionTo<T>();
	}
#endregion Function_zone

#region FSM_STATE
	//FSM_STATE----------------------------------------
	public class ObjectState: FSM<MoveObject>.State {}
	public class FROZEN: ObjectState{
		public override void Update(){
			UpdateDir_Event updateDir_Event = new UpdateDir_Event();
			Context.ClearDirection();
			Service.eventManager.FireEvent(updateDir_Event);
			// if(Input.GetButtonUp("Fire1")){
			// 	Context.OnMouse_Up();
			// }
		}
	}
	public class MOVEABLE: ObjectState{
		public override void Update(){
			Context.moveCheck();
			// if(Input.GetButtonUp("Fire1")){
			// 	Context.OnMouse_Up();
			// }
		}
	}
	public class MOVING: ObjectState{
		public override void Update(){
			//Start Test
					// if(Input.GetAxis("Horizontal")>0.0f && !Context.pushButton){
					// 	Context.timer = 0.0f;
					// 	Context.pushButton = true;	
					// }

					// Context.timer += Time.deltaTime;
					// if(Context.pushButton){
					// 	Context.testPos = Vector3.Lerp(Context.StartPos, Context.StartPos + Vector3.right * 42.0f, Context.timer * 5.0f * 6.0f/42.0f);
					// }

					// Context.Test(Context.testPos);
			//End Test

			UpdateDir_Event updateDir_Event = new UpdateDir_Event(); 
			if(Context.AtNextPoint()){

				Context.originPos = Context.transform.position;
				Context.ClearDirection();

				Service.eventManager.FireEvent(updateDir_Event);
				Context.moveCheck();
			}
			if(Context.OverNextPoint()){
				Debug.Log("Over Point");
				Context.RoundNextPoint();
				Context.ClearDirection();	

				TransitionTo<FROZEN>();
				Service.eventManager.FireEvent(updateDir_Event);
			}
		}
	}
	public class PULLING: ObjectState{
		private bool IF_Pull;
		public override void OnEnter(){
			IF_Pull = false;
			Context.dir = DIRECTION.EMPTY;
		}
		public override void Update(){
			if(Context.transform.position == Context.Nextpos && !IF_Pull){
				IF_Pull = true;
				Context.Delay_TransToState<FROZEN>(0.3f);
			}
		}
	}
	public class PENDING: ObjectState{
		public override void OnEnter(){
			Context.dir = DIRECTION.EMPTY;
		}
	}
#endregion FSM_STATE
}

public class MoveToTask:Task {
	private MoveObject Context{get{return moveTrans.GetComponent<MoveObject>();}}
	private Transform moveTrans;
	private Vector3 startPos;
	private Vector3 endPos;
	private float timer;
	private float speed;
	public MoveToTask(Transform m_Trans, Vector3 m_endPos, int m_speed){
		moveTrans = m_Trans;
		startPos = m_Trans.position	;
		endPos = m_endPos;
		speed = m_speed;
	}
	protected override void Init(){
		startPos = moveTrans.position;
		timer = 0.0f;
	}
	internal override void TUpdate(){
		timer += Time.deltaTime;

		moveTrans.position = Vector3.Lerp(startPos, endPos, (timer * speed));
		// moveTrans.position += (endPos - startPos).normalized * 0.5f;
		if((moveTrans.position - endPos).magnitude <= 0.01f){
			moveTrans.position = endPos;
		}

		if(moveTrans.position == endPos){
			SetStatus(TaskStatus.Success);
		}
	}
	public void SetEndPos(Vector3 m_endPos){
		// Debug.Log(timer.ToString());
		timer = 0.0f;
		startPos = moveTrans.position;
		endPos = m_endPos;
		// Debug.Log("Origin: " + startPos.ToString() + " NextPos: " + endPos.ToString());
	}
	public void SetSpeed(float m_Speed){
		speed = Mathf.Min(5.0f, m_Speed);
	}
	public Vector3 GET_ENDPOS(){
		return endPos;
	}
}