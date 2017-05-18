using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspect_Ratio : MonoBehaviour {

	[SerializeField] Vector2 myDefaultRatio = new Vector2 (16.0f, 9.0f);
	private Vector2 myLastScreenSize = Vector2.zero;
	private float myOrthographicSize;
	private Vector2 viewSize;

	// Use this for initialization
	void Awake () {
		myOrthographicSize = this.GetComponent<Camera> ().orthographicSize;
		UpdateOrthographicSize ();
	}

	// Update is called once per frame
	void Update () {
		Debug.Log(Screen.width.ToString() + " " + Screen.height.ToString());
		// if (Screen.width != myLastScreenSize.x || Screen.height != myLastScreenSize.y) {
		// 	UpdateOrthographicSize ();
		// }
	}

	private void UpdateOrthographicSize () {
		float para = (myDefaultRatio.x/myDefaultRatio.y)/Screen.width * Screen.height;
		Debug.Log(para);
		GetComponent<Camera>().rect = new Rect(new Vector2(0, 0.5f - 0.5f/para), new Vector2(1,1f/para));
	}
}