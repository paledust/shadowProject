using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

	float masterVolPerc = 1; //master volume
	float sfxVolPerc = 1; //sound effects volume
	float ambientVolPerc = 1; //ambient sound volume

	AudioSource[] ambientSources; //array of audio sources for ambient sounds
	int activeAmbientSourceIndex; //index for the current ambient sound audio source

	public static AudioManagerScript instance;

	void Awake(){
		instance = this;

		ambientSources = new AudioSource[2]; //creates audio sources for ambient sounds
		for (int i = 0; i < 2; i++) {
			GameObject newAmbientSource = new GameObject("Ambient source " + (i + 1));
			ambientSources[i] = newAmbientSource.AddComponent<AudioSource>();
			newAmbientSource.transform.parent = transform;  //parents sources to audio manager
		}   
	}

	public void PlayAmbient(AudioClip clip, float fadeDuration = 1){ //for playing ambient sounds
		activeAmbientSourceIndex = 1 - activeAmbientSourceIndex; //cycles audio source index
		ambientSources[activeAmbientSourceIndex].clip = clip;//sets new active audio source as the clip to be played
		ambientSources[activeAmbientSourceIndex].Play(); //plays ambient audio source
		StartCoroutine(AmbientCrossfade(fadeDuration)); //crossfades using provided duration
	}

	public void PlaySound(AudioClip clip, Vector3 pos){ //for playing sound effects
		AudioSource.PlayClipAtPoint (clip, pos, sfxVolPerc * masterVolPerc); //plays audio clip at position, at adjusted volume
	}

	IEnumerator AmbientCrossfade(float duration){ //crossfade coroutine
		float perc = 0; //percent into fade
		while (perc < 1){ //while not 100 percent
			perc += Time.deltaTime * 1 / duration; //increase percent over time
			ambientSources[activeAmbientSourceIndex].volume = Mathf.Lerp(0, ambientVolPerc * masterVolPerc, perc); //lerp new active source, increasing volume
			ambientSources[1-activeAmbientSourceIndex].volume = Mathf.Lerp(ambientVolPerc * masterVolPerc, 0, perc);//lerp old active source, decreasing volume
			yield return null;
		}
	}
}
