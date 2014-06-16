using UnityEngine;
using System.Collections;

public class StraightFlyEnemy : Enemy {
	public enum AIMode
	{
		Go, Stop
	}

	private AIMode mode;
	private AIMode Mode { get { return mode; } set { mode = value; modeTime = 0f; } }
	public float modeTime;
	
	public Vector3 velocity;
	public float timeBeforeDisappearing;

	public Collider trigger;
	public string DeathAnimationName;

	protected override void Init()
	{
		Mode = AIMode.Go;
	}

	protected override void RunAI()
	{
		modeTime += Time.fixedDeltaTime;
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
		characterController.Move(velocity*Time.fixedDeltaTime);
	}

	private void Stop()
	{
		if (modeTime > timeBeforeDisappearing)
		{
			Destroy(this.gameObject);
		}
		else if(!string.IsNullOrEmpty(DeathAnimationName)) 
		{
			animator.Play (DeathAnimationName);
		}
	}

	public override void TouchedByWall(Collider other)
	{
		if (Mode != AIMode.Stop)
		{
			trigger.enabled = false;
			Mode = AIMode.Stop;
		}
	}

	public override void TouchedByEntity (Entity other)
	{
		if (Mode == AIMode.Go)
		{
			if (other is Player)
			{
				Destroy (this.gameObject);
			}
		}
	}
}
