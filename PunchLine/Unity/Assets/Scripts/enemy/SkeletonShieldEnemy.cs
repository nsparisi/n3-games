using UnityEngine;
using System.Collections;

public class SkeletonShieldEnemy : Enemy 
{	
	public float timeBeforeCharge = 1.0f;
	public float distanceToMaintain = 200;
	public float catchUpSpeedCoefficient = 1.5f;
	public float shadowSpeedCoefficient = 0.9f;
	public float tooFarAway = 300;

	private Transform player;
	private Vector3 targetDirectionFromPlayer;
	private Vector3 destination;
	private EntityFacing facing;

	EnemyAIUpdateFunction currentUpdate;

	protected override void Init ()
	{
		player = Player.Instance.transform;
		currentUpdate = DoNothing;
		BeginShadowPlayer();
	}

	protected override void RunAI ()
	{
		currentUpdate();
		DoFacing();
	}

	void DoFacing()
	{
		Vector3 facingVector = player.transform.position - this.transform.position;
		
		if( Mathf.Abs(facingVector.x) > Mathf.Abs(facingVector.y)) 
		{
			if(facingVector.x > 0)
			{
				facing = EntityFacing.Right;
			}
			else 
			{
				facing = EntityFacing.Left;
			}
		} else
		{
			if(facingVector.y > 0)
			{
				facing = EntityFacing.Up;
			}
			else 
			{
				facing = EntityFacing.Down;
			}
		}

		animator.Play("skeletonwalk"+facing.ToString().ToLower());
	}

	void DoNothing()
	{
	}

	void BeginCatchUpToPlayer()
	{
		currentUpdate = DoNothing;
		StartCoroutine(WaitBeforeCatchUp());
	}
	
	IEnumerator WaitBeforeCatchUp()
	{
		yield return new WaitForSeconds(timeBeforeCharge);
		currentUpdate = CatchUpToPlayerUpdate;
		
		// determine destination
		if( Mathf.Abs(this.transform.position.x - player.position.x) < 
		   Mathf.Abs(this.transform.position.y - player.position.y))
		{
			float xDistance = this.transform.position.x - player.position.x;
			targetDirectionFromPlayer = 
				xDistance == 0 ? 
					Vector3.right : 
					Vector3.right * (this.transform.position.x - player.position.x);
			targetDirectionFromPlayer = targetDirectionFromPlayer.normalized;
		}
		else 
		{
			float yDistance = this.transform.position.y - player.position.y;
			targetDirectionFromPlayer = 
				yDistance == 0 ? 
					Vector3.up : 
					Vector3.up * (this.transform.position.y - player.position.y);
			targetDirectionFromPlayer = targetDirectionFromPlayer.normalized;
		}
	}

	void CatchUpToPlayerUpdate()
	{
		destination = player.position + targetDirectionFromPlayer * distanceToMaintain;

		Vector3 movement = destination - this.transform.position;
		movement = movement.normalized * moveSpeed * catchUpSpeedCoefficient * Time.fixedDeltaTime;

		// if we're close enough, don't overshoot
		if(UsefulStuff.CloseEnough(this.transform.position, destination, movement.sqrMagnitude))
		{
			BeginShadowPlayer();
			movement = destination - this.transform.position;
		}
		
		base.MoveWithSliding(
			new Vector3(
			movement.x,
			movement.y,
			0));
	}

	void BeginShadowPlayer()
	{
		currentUpdate = ShadowPlayerUpdate;
	}

	void ShadowPlayerUpdate()
	{
		// maybe change logic here?
		destination = player.position + targetDirectionFromPlayer * distanceToMaintain;
		
		Vector3 movement = destination - this.transform.position;
		movement = movement.normalized * moveSpeed * shadowSpeedCoefficient * Time.fixedDeltaTime;
		
		// if we're close enough, don't overshoot
		if(UsefulStuff.CloseEnough(this.transform.position, destination, movement.sqrMagnitude))
		{
			movement = destination - this.transform.position;
		}
		else if(UsefulStuff.FarEnough(this.transform.position, destination, tooFarAway * tooFarAway))
		{
			BeginCatchUpToPlayer();
		}
		
		base.MoveWithSliding(
			new Vector3(
			movement.x,
			movement.y,
			0));
	}

	void BeginBigSwing()
	{
	}

	void BigSwingUpdate()
	{
	}
}