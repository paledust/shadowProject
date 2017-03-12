﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManager : MonoBehaviour {
	public ObjectState objectState;

	void Awake()
	{
		objectState = new ObjectState();

		objectState.SetMovingObject(gameObject);
		objectState.SetStatus(MovingState.Frozen);
	}
	void Update()
	{
		if(objectState.ifFrozen)
		{}
		if(objectState.ifMoving)
		{
			objectState.MovingUpdate();
		}
	}
}
