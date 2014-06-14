using UnityEngine;
using System.Collections;

public class ArcherEnemy : Enemy
{

	public enum AIMode
	{
		Pursue,
		Aim,
		Cooldown,
		Patrol
	}

	public EntityFacing facing;

	public float aimTime;
	public float patrolSpeed;
	public float postFireCooldown;
	public float missileSpeed;
	public AIMode aiMode;
	public AIMode Mode { 
		get { 
			return aiMode;
		}
		set {
			aiMode = value;
			modeTime = 0;
		}
	}

	private float modeTime;

	//pursue
	private Vector3 destination;
	public float positioningSpeed;
	public float preferredFireDistance;

	public StraightFlyEnemy projectilePrefab;

	protected override void Init()
	{

	}

	protected override void RunAI()
	{
		modeTime += Time.deltaTime;
		switch (aiMode) {
		case AIMode.Pursue:
			PursuePlayer();
			break;
		case AIMode.Aim:
			Aim();
			break;
		case AIMode.Cooldown:
			Cooldown();
			break;
		case AIMode.Patrol:
			Patrol();
			break;
		}
	}

	public Vector3 FindClosestAlignedDistance(Vector3 origin, Vector3 destination, float distance)
	{
		Vector3 diff = destination - origin;
		//for aligning along the X axis (same y)
		Vector3 xAlignedDiff = diff;
		//for aligning along the Y axis (same x)
		Vector3 yAlignedDiff = diff;

		//if the origin is to the left of the destination, SUBTRACT distance for the spacing spot to be to the left, else ADD distance
		if (diff.x > 0)
		{
			xAlignedDiff.x = xAlignedDiff.x - distance;
		}
		else
		{
			xAlignedDiff.x = xAlignedDiff.x + distance;
		}

		if (diff.y > 0)
		{
			yAlignedDiff.y = yAlignedDiff.y - distance;
		}
		else
		{
			yAlignedDiff.y = yAlignedDiff.y + distance;
		}

		if (xAlignedDiff.sqrMagnitude < yAlignedDiff.sqrMagnitude)
		{
			return xAlignedDiff + origin;
		}
		else
		{
			return yAlignedDiff + origin;
		}
	}

	private void PursuePlayer()
	{
		destination = FindClosestAlignedDistance (this.transform.position, Player.Instance.transform.position, preferredFireDistance);
		
		Vector3 movement = destination - this.transform.position;
		if (movement.sqrMagnitude < positioningSpeed*positioningSpeed)
		{
			Mode = AIMode.Aim;
			//lock in facing
			facing = Facing.DirectionToFacing(Player.Instance.transform.position - this.transform.position);
			print ("aiming");
		}
		else
		{
			movement = movement.normalized * positioningSpeed;
		}

		base.MoveWithSliding(movement);
	}

	private void Aim()
	{
		if (modeTime > aimTime)
		{
			print ("bang");
			Fire();
			Mode = AIMode.Cooldown;
		}
	}

	private void Cooldown()
	{
		if (modeTime > postFireCooldown)
		{
			print ("ready for more");
			Mode = AIMode.Pursue;
		}
	}

	private void Patrol()
	{
	}

	private void Fire()
	{

	}

	private void OnDrawGizmos()
	{
		switch(aiMode)
		{
			case AIMode.Pursue:
				Gizmos.color = Color.green;
				Gizmos.DrawSphere(destination, 10);
				break;
		}
	}
}
