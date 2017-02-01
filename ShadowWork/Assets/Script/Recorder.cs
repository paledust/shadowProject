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
			timeController.time = 0;
		}
	}
	bool isRecording = true;
	// Update is called once per frame
	void FixedUpdate () {
		if(isRecording)
		{
			state.Add(timeController.time, new PlayerState(transform.position, 
															_animator.GetCurrentAnimatorStateInfo(0).shortNameHash,
															transform.localScale.x > 0));
		}
	}
}
