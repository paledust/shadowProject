using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ASRScript : MonoBehaviour
{
	public AudioClip AttackClip;
	public AudioClip SustainClip;
	public AudioClip ReleaseClip;

	private AudioSource as1;
	private AudioSource as2;

	private bool sustaining;
	private bool release;
	private double timeOfAttack;
	private int sustainLoopCount;

	void Start()
	{
		as1 = GetComponent<AudioSource>();
		as2 = gameObject.AddComponent<AudioSource>();
		as1.playOnAwake = false;
		as2.playOnAwake = false;
		Init();
	}

	void Init()
	{
		as1.clip = AttackClip;
		as2.clip = SustainClip;
		as2.loop = true;
		sustaining = false;
		release = false;
		timeOfAttack = 0;
		sustainLoopCount = 0;
	}

	public void Play()
	{
		StartCoroutine(AttackRelease());
		Debug.Log("Playing");
	}

	public void Stop()
	{
		release = true;
	}

	IEnumerator AttackRelease()
	{
		timeOfAttack = AudioSettings.dspTime + 100 / AudioSettings.outputSampleRate;
		as1.PlayScheduled(timeOfAttack);
		as2.PlayScheduled(timeOfAttack + AttackClip.length);
		yield return new WaitForSeconds(AttackClip.length * 1.01f);
		sustaining = true;
		as1.clip = ReleaseClip;
		yield return StartCoroutine(Sustain());
		as2.loop = false;
		as2.SetScheduledEndTime(timeOfAttack + AttackClip.length + SustainClip.length * sustainLoopCount);
		as1.PlayScheduled(timeOfAttack + AttackClip.length + SustainClip.length * sustainLoopCount);
		sustaining = false;
		yield return new WaitForSeconds(ReleaseClip.length);
		Init();
	}

	IEnumerator Sustain()
	{
		sustainLoopCount++;
		yield return new WaitForSeconds(SustainClip.length);
		if (release)
		{
			yield return null;
		}
		else
		{
			yield return StartCoroutine(Sustain());
		}
	}
}
