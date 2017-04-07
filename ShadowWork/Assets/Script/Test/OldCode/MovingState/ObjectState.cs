using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MovingState{
	Frozen,
	Moveable,
	Moving,
	Pulled
}

public class ObjectState {
	// static private GameObject movingObject;
	public MovingState movingState;
	public bool ifFrozen{get{return movingState == MovingState.Frozen;}}
	public bool ifMoveable{get{return movingState == MovingState.Moveable;}}
	public bool ifMoving{get{return movingState == MovingState.Moving;}}
	public bool ifPulled{get{return movingState == MovingState.Pulled;}}

	
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
			case MovingState.Pulled:
				OnBecomePulled();
				break;
			default:
				Debug.Log("Enter Into Non-Status! Wrong!");
				Debug.Assert(false);
				break;
		}
	}

	protected void OnBecomeFrozen()
	{}
	protected void OnBecomeMoveable()
	{}
	protected void OnBecomeMoving()
	{}
	protected void OnBecomePulled()
	{}
	internal void MovingUpdate()
	{}
	internal void SetMovingObject(GameObject _object)
	{
		// movingObject = _object;
	}
	internal void PulledUpdate()
	{
	}
}
