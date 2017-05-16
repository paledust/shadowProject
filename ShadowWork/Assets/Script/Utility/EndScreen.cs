using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Kevin_Event;


public class EndScreen : Main {

	[SerializeField] AnimationCurve increaseIntensity;
	[SerializeField] float waitTimeDots = 0.5f;
	[SerializeField] float waitTimeShadows = 0f;
	[SerializeField] float waitTimeCreditsAnim = 0.5f;
	bool fadeInStarted = false;
	float animTimer = 0.0f;
	// Use this for initialization
	void Awake(){
		base.Awake();

	}

	void Start () {
		Service.eventManager.Register<RestartEvent>(RestartLevelHandler);
		Service.eventManager.Register<LoadLevelEvent>(LoadNextLevelHandler);
		StartCoroutine(FadeInDots(waitTimeDots));

		//StartCoroutine(AnimateCredits(waitTimeCreditsAnim));
	}
	
	// Update is called once per frame

	IEnumerator FadeInDots(float _waitTime){
		yield return new WaitForSeconds(_waitTime);
		for(float i = 0.0f; i < 1.0f; i += 0.4f * Time.deltaTime){
			//Debug.Log("ADSASD");
			backGround.GetComponentInChildren<UnityEngine.UI.Image>().color = Color.Lerp(Color.black, Color.clear,i);
			if (i > 0.1f && fadeInStarted == false){
				StartCoroutine(FadeInShadows(waitTimeShadows));
				fadeInStarted = true;
			}
			yield return null;
		}

		//StartCoroutine(FadeInShadows(waitTimeShadows));

		yield return new WaitForSeconds(Camera_WaitTime);

		if(Camera.main.GetComponent<CameraManager>())
			Camera.main.GetComponent<CameraManager>().CameraAnimationTrigger();
		
		yield return null;
	}

	IEnumerator FadeInShadows(float _waitTime){
		
		yield return new WaitForSeconds(_waitTime);

		for(float i = 0.0f; i < 1.0f; i += 0.4f * Time.deltaTime){
			//Debug.Log ("fade shadows");
			//Service.ActiveDirLight.GetComponent<Light>().intensity = Mathf.Lerp(0.0f, 1.0f, i);
			Service.ActiveDirLight.GetComponent<Light>().intensity = increaseIntensity.Evaluate(i);
			yield return null;
		}
	}

	IEnumerator AnimateCredits(float _waitTime){
		yield return new WaitForSeconds(_waitTime);

		yield return null;
	}

}
