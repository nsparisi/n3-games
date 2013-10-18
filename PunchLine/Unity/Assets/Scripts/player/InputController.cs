using System;
using UnityEngine;
using System.Collections.Generic;

public class InputController
{
	Dictionary<string, List<KeyCode>> mappings;
	
	public InputController()
	{
		mappings = new Dictionary<string, List<KeyCode>>();
	}
	
	public void RegisterAction(string action, params KeyCode[] keys )
	{
		if(!mappings.ContainsKey(action))
		{
			mappings.Add(action, new List<KeyCode>());
		}
		
		mappings[action].Clear();
		foreach(KeyCode key in keys)
		{
			mappings[action].Add(key);
		}
	}
	
	public bool GetAction(string action)
	{
		if(mappings.ContainsKey(action))
		{
			foreach(KeyCode key in mappings[action])
			{
				if(Input.GetKey(key))
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	public bool GetActionDown(string action)
	{
		if(mappings.ContainsKey(action))
		{
			foreach(KeyCode key in mappings[action])
			{
				if(Input.GetKeyUp(key))
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	public bool GetActionUp(string action)
	{
		if(mappings.ContainsKey(action))
		{
			foreach(KeyCode key in mappings[action])
			{
				if(Input.GetKeyDown(key))
				{
					return true;
				}
			}
		}
		
		return false;
	}
}

