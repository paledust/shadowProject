using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CS_Kevin {
	public class DirectionCheck
	{
		public bool ifX{get {return ifRight && ifLeft;}}
		public bool ifY{get {return ifUp && ifDown;}}
		public bool ifZ{get {return ifForward && ifBack;}}
		public bool ifRight;
		public bool ifLeft;
		public bool ifUp;
		public bool ifDown;
		public bool ifForward;
		public bool ifBack;
	}

	public enum DirectionOption
	{
		left,
		right,
		up,
		down,
		forward,
		back,
		x,
		y,
		z
	}
}
