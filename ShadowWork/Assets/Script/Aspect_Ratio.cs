using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspect_Ratio : MonoBehaviour {

	[SerializeField] Vector2 myDefaultRatio = new Vector2 (16, 9);
	private Vector2 myLastScreenSize = Vector2.zero;
	private float myOrthographicSize;

	// Use this for initialization
	void Start () {
		myOrthographicSize = this.GetComponent<Camera> ().orthographicSize;
		UpdateOrthographicSize ();
	}

	// Update is called once per frame
	void Update () {
		if (Screen.width != myLastScreenSize.x || Screen.height != myLastScreenSize.y) {
			UpdateOrthographicSize ();
		}
	}

	private void UpdateOrthographicSize () {
		this.GetComponent<Camera> ().orthographicSize = myOrthographicSize * myDefaultRatio.x / myDefaultRatio.y / (float)Screen.width * (float)Screen.height;
		myLastScreenSize = new Vector2 (Screen.width, Screen.height);
	}
}