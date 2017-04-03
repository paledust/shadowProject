using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;
public class ShadowTrail : MonoBehaviour {
	public DirectionOption directionOption;
	public List<DIRECTION> avaDirection;
	public bool ifEnd{get {return directionOption == DirectionOption.right||
								directionOption == DirectionOption.left||
								directionOption == DirectionOption.up||
								directionOption == DirectionOption.down||
								directionOption == DirectionOption.forward||
								directionOption == DirectionOption.back;}}
	public DirectionOption getUpperDirection()
	{
		if(directionOption == DirectionOption.right || directionOption == DirectionOption.left)
			return DirectionOption.x;
		if(directionOption == DirectionOption.up || directionOption == DirectionOption.down)
			return DirectionOption.y;
		if(directionOption == DirectionOption.forward || directionOption == DirectionOption.back)
			return DirectionOption.z;
		else 
			return DirectionOption.x;
	}
	public IEnumerable<DIRECTION> GET_DIRECTION_LIST(){return avaDirection;}
}
