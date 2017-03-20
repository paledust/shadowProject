using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLightSwitchManager : MonoBehaviour {
	public bool ifSwitch = false;
	private float timer;
	private Light light;
	public float tarIntensity;
	private float startIntensity;
	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		startIntensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if(ifSwitch)
		{
			TurnLight();
			timer += Time.deltaTime;
			if(ifSwitchComplete())
			{
				ResetToUnSwitch();
			}
		}
	}

	private void TurnLight()
	{
		light.intensity = Mathf.Lerp(startIntensity, tarIntensity, timer);	
	}
	public void SetIntensity(float m_tarIntensity)
	{
		tarIntensity = m_tarIntensity;
	}

	public bool ifSwitchComplete()
	{
		return light.intensity == tarIntensity;
	}

	private void ResetToUnSwitch()
	{
		timer = 0.0f;
		ifSwitch = false;
		startIntensity = tarIntensity;
	}
}
