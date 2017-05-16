using UnityEngine;
using CS_Kevin;

public class Collision_Base : MonoBehaviour {
	protected virtual void Start(){
	}
	protected virtual void Update(){

	}
	protected virtual void WAIT_UPDATE(){
	}
	protected virtual void PUSH_UPDATE(){
	}
	protected virtual void ON_HIT(Collider collider){}
	protected virtual void OnTriggerEnter(Collider collider){
		if(collider.name == "MovingBox"){
			ON_HIT(collider);
			collider.GetComponent<MoveObject>().SetStatus(MOVESTATE.PULLING);
			collider.GetComponent<MoveObject>().MoveBack();
			Service.audioManager.PlaySound2D("BoxCollide", 0.8f, Random.Range(0.7f,1.0f));
		}
	}
	// protected virtual void OnTriggerExit(Collider collider){
	// 	if(collider.name == "MovingBox"){
	// 		collider.GetComponent<MoveObject>().SetStatus(MOVESTATE.FROZEN);
	// 	}
	// }
}
