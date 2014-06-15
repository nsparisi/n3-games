using UnityEngine;
using System.Collections;

public class OctorokEnemy : Enemy {
	public enum AIMode { 
		Move,
		Stop,
		Fire
	}
	public AIMode mode;
	public AIMode Mode { get { return mode; } set { mode = value; modeTime = 0; } }
	public float modeTime;

	public float minTravelTime;
	public float maxTravelTime;
	public float chosenTravelTime;

	public float fireWait;

	public int shotCount;
	public int maxShots;

	public EntityFacing facing;

	public float projectileSpeed;
	public StraightFlyEnemy projectilePrefab;

	public Vector3 trajectory;

	protected override void RunAI ()
	{
		modeTime += Time.fixedDeltaTime;
		switch(mode)
		{
		case AIMode.Move:
			Move();
			break;
		case AIMode.Fire:
			Firing();
			break;
		case AIMode.Stop:
			break;
		}
	}

	private void StartMove()
	{
		Mode = AIMode.Move;
		//choose direction
		facing = Facing.RandomFacingThatIsntThisOne(facing);
		trajectory = Facing.FacingToUnitVector3(facing);
		//choose length of travel
		chosenTravelTime = Random.Range(minTravelTime, maxTravelTime);
	}

	private void Move()
	{
		if (modeTime > chosenTravelTime)
		{
			StartFiring();
		}
		Vector3 positionBefore = transform.position;
		base.MoveWithSliding(trajectory*Time.fixedDeltaTime*moveSpeed);
		Vector3 positionAfter = transform.position;

		if (positionBefore == positionAfter)
		{
			facing = Facing.OppositeFacing(facing);
			trajectory = Facing.FacingToUnitVector3(facing);
		}
	}

	private void StartFiring()
	{
		Mode = AIMode.Fire;
		maxShots = Random.Range(0,2)==0?1:7;
	}

	private void Firing()
	{
		if (modeTime > fireWait)
		{
			if (shotCount > maxShots)
			{
				shotCount = 0;
				StartMove ();
			}
			else
			{
				//shoot and turn clockwise
				shotCount++;
				FireProjectile();
				facing = Facing.RotateClockwise(facing);
				modeTime = 0f;
			}
		}
	}

	private void FireProjectile()
	{
		StraightFlyEnemy firedProjectile = Instantiate (projectilePrefab, this.transform.position, Quaternion.identity) as StraightFlyEnemy;
		firedProjectile.velocity = Facing.FacingToUnitVector3(facing)*projectileSpeed;
	}

	public override void TouchedByWall (Collider other)
	{
//		if (Mode == AIMode.Move)
//		{
//			facing = Facing.OppositeFacing(facing);
//			trajectory = Facing.FacingToUnitVector3(facing);
//			print (facing.ToString () + " going " + trajectory);
//		}
	}
}
