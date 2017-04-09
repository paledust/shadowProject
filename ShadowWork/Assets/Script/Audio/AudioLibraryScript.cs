using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibraryScript : MonoBehaviour {

	public SoundGroup[] soundGroups;

	Dictionary<string, AudioClip[]> soundGroupDictionary = new Dictionary<string, AudioClip[]>(); //dictionary of sounds

	void Awake(){
		foreach(SoundGroup soundGroup in soundGroups){ //loops through sound groups in our array
			soundGroupDictionary.Add(soundGroup.soundGroupID, soundGroup.group); //adds them to dictionary using the groupID as the key
		}
	}

	public AudioClip GetClipFromName(string name){ //uses string to retrieve sound clip from dictionary
		if (soundGroupDictionary.ContainsKey (name)){ //if dictionary contains the sound group
			AudioClip[] sounds = soundGroupDictionary [name]; //takes array of sound clips using group name
			return sounds [Random.Range (0, sounds.Length)]; //returns a random one of those clips
		}
		return null;
	}

	[System.Serializable]
	public class SoundGroup{ //group of sounds to be used interchangeably
		public string soundGroupID; //id for the group
		public AudioClip[] group; //the actual array of audioclips
	}
}
