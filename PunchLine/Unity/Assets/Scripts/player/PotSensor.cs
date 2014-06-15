using UnityEngine;
using System.Collections;

public class PotSensor : MonoBehaviour {
	
	public Pot TouchingPot { get; private set; }
	
	bool waitAFrame;
	
	void FixedUpdate()
	{
		if (!waitAFrame)
		{
			TouchingPot = null;
		} 
		waitAFrame = false;
	}
	
	void OnTriggerStay (Collider other)
	{
		waitAFrame = true;

		TouchingPot = other.GetComponent<Pot>();
	}
}
