using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class DragMouse : MonoBehaviour {
	public FACING_DIRECTIOM facingDir;
	public List<DIRECTION> directions;
	[SerializeField] Color DeactiveColor = Color.black;
	[SerializeField] Color ActivateColor = Color.blue;
	DragObjectScriptDanVersion moveObject;
	RaycastHit[] rayHits;
	Ray ray;
	RaycastHit rayhit;
	private bool ifDrag = false;
	
	void Start() {
		//Register UpdateDir_Handler Function to UpdateDir_Event
		//Whenever UpdateDir_Event Fired, UpdateDir_Handler Function will be Called once
		moveObject = GetComponentInParent<DragObjectScriptDanVersion>();
	}
	void Update(){
		if(Input.GetButtonDown("Fire1")){
			OnMouseDown();
		}
		if(Input.GetButton("Fire1")){
			HoldMouse();
		}
		if(Input.GetButtonUp("Fire1")){
			OnMouseUp();
		}
	}
	void FACE_ACTIVE(){
		Camera.main.GetComponent<CustomCursor>().Cursor_Raycast(out rayhit);
		if(rayhit.collider.gameObject == gameObject && Input.GetButtonDown("Fire1")){
			ifDrag = true;		
		} 	
		if(ifDrag){
			// Vector3 cursorPos = Camera.main.WorldToScreenPoint(transform.position);
			// Camera.main.GetComponent<CustomLockCursor>().SetCursor(cursorPos);
			// Debug.Log("Hello");
			GetComponentInParent<MoveObject>().Set_DragFace(facingDir);	
		}
		if(Input.GetButtonUp("Fire1")){
			ifDrag = false;
		}
	}
	void OnMouseDown(){
		Camera.main.GetComponent<CustomCursor>().Cursor_Raycast(out rayhit);
		if(rayhit.collider.gameObject == gameObject){
			ifDrag = true;	
			moveObject.AddFaceDir(facingDir);
		} 	
	}
	void OnMouseUp(){
		ifDrag = false;
	}
	void HoldMouse(){

	}
}
