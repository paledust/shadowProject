using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Follow : MonoBehaviour {
	[SerializeField] Transform follow_Trans;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(follow_Trans.position.x, transform.position.y, follow_Trans.position.z);
	}
}
