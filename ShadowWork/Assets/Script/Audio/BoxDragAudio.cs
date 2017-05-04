using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDragAudio : MonoBehaviour {
	private MoveObject moveObject;
	private AudioSource audioSource;
	private bool IfAudioPlay;
	// Use this for initialization
	void Start () {
		moveObject = GetComponent<MoveObject>();
		if(!GetComponent<AudioSource>())
			gameObject.AddComponent<AudioSource>();
		audioSource = GetComponent<AudioSource>();

		audioSource.playOnAwake = false;
		audioSource.clip = Service.audioManager.AudioLibrary.GetClipFromName("BoxDrag");
	}
	
	// Update is called once per frame
	void Update () {
		if(moveObject.IF_PLAY_DRAG_SOUND && !IfAudioPlay){
			IfAudioPlay = true;
			PlayDragSound();
		}
		else if(!moveObject.IF_PLAY_DRAG_SOUND){
			IfAudioPlay = false;
			StopDragSound();
		}
	}
	private void PlayDragSound(){
		audioSource.Play();
	}
	private void StopDragSound(){
		audioSource.Stop();
	}

}
