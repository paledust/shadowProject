using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public GameObject ActiveDirLight;
	// Use this for initialization
	void Awake () {
		KeyObjCollect.Instance.SetNewActiveDirLight(ActiveDirLight);
	}
}
