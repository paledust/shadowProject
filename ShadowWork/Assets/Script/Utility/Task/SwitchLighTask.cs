using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLighTask : Task {
	private Light new_light;
	private Light old_light;
	private float timer = 0.0f;
	public SwitchLighTask(Light m_startLight, Light m_endLight)
	{
		new_light = m_startLight;
		old_light = m_endLight;
	}
	override internal void TUpdate()
	{
		new_light.intensity = Mathf.Lerp(0.0f, 1.0f, timer);
		old_light.intensity = Mathf.Lerp(1.0f, 0.0f, timer);
		timer += Time.deltaTime;

		if(new_light.intensity == 1.0f && old_light.intensity == 0.0f)
		{
			SetStatus(TaskStatus.Success);
		}
	}
	override protected void CleanUp()
	{
		new_light = null;
		old_light = null;
		timer = 0.0f;
	}
	override protected void OnSuccess()
	{
		Debug.Log("Done");
		Service.SetNewActiveDirLight(new_light.gameObject);
	}
}
