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
	public float pursuitTime;
	public float preferredFireDistance;

	public float projectileVelocity;

	public StraightFlyEnemy projectilePrefab;
	private Animator animator;
	private Transform player;


	protected override void Init()
	{
		animator = this.GetComponent<Animator>();
		player = Player.Instance.transform;
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
		DoFacing();
	}
	
	void DoFacing()
	{
		Vector3 facingVector = player.transform.position - this.transform.position;
		EntityFacing facing = Facing.DirectionToFacing (facingVector);
		animator.Play("ArcherWalk"+facing.ToString());
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
		
//		if (modeTime > pursuitTime)
//		{
//      }
		
		if (modeTime > pursuitTime)
		{
			Mode = AIMode.Aim;
			//lock in facing
			facing = Facing.DirectionToFacing(Player.Instance.transform.position - this.transform.position);
		}
		else
		{
			destination = FindClosestAlignedDistance (this.transform.position, Player.Instance.transform.position, preferredFireDistance);
			Vector3 movement = destination - this.transform.position;
			float timedPositioningSpeed = moveSpeed * Time.fixedDeltaTime;
			if (movement.magnitude < timedPositioningSpeed)
			{
				Mode = AIMode.Aim;
				//lock in facing
				facing = Facing.DirectionToFacing(Player.Instance.transform.position - this.transform.position);
			}
			else
			{
				movement = movement.normalized * timedPositioningSpeed;
			}
			base.MoveWithSliding(movement);
		}
	}

	private void Aim()
	{
		if (modeTime > aimTime)
		{
			Fire();
			Mode = AIMode.Cooldown;
		}
	}

	private void Cooldown()
	{
		if (modeTime > postFireCooldown)
		{
			Mode = AIMode.Pursue;
		}
	}

	private void Patrol()
	{
	}

	private void Fire()
	{
		if(projectilePrefab)
		{
			StraightFlyEnemy firedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as StraightFlyEnemy;
			firedProjectile.velocity = Facing.FacingToUnitVector3(facing)*projectileVelocity;
		}
	}

	public override void TouchedByWall (Collider other)
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
