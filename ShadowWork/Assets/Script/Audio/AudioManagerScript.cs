using System.Collections;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

	float masterVolPerc = 1; //master volume
	float sfxVolPerc = 1; //sound effects volume
	float ambientVolPerc = 1; //ambient sound volume

	public ASRScript asr{get; private set;}

	AudioSource soundEffectSource;
	AudioSource[] ambientSources; //array of audio sources for ambient sounds
	int activeAmbientSourceIndex = 0; //index for the current ambient sound audio source

	AudioLibraryScript library; //reference to sound library
	static private AudioManagerScript instance;
	static public AudioManagerScript Instance;
	[SerializeField] AudioLibraryScript audioLibrary;
	void Awake(){
		if(instance != null){
			Destroy(gameObject);
		}
		else{
			instance = gameObject.GetComponent<AudioManagerScript>();
		}
		Instance = instance;

		audioLibrary.InitialAudioLibrary();
		DontDestroyOnLoad(gameObject);
		if(!GetComponent<AudioManagerScript>())
			Service.audioManager = gameObject.AddComponent<AudioManagerScript>();
		else
			Service.audioManager = GetComponent<AudioManagerScript>();

		Service.audioManager.InitialAudio(audioLibrary);

		asr = gameObject.GetComponent<ASRScript>();
	}

	public void InitialAudio(AudioLibraryScript _library){
		// library = GetComponent<AudioLibraryScript>();
		library = _library;

		ambientSources = new AudioSource[2]; //creates audio sources for ambient sounds
		for (int i = 0; i < 2; i++) {
			GameObject newAmbientSource = new GameObject("Ambient source " + (i + 1));
			ambientSources[i] = newAmbientSource.AddComponent<AudioSource>();
			newAmbientSource.transform.parent = transform;  //parents sources to audio manager
			ambientSources[i].loop = true;
		}
		GameObject newSoundEffectSource = new GameObject("Sound Effect source");
		soundEffectSource = newSoundEffectSource.AddComponent<AudioSource>();
		newSoundEffectSource.transform.parent = transform; 
	}
	public void PlayAmbient(AudioClip clip, float fadeDuration = 1){ //for playing ambient sounds
		activeAmbientSourceIndex = 1 - activeAmbientSourceIndex; //cycles audio source index
		ambientSources[activeAmbientSourceIndex].clip = clip;//sets new active audio source as the clip to be played
		ambientSources[activeAmbientSourceIndex].Play(); //plays ambient audio source
		StartCoroutine(AmbientCrossfade(fadeDuration)); //crossfades using provided duration
	}
	public void PlayAmbient(string soundName, float fadeDuration = 1){
		// Debug.Log(library.GetClipFromName(soundName));
		PlayAmbient(library.GetClipFromName(soundName), fadeDuration);
	}

	public void PlaySound(string soundName, Vector3 pos){ //for playing sound effects through audio library
		PlaySound(library.GetClipFromName(soundName), pos);//calls other playSound function with clips from library
	}

	//use the following format in other scripts to use this function
	//AudioManagerScript.instance.PlaySound("soundNameHere", positionGoesHere);

	public void PlaySound(AudioClip clip, Vector3 pos){ //for playing sound effects directly using clips
		if(clip != null){
			AudioSource.PlayClipAtPoint (clip, pos, sfxVolPerc * masterVolPerc); //plays audio clip at position, at adjusted volume
		}
	}
	//use the following format in other scripts to use this function
	//AudioManagerScript.instance.PlaySound(audioSourceGoesHere, positionGoesHere);

	public void PlaySound2D(string soundName){
		soundEffectSource.PlayOneShot(library.GetClipFromName(soundName), sfxVolPerc * masterVolPerc);
	}
	public void StopPlaying(string soundName){
		if(soundEffectSource.isPlaying)
			soundEffectSource.Stop();
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
