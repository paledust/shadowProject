using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjCollect {
	static private KeyObjCollect instance;
	static public KeyObjCollect Instance{
		get{
			if(instance == null)
				instance = new KeyObjCollect();
			return instance;
		}
	}
	
	public GameObject ActiveDirLight;
	public void SetNewActiveDirLight(GameObject m_Light)
	{
		if(ActiveDirLight != null)
			ActiveDirLight.GetComponent<DirLightManager>().UnregisterFunction();
			
		ActiveDirLight = m_Light;
		ActiveDirLight.GetComponent<DirLightManager>().registerFunction();
	}
}
