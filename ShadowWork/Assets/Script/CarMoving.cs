using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoving : MonoBehaviour {
	[SerializeField] TestTimeController timeController;
	Dictionary<int, PlayerState> recording;
	Animator _animator;

	void Start()
	{
		_animator = GetComponent<Animator>();
	}

	public void SetRecording(Dictionary<int, PlayerState> recording)
	{
		this.recording = new Dictionary<int, PlayerState>(recording);
		isPlaying = true;
	}
	void Update()
	{

	}
	bool isPlaying = false;
	// Update is called once per frame
	void FixedUpdate () {
		if(isPlaying)
		{
			if(recording.ContainsKey(timeController.time))
			{
				PlayState(recording[timeController.time]);
			}
		}
	}

	void PlayState(PlayerState playerState)
	{
		transform.position = playerState.position;
		_animator.Play(playerState.animState);
		Vector3 localScale = transform.localScale;
		localScale.x = playerState.direction ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
		transform.localScale = localScale;
	}
}
