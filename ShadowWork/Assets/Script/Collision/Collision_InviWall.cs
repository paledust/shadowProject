using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_InviWall : Collision_Base {
	private Wall_Manager wallManager;
	protected override void Start(){
		wallManager = GameObject.Find("WallManager").GetComponent<Wall_Manager>();
	}
	protected override void ON_HIT(Collider collider){
		Vector3 Offset = collider.transform.position - transform.rotation * Vector3.up * 9;
		wallManager.Put_Wall(Offset, transform.parent.rotation);
	}
	
}
