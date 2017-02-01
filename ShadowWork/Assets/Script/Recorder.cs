using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerState
{
	public Vector3 position;
	public int animState;
	public bool direction;

	public PlayerState(Vector3 position, int animState, bool direction)
	{
		this.position = position;
		this.animState = animState;
		this.direction = direction;
	}
}

public class Recorder : MonoBehaviour {
	[SerializeField] TestTimeController timeController;
	
	Animator _animator;
	Dictionary<int, PlayerState> state = new Dictionary<int, PlayerState>();
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.K))
		{
			isRecording = false;
			isPlaying = true;
			timeController.SetRewind(false);
			//timeController.time = 0;
		}

		if(Input.GetKeyUp(KeyCode.K))
		{
			isRecording = true;
			isPlaying = false;
			timeController.SetRewind(true);
		}
	}
	bool isRecording = true;
	bool isPlaying = false;
	// Update is called once per frame
	void FixedUpdate () {
		if(isRecording)
		{
			if(!state.ContainsKey(timeController.time))
				state.Add(timeController.time, new PlayerState(transform.position, 
															_animator.GetCurrentAnimatorStateInfo(0).shortNameHash,
															transform.localScale.x > 0));
			else
				state[timeController.time] = new PlayerState(transform.position, 
															_animator.GetCurrentAnimatorStateInfo(0).shortNameHash,
															transform.localScale.x > 0);
			// state.Add(timeController.time, new PlayerState(transform.position, 
			// 												_animator.GetCurrentAnimatorStateInfo(0).shortNameHash,
			// 												transform.localScale.x > 0));
		}

		if(isPlaying)
		{
			if(state.ContainsKey(timeController.time))
			{
				PlayState(state[timeController.time]);
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
		state.Remove(timeController.time);
	}
}
