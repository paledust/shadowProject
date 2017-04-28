using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class CS_UI_Management : MonoBehaviour {
	[System.SerializableAttribute]
	public struct Orient_UI{
		public SpriteRenderer left;
		public SpriteRenderer right;
		public SpriteRenderer Up;
		public SpriteRenderer Down;
		public SpriteRenderer Center;
	}
	[SerializeField] private Orient_UI UI_Sprite;
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
		if(detectInShadow.IfActive){
			if(If_Enter_Shadow_Anime){
				
			}
			if(If_Sustain_Anime){

			}
			if(If_Direction_Anime){
				
			}
		}
		else{
			
		}
	}
}
