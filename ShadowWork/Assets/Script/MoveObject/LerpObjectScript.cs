using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpObjectScript : MonoBehaviour {
	

	public static void LerpObject(GameObject obj, Vector3 pos1, Vector3 pos2, float lerpTime, float currentLerpTime){
		//currentLerpTime += Time.deltaTime;
		if(currentLerpTime >= lerpTime){
			currentLerpTime = lerpTime;
		}
		float perc = currentLerpTime/lerpTime;
		obj.transform.position = Vector3.Lerp(pos1, pos2, perc);
	}

}
