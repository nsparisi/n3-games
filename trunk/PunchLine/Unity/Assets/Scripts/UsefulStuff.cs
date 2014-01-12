using System;
using UnityEngine;

public class UsefulStuff
{
	public static bool CloseEnough(float a, float b, float leeway)
	{
		return Math.Abs(a - b) <= leeway;
	}

	public static bool CloseEnough(Vector3 a, Vector3 b, float leewaySquared)
	{
		return (a - b).sqrMagnitude <= leewaySquared;
	}

	public static bool FarEnough(Vector3 a, Vector3 b, float leewaySquared)
	{
		return (a - b).sqrMagnitude > leewaySquared;
	}

}

