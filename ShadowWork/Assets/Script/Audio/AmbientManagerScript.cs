using UnityEngine;

public class AmbientManagerScript : MonoBehaviour {

	public AudioClip ambientA;
	public AudioClip ambientB;

	void Start(){
		AudioManagerScript.Instance.PlayAmbient(ambientA,2);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			AudioManagerScript.Instance.PlayAmbient (ambientB, 2);
		}
	}
}
