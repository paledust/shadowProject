using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public abstract class Event {
	public delegate void Handler(Event e);
}

public class PullEvent: Event{

}

public class LoadLevelEvent: Event{
	public int NextLevelIndex = 0;
}

public class RestartEvent: Event{
	
}

public class CameraMoveEvent: Event{
	public MoveInfo camMoveInfo = new MoveInfo();
}

public class changeDirLightEvent: Event{
	public RotationInfo dirRotationInfo = new RotationInfo();
}

public class swithDirLightEvent: Event{
	public Light light_Start;
	public Light light_End;
}

public class ButtonEvent_Right: Event{

}
public class ButtonEvent_Left: Event{

}
public class ButtonEvent_Up: Event{

}
public class ButtonEvent_Down: Event{

}
public class ButtonEvent_Forward: Event{

}
public class ButtonEvent_Back: Event{

}

