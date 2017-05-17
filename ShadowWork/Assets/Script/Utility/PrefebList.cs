using UnityEngine;

[CreateAssetMenu (menuName = "PrefebList")]
public class PrefebList : ScriptableObject {
	[SerializeField] GameObject _Hero_Cube;
	public GameObject Hero_Cube{get{return _Hero_Cube;}}
	[SerializeField] GameObject _AudioManager;
	public GameObject AudioManager{get{return _AudioManager;}}
	[SerializeField] GameObject _Canvas;
	public GameObject canvas{get{return _Canvas;}}
	[SerializeField] GameObject _SceneManager;
	public GameObject SceneManager{get{return _SceneManager;}}
	[SerializeField] GameObject _BackGround;
	public GameObject BackGround{get{return _BackGround;}}
	[SerializeField] GameObject _Wall;
	public GameObject Wall{get{return _Wall;}}
	[SerializeField] GameObject _Wall_Manager;
	public GameObject WallManager{get{return _Wall_Manager;}}
	[SerializeField] GameObject _Restart_UI;
	public GameObject Restart_UI{get{return _Restart_UI;}}
}
