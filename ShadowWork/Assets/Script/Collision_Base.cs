using UnityEngine;
using CS_Kevin;

public class Collision_Base : MonoBehaviour {
	public enum PushState{
		PUSH,
		WAIT
	}
	protected PushState pushState;
	protected virtual void Start(){
		pushState = PushState.WAIT;
	}
	protected virtual void Update(){
		switch (pushState)
		{
			case PushState.WAIT:
				WAIT_UPDATE();
				break;
			case PushState.PUSH:
				PUSH_UPDATE();
				break;
			default:
				break;
		}
	}
	protected virtual void WAIT_UPDATE(){
	}
	protected virtual void PUSH_UPDATE(){
	}
	protected virtual void OnTriggerEnter(Collider collider){
		if(collider.name == "MovingBox"){
			collider.GetComponent<MoveObject>().SetStatus(MOVESTATE.PULLING);
			collider.GetComponent<MoveObject>().MoveBack();
			Service.audioManager.PlaySound2D("ClickOff");
		}
	}
	// protected virtual void OnTriggerExit(Collider collider){
	// 	if(collider.name == "MovingBox"){
	// 		collider.GetComponent<MoveObject>().SetStatus(MOVESTATE.FROZEN);
	// 	}
	// }
	public void SetStatus(PushState m_pushState){
		pushState = m_pushState;
	}
}
