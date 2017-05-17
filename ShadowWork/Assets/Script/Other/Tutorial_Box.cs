using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class Tutorial_Box : MonoBehaviour {
	[SerializeField] CS_Tutorial_UI Joystick;
	[SerializeField] List<DIRECTION> UI_Directions;

	void Update(){

	} 
	void OnTriggerStay(Collider collider){
		if(collider.name == "MovingBox"){
			if(UI_Directions.Count > 0 && Joystick.gameObject.GetComponent<UI_Activated>().Activated){
				foreach(DIRECTION dir in UI_Directions){
					Joystick.LitArrow(dir);
				}
			}
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.name == "MovingBox"){
			if(UI_Directions.Count > 0){
				foreach(DIRECTION dir in UI_Directions){
					Joystick.OffArrow(dir);
				}
			}
		}
	}
}
