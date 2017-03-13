using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Event {
	public delegate void Handler(Event e);
}

public class PullEvent: Event{

}

public class CompleteEvent: Event{
	
}

public class RestartEvent: Event{
	
}

