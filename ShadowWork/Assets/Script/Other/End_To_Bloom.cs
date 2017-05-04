using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_To_Bloom : MonoBehaviour {
	[SerializeField] AnimationCurve BloomCurve;
	private AmplifyBloom.AmplifyBloomEffect amplify;
	void Start () {
		amplify = GetComponent<AmplifyBloom.AmplifyBloomEffect>();
		Service.eventManager.Register<Kevin_Event.EndGame_Event>(End_Bloom);
	}
	
	// Update is called once per frame
	void End_Bloom(Kevin_Event.Event e){
		StartCoroutine(Bloom());
	}
	IEnumerator Bloom(){
		yield return new WaitForSeconds(0.5f);
		for(float i = 0; i< 2.0f; i += Time.deltaTime){
			amplify.OverallIntensity = BloomCurve.Evaluate(i);
			yield return null;
		}
	}
}
