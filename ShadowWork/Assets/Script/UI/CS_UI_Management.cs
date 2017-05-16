using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class CS_UI_Management : MonoBehaviour {
	[System.SerializableAttribute]
	public struct Orient_UI{
		public CS_UI_Element left;
		public CS_UI_Element right;
		public CS_UI_Element Up;
		public CS_UI_Element Down;
		public CS_UI_Element Center;
	}
	[SerializeField] private Orient_UI UI_Sprite;
	public Orient_UI Element{get{return UI_Sprite;}}
	private DetectInShadow detectInShadow;

	// Use this for initialization
	void Start () {
		detectInShadow = GetComponentInParent<DetectInShadow>();
	}
	
	// Update is called once per frame
	void Update () {
		if(detectInShadow.IfActive){
			if(!UI_Sprite.Center.ifActivated)
				LitArrow(DIRECTION.EMPTY);
			LightManager();
		}
		else{
			TurnOffAll();
		}
	}
	public void LitArrow(DIRECTION direction){
		switch (direction)
		{
			case DIRECTION.RIGHT:
				UI_Sprite.right.LitUp();
				return;
			case DIRECTION.LEFT:
				UI_Sprite.left.LitUp();
				return;
			case DIRECTION.UP:
				UI_Sprite.Up.LitUp();
				return;
			case DIRECTION.DOWN:
				UI_Sprite.Down.LitUp();
				return;
			case DIRECTION.EMPTY:
				UI_Sprite.Center.LitUp();
				return;
			default:
				return;
		}
	}
	public void OffArrow(DIRECTION direction){
		switch (direction)
		{
			case DIRECTION.RIGHT:
				UI_Sprite.right.Off();
				return;
			case DIRECTION.LEFT:
				UI_Sprite.left.Off();
				return;
			case DIRECTION.UP:
				UI_Sprite.Up.Off();
				return;
			case DIRECTION.DOWN:
				UI_Sprite.Down.Off();
				return;
			case DIRECTION.EMPTY:
				UI_Sprite.Center.Off();
				return;
			default:
				return;
		}
	}
	public void TurnOffAll(){
		if(UI_Sprite.left.ifActivated){OffArrow(DIRECTION.LEFT);}
		if(UI_Sprite.right.ifActivated){OffArrow(DIRECTION.RIGHT);}
		if(UI_Sprite.Up.ifActivated){OffArrow(DIRECTION.UP);}
		if(UI_Sprite.Down.ifActivated){OffArrow(DIRECTION.DOWN);}
		if(UI_Sprite.Center.ifActivated){OffArrow(DIRECTION.EMPTY);}
	}

	void LightManager(){
		if(detectInShadow.GET_Direction.Contains(DIRECTION.LEFT)){
			if(!UI_Sprite.left.ifActivated){
				LitArrow(DIRECTION.LEFT);
			}
		}
		else{
			OffArrow(DIRECTION.LEFT);
		}

		if(detectInShadow.GET_Direction.Contains(DIRECTION.RIGHT)){
			if(!UI_Sprite.right.ifActivated){
				LitArrow(DIRECTION.RIGHT);
			}
		}
		else
			OffArrow(DIRECTION.RIGHT);

		if(detectInShadow.GET_Direction.Contains(DIRECTION.UP) || 
			detectInShadow.GET_Direction.Contains(DIRECTION.FORWARD)){
				if(!UI_Sprite.Up.ifActivated)
					LitArrow(DIRECTION.UP);
			}
		else
			OffArrow(DIRECTION.UP);

		if(detectInShadow.GET_Direction.Contains(DIRECTION.DOWN) || 
			detectInShadow.GET_Direction.Contains(DIRECTION.BACK)){
				if(!UI_Sprite.Down.ifActivated)
					LitArrow(DIRECTION.DOWN);
			}
		else
			OffArrow(DIRECTION.DOWN);
	}
}
