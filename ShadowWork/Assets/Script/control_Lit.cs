using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_Lit : MonoBehaviour {
	[SerializeField] CS_UI_Management UI_Management;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)){
			if(!UI_Management.Element.Up.ifActivated)
				UI_Management.LitArrow(CS_Kevin.DIRECTION.UP);
			else{
				UI_Management.OffArrow(CS_Kevin.DIRECTION.UP);
			}
		}
		if(Input.GetKeyDown(KeyCode.S)){
			if(!UI_Management.Element.Down.ifActivated)
				UI_Management.LitArrow(CS_Kevin.DIRECTION.DOWN);
			else{
				UI_Management.OffArrow(CS_Kevin.DIRECTION.DOWN);
			}
		}
		if(Input.GetKeyDown(KeyCode.A)){
			if(!UI_Management.Element.left.ifActivated)
				UI_Management.LitArrow(CS_Kevin.DIRECTION.LEFT);
			else{
				UI_Management.OffArrow(CS_Kevin.DIRECTION.LEFT);
			}
		}
		if(Input.GetKeyDown(KeyCode.D)){
			if(!UI_Management.Element.right.ifActivated)
				UI_Management.LitArrow(CS_Kevin.DIRECTION.RIGHT);
			else{
				UI_Management.OffArrow(CS_Kevin.DIRECTION.RIGHT);
			}
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			if(!UI_Management.Element.Center.ifActivated)
				UI_Management.LitArrow(CS_Kevin.DIRECTION.EMPTY);
			else{
				UI_Management.OffArrow(CS_Kevin.DIRECTION.EMPTY);
			}
		}
		// if(Input.GetKeyDown(KeyCode.Space)){
		// 	UI_Management.LitArrow(CS_Kevin.DIRECTION.UP);
		// }
	}
}
