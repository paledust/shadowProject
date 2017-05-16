using UnityEngine;

public static class Service {
	public static GameObject ActiveDirLight;
	public static void SetNewActiveDirLight(GameObject m_Light){
		if(ActiveDirLight != null)
			ActiveDirLight.GetComponent<DirLightManager>().UnregisterFunction();
			
		ActiveDirLight = m_Light;
		if(ActiveDirLight.GetComponent<DirLightManager>())
			ActiveDirLight.GetComponent<DirLightManager>().registerFunction();
	}
	public static EventManager eventManager;
	public static AudioManagerScript audioManager;
	public static PrefebList prefebList = Resources.Load<PrefebList>("List/PrebList");
	public static Color invis_Color = new Color(1,1,1,0);
}
