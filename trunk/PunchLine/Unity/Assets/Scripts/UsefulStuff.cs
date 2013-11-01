using System;
using UnityEngine;

public class UsefulStuff
{
	public static bool CloseEnough(float a, float b, float leeway)
	{
		return Math.Abs(a - b) < leeway;
	}
}

