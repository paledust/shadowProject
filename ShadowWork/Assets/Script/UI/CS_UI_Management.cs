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
	[SerializeField] private bool If_Enter_Shadow_Anime = true;
	[SerializeField] private bool If_Sustain_Anime = false;
	[SerializeField] private bool If_Direction_Anime = true;
	private DetectInShadow detectInShadow;

	// Use this for initialization
	void Start () {
		detectInShadow = GetComponentInParent<DetectInShadow>();
	}
	
	// Update is called once per frame
	void Update () {
		// if(detectInShadow.IfActive){
		// 	if(If_Enter_Shadow_Anime){
				
		// 	}
		// 	if(If_Sustain_Anime){

		// 	}
		// 	if(If_Direction_Anime){
				
		// 	}
		// }
		// else{
			
		// }
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
}
