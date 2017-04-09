using UnityEngine;
using System.Collections;


public class ObjAct : MonoBehaviour {

	public float RotateSpeed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		int i = Random.Range (0, 3);
		switch (i) {
		case 0:
			transform.Rotate(Vector3.Lerp(Vector3.zero, new Vector3(180, 0, 0), Time.deltaTime * RotateSpeed), Space.World);
			break;
		case 1:
			transform.Rotate(Vector3.Lerp(Vector3.zero, new Vector3(0, 180, 0), Time.deltaTime * RotateSpeed), Space.World);
			break;
		case 2:
			transform.Rotate(Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 180), Time.deltaTime * RotateSpeed), Space.World);
			break;
		default:
			break;
		}

		if (Input.GetMouseButton (0)
		    || Input.GetMouseButton (1)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (CustomCursor.MousePosition);
			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
				if (hit.collider.gameObject == this.gameObject) {
					Material m = this.GetComponent<Renderer>().material;

					if (m)
						m.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

					Debug.Log("Been Clicked!");
				}
			}
		}
	}
}
