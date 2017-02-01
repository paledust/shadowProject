using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimeController : MonoBehaviour {
	public int time;
	bool isForward = true;
	// Update is called once per frame
	void FixedUpdate () {
		if(isForward)
		{
			time ++;
		}

		if(!isForward)
		{
			if(time > 0)
				time --;
		}
	}

	public void SetRewind(bool m_isforward)
	{
		isForward = m_isforward;
	}
}
