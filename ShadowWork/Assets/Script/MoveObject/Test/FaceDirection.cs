using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;
public class FaceDirection : MonoBehaviour {
	public FACING_DIRECTIOM  faceDirection;

	void OnMouseOver(){
		GetComponentInParent<DragObjectScriptDanVersion>().AddFaceDir(faceDirection);
	}
	
}
