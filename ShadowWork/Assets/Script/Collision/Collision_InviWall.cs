using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class Collision_InviWall : Collision_Base {
	private Wall_Manager wallManager;
	protected override void Start(){
		wallManager = GameObject.Find("WallManager").GetComponent<Wall_Manager>();
	}
	protected override void ON_HIT(Collider collider){
		Vector3 Offset = collider.transform.position - transform.rotation * Vector3.up * 9;
		wallManager.Put_Wall(Offset, transform.parent.rotation);
	}
	protected override void OnTriggerEnter(Collider collider){
		if(collider.name == "MovingBox"){
			ON_HIT(collider);
			collider.GetComponent<MoveObject>().SetStatus(MOVESTATE.PULLING);
			collider.GetComponent<MoveObject>().MoveBack();
			Service.audioManager.PlaySound2D("BoxCollideInviWall", 0.8f, Random.Range(0.7f,1.0f));
		}
	}
}
