﻿using UnityEngine;
using CS_Kevin;

public class CollisionBox : Collision_Base {
	Task_Manager taskManager;
	MoveObject.MoveToTask moveToTask;
	MoveObject.MoveToTask moveBack;
	Vector3 OriginPos;
	[SerializeField] int speed;
	[SerializeField] int Offset;
	protected override void Start(){
		pushState = PushState.WAIT;
		OriginPos = transform.position;

		taskManager = new Task_Manager();
		moveToTask = new MoveObject.MoveToTask(transform, transform.position, speed);
		moveBack = new MoveObject.MoveToTask(transform, OriginPos, speed);
		moveToTask.Then(moveBack);
	}
	protected override void Update(){
		base.Update();
		
		taskManager.Update();
	}
	protected override void WAIT_UPDATE(){

	}
	protected override void PUSH_UPDATE(){

	}
	protected override void OnTriggerEnter(Collider collider){
		if(collider.name == "MovingBox"){
			collider.GetComponent<MoveObject>().MoveBack();
			collider.GetComponent<MoveObject>().SetStatus(MOVESTATE.PULLING);
			if(moveToTask.ifDetached){
				moveToTask.SetEndPos(HandleDirection.DIRECTION_To_VECTOR(collider.GetComponent<MoveObject>().dir) * Offset + transform.position);
				taskManager.AddTask(moveToTask);
			}
		}
	}

}
