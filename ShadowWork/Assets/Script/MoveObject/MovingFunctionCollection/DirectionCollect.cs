using UnityEngine;

namespace CS_Kevin {
	public enum MOVESTATE{
		MOVEABLE,
		MOVING,
		PULLING,
		FROZEN,
	}
	public enum FACING_DIRECTIOM{
		X,
		Y,
		Z
	}
	public class DirectionCheck{
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
	public enum DirectionOption{
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
	public enum DIRECTION {LEFT,RIGHT,UP,DOWN,FORWARD,BACK,EMPTY};
	public static class HandleDirection{
		static public Vector3 DIRECTION_To_VECTOR(DIRECTION direction){
			switch (direction)
			{
				case DIRECTION.FORWARD:
					return Vector3.forward;
				case DIRECTION.BACK:
					return Vector3.back;
				case DIRECTION.RIGHT:
					return Vector3.right;
				case DIRECTION.LEFT:
					return Vector3.left;
				case DIRECTION.UP:
					return Vector3.up;
				case DIRECTION.DOWN:
					return Vector3.down;
				default:
					return Vector3.zero;
			}
		}
	}
}
