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
}
