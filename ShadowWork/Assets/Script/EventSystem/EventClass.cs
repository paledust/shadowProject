using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Event {
	public delegate void Handler(Event e);
}

public class PullEvent: Event{

}

public class CompleteEvent: Event{
	public int NextLevelIndex = 0;
}

public class RestartEvent: Event{
	
}

public class CameraMoveEvent: Event{
	public CameraMoveInfo camMoveInfo = new CameraMoveInfo();
}

public class changeDirLightEvent: Event{
	public RotationInfo dirRotationInfo = new RotationInfo();
}

public class swithDirLightEvent: Event{
	public Light light_Start;
	public Light light_End;
}

