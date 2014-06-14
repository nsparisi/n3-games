using UnityEngine;
using System.Collections;

public class StraightFlyEnemy : Enemy {
	public enum AIMode
	{
		Go, Stop
	}

	private AIMode mode;
	private AIMode Mode { get { return mode; } set { mode = value; modeTime = 0f; } }
	private float modeTime;
	
	public Vector3 velocity;

	protected override void Init ()
	{
		Mode = AIMode.Go;
	}

	protected override void RunAI ()
	{
		modeTime += Time.deltaTime;
		switch(mode)
		{
		case AIMode.Go:
			Go();
			break;
		case AIMode.Stop:
			Stop();
			break;
		}
	}

	private void Go()
	{
		transform.Translate(velocity);
	}

	private void Stop()
	{
		return;
	}

	public override void TouchedByWall (Collider other)
	{
		Mode = AIMode.Stop;
	}
}
