using UnityEngine;
using CS_Kevin;


public class Collision_Ground : Collision_Base {
	
	protected override void OnTriggerEnter(Collider collider){
		if(collider.name == "MovingBox"){
			ON_HIT();
			collider.GetComponent<MoveObject>().SetStatus(MOVESTATE.PULLING);
			collider.GetComponent<MoveObject>().MoveBack();
			Service.audioManager.PlaySound2D("BoxCollideGround", 0.8f, Random.Range(0.7f,1.0f));
		}
	}
}
