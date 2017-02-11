using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public struct LightData
{
	public Transform tagtTransform;
	public int[] lightsIndex;
}

public class LightManager : MonoBehaviour {
	public LightData[] datas; 
	public	GameObject movePlat;
	public float DetctRange = 1.0f;
	public TriggerLight[] lights;
	private bool inPosition = false;
	// Use this for initialization
	void Start () {
		movePlat = GameObject.FindGameObjectWithTag("MovingPlat");
		inPosition = false;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (LightData data in datas)
		{
			if((movePlat.transform.position - data.tagtTransform.position).magnitude <= DetctRange)
			{
				for(int i = 0; i < data.lightsIndex.Length; i++)
				{
					TurnOnLight(data.lightsIndex[i]);
				}
				break;
			}
			else
			{
				for(int i = 0; i < data.lightsIndex.Length; i++)
				{
					TurnOffLight(data.lightsIndex[i]);
				}
			}
		}
	}

	void TurnOnLight(int _LightIndex)
	{
		if(!lights[_LightIndex].lightState())
			lights[_LightIndex].LightOn();
	}

	void TurnOffLight(int _LightIndex)
	{
		if(lights[_LightIndex].lightState())
			lights[_LightIndex].LightOff();
	}
}