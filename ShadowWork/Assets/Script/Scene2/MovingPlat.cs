﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour {
	[SerializeField] bool ifMove;
	[SerializeField] Vector3 AllowDirection;
	// Use this for initialization
	void Start () {
		AllowDirection = Vector3.up;
		ifMove = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetMove(bool _setMove)
	{
		ifMove = _setMove;
	}
}