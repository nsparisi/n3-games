using UnityEngine;
using System.Collections;

public class FollowPlayerEnemy : Enemy 
{	
	private Transform target;

	EnemyAIUpdateFunction currentUpdate;

	protected override void Init ()
	{
		target = Player.Instance.transform;
		currentUpdate = FollowPlayer;
	}

	protected override void RunAI ()
	{
		currentUpdate();
	}

	void DoNothing()
	{
	}

	void FollowPlayer()
	{
		Vector3 direction = target.transform.position - this.transform.position;
		direction = direction.normalized * moveSpeed * Time.fixedDeltaTime;
		
		base.MoveWithSliding(
			new Vector3(
			direction.x,
			direction.y,
			0));
	}
}