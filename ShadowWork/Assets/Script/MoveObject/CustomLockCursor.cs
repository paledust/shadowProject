using UnityEngine;
using UnityEngine.UI;

//#define CustomCursor.MousePosition CCMousePos

public class CustomLockCursor : MonoBehaviour {

	public static Vector3 MousePosition = Vector3.zero;
	public Image cursor;
	public float MouseSpeed = 1f;
	public float FollowSpeed = 1f;
	public LayerMask DragLayer;
	private RaycastHit rayhit;
	private bool CursorFollow;

	// Update is called once per frame
	void Start(){
		if (Cursor.visible) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	void Update () {
		if(Input.GetButtonDown("Fire1") && Cursor_Raycast(out rayhit, 100.0f, DragLayer)){
			CursorFollow = true;
			SetCursor(rayhit.collider.transform.position);
		}

		if(CursorFollow){
			Vector3 cursorPos = Camera.main.WorldToScreenPoint(rayhit.collider.transform.position);
			Debug.Log(rayhit.collider);
			Camera.main.GetComponent<CustomLockCursor>().SetCursor(cursorPos);
			SetCursor(cursorPos);
		} else{
			Debug.Log("No!!");
			Vector3 p = cursor.rectTransform.position;
			p.x += Input.GetAxis ("Mouse X") * MouseSpeed;
			p.y += Input.GetAxis ("Mouse Y") * MouseSpeed;

			if (p.x < 0) p.x = 0;
			if (p.x > Screen.width) p.x = Screen.width;
			if (p.y < 0) p.y = 0;
			if (p.y > Screen.height) p.y = Screen.height;

			cursor.rectTransform.position = p;
			MousePosition = cursor.rectTransform.position;
		}

		if(Input.GetButtonUp("Fire1")){
			CursorFollow = false;
		}
	}
	public void SetCursor(Vector3 cursorPos){
		Vector3 p = cursorPos;
		
		if (p.x < 0) p.x = 0;
		if (p.x > Screen.width) p.x = Screen.width;
		if (p.y < 0) p.y = 0;
		if (p.y > Screen.height) p.y = Screen.height;

		cursor.rectTransform.position = p;
	}
	
	public bool Cursor_Raycast(float distance, LayerMask m_layerMask){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (CustomLockCursor.MousePosition);

		return(Physics.Raycast (ray.origin, ray.direction, out hit,distance, m_layerMask));
	}
	public bool Cursor_Raycast(out RaycastHit rayhit, float distance, LayerMask m_layerMask){
		Ray ray = Camera.main.ScreenPointToRay (CustomLockCursor.MousePosition);
		return(Physics.Raycast (ray.origin, ray.direction, out rayhit,distance, m_layerMask));
	}
	public bool Cursor_Raycast(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (CustomLockCursor.MousePosition);

		return(Physics.Raycast (ray.origin, ray.direction, out hit));
	}
	public bool Cursor_Raycast(out RaycastHit rayhit){
		Ray ray = Camera.main.ScreenPointToRay (CustomLockCursor.MousePosition);
		return(Physics.Raycast (ray.origin, ray.direction, out rayhit));
	}

}
