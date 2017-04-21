using System;
using System.Collections.Generic;
using Kevin_Event;

public class EventManager {
	static private EventManager instance;
	static public EventManager Instance{
		get{
			if(instance == null)
				instance = new EventManager();
			return instance;
		}
	}

	private Dictionary<Type, Event.Handler> RegisteredHandlers = new Dictionary<Type, Event.Handler>();

	public void Register<T>(Event.Handler handler) where T: Event {
		Type  type = typeof(T);

		if(RegisteredHandlers.ContainsKey(type))
		{
			RegisteredHandlers[type] += handler;
		}
		else
		{
			RegisteredHandlers[type] = handler;
		}
	}

	public void UnRegister<T>(Event.Handler handler) where T: Event
	{
		Type type = typeof(T);
		Event.Handler handlers;

		if(RegisteredHandlers.TryGetValue(type, out handlers))
		{
			handlers -= handler;
			if(handlers == null)
			{
				RegisteredHandlers.Remove(type);
			}
			else
			{
				RegisteredHandlers[type] = handlers;
			}
		}
	}
	public void ClearList()
	{
		RegisteredHandlers.Clear();
	}
	public void FireEvent(Event e)
	{
		Type type = e.GetType();
		Event.Handler handlers;
		if(RegisteredHandlers.TryGetValue(type, out handlers))
		{
			handlers(e);
		}
	}
}
