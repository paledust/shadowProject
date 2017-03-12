using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MovingState{
	Frozen,
	Moveable,
	Moving
}

public class ObjectState {
	private GameObject movingObject;
	public MovingState movingState;
	public bool ifFrozen{get{return movingState == MovingState.Frozen;}}
	public bool ifMoveable{get{return movingState == MovingState.Moveable;}}
	public bool ifMoving{get{return movingState == MovingState.Moving;}}

	
	public void SetStatus(MovingState _State){
		movingState = _State;

		switch (movingState)
		{
			case MovingState.Frozen:
				OnBecomeFrozen();
				break;
			case MovingState.Moveable:
				OnBecomeMoveable();
				break;
			case MovingState.Moving:
				OnBecomeMoving();
				break;
			default:
				Debug.Log("Enter Into Non-Status! Wrong!");
				Debug.Assert(false);
				break;
		}
	}

	protected void OnBecomeFrozen()
	{Debug.Log("Frozen State");}
	protected void OnBecomeMoveable()
	{
		Debug.Log(movingObject);
		
	}
	protected void OnBecomeMoving()
	{Debug.Log("OnMoving State");}
	internal void MovingUpdate()
	{Debug.Log("Moving State");}
	internal void SetMovingObject(GameObject _object)
	{
		movingObject = _object;
	}
}
