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
	public float timeBeforeDisappearing;

	protected override void Init()
	{
		Mode = AIMode.Go;
	}

	protected override void RunAI()
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
		transform.Translate(velocity*Time.fixedDeltaTime);
	}

	private void Stop()
	{
		if (modeTime > timeBeforeDisappearing)
		{
			Destroy(this.gameObject);
		}
	}

	public override void TouchedByWall(Collider other)
	{
		Mode = AIMode.Stop;
	}

	public override void TouchedByEntity (Entity other)
	{
		print("Bang: " + other.name);
		if (other is Player)
		{
			Destroy (this.gameObject);
		}
	}
}
