using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTimeControl : MonoBehaviour {
	Animator _animator;

	public float AnimeFrame;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		_animator.SetTime(AnimeFrame);
	}

	public void SetAnimeFrame(float value)
	{
		AnimeFrame = value * _animator.GetCurrentAnimatorStateInfo(0).length;
	}
}
