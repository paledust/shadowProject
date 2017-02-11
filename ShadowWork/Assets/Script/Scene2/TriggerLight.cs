using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLight : MonoBehaviour {

	public Color lightOnColor;
	public Color lightOffColor;
	public int lightID;
	[SerializeField] bool ifLight = false;
	// Use this for initialization
	void Start () {
		ifLight = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(ifLight)
		{
			ChangeLight(lightOnColor);
		}
		else
		{
			ChangeLight(lightOffColor);
		}
	}

	public void LightOn()
	{
		ifLight = true;
	}

	public void LightOff()
	{
		ifLight = false;
	}

	private void ChangeLight(Color TagColor)
	{
		GetComponent<Renderer>().material.color = TagColor;
	}

	public bool lightState()
	{
		return ifLight;
	}

	static public int lightNumber()
	{
		return GameObject.FindGameObjectsWithTag("Light").Length;
	}

	public void Getlight()
	{
		Debug.Log(lightID);
	}
}
