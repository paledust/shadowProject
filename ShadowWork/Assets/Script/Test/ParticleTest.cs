using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour {
	[SerializeField] Wall_Manager WallManager;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			WallManager.Put_Wall(pos);
		}
	}
}
