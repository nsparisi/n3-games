using UnityEngine;
using System.Collections;

public class Pot : Weapon 
{
	public bool isFlying {get; set;}
	public AnimationCurve heightOverTime;
	public float ThrowSpeed = 400f;
	public float ThrowDuration = 0.4f;

	Vector3 velocity;
	float throwTimer = 0;

	public void PickUp(Player player)
	{
		foreach(Collider collider in this.GetComponentsInChildren<Collider>())
		{
			collider.enabled = false;
		}

		this.collider.enabled = true;
		owner = player;
		transform.parent = player.potHolder;
		transform.localPosition = Vector3.zero;
	}

	public void Throw()
	{
		transform.parent = null;
		velocity = ((Player) owner).GetFacingAsAVector() * ThrowSpeed;
		isFlying = true;
	}

	public void TouchedByEntity(Entity entity)
	{
		if(owner != null && entity.Faction != owner.Faction)
		{
			if(isFlying)
			{
				Break();
			}
		}
	}

	public void TouchedByWeapon(Weapon weapon)
	{
		if(owner != null && weapon.owner.Faction != owner.Faction)
		{
			if(isFlying)
			{
				Break();
			}
		}
	}

	public void TouchedByWall()
	{
		if(isFlying)
		{
			Break();
		}
	}

	void FixedUpdate()
	{
		if(isFlying)
		{
			throwTimer += Time.fixedDeltaTime;
			if(throwTimer > ThrowDuration)
			{
				Break();
			}

			else if(velocity.x != 0)
			{
				velocity.y = heightOverTime.Evaluate(throwTimer) * ThrowSpeed;
			}
			this.transform.Translate(velocity * Time.fixedDeltaTime);
		}
	}

	public void Break()
	{
		Destroy(this.gameObject);
	}

}
