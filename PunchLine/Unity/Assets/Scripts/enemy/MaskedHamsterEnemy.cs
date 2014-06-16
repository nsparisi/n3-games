using UnityEngine;
using System.Collections;

public class MaskedHamsterEnemy : Enemy {
	public float acceleration;
	public float maxSpeed;
	public Vector3 velocity;

	public float skidValue;

	protected override void RunAI ()
	{
		//accelerate towards player
		Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
		Vector3 accelerationVector = direction*acceleration;
		velocity += accelerationVector*Time.fixedDeltaTime;

		float dotProduct = Vector3.Dot(direction, velocity);
		//velocity moves away from destination, skid
		if (dotProduct < 0)
		{
			velocity -= accelerationVector*Time.fixedDeltaTime;
			velocity = velocity*Mathf.Pow(skidValue, Time.fixedDeltaTime);
		}
		else
		{
			if (velocity.magnitude > maxSpeed)
			{
				velocity = velocity.normalized*maxSpeed;
			}
		}

		base.MoveWithSliding(velocity*Time.fixedDeltaTime);
	}
}
