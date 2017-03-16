using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public GameObject ActiveDirLight;
	// Use this for initialization
	void Start () {
		KeyObjCollect.Instance.SetNewActiveDirLight(ActiveDirLight);
	}
}
