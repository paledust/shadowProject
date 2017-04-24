using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectShadow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.rotation = Service.ActiveDirLight.transform.rotation;
	}
}
