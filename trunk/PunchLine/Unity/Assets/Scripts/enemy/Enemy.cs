using UnityEngine;
using System.Collections;

public class Enemy : Entity 
{	
	public bool IsInvulnerable 
	{
		get 
		{
			return hurtTimer < invulnerabilityDuration;
		}
	}
	
	private Vector2 hurtMovement;
	private float hurtTimer;
	private Transform target;
	private float pushBackTimer;
	
	new void Awake()
	{
		base.Awake();
	}
	
	void Start()
	{
		Faction = -1;
		hurtTimer = invulnerabilityDuration;
		target = GameObject.Find("Player").transform;
	}
	
	protected override void EntityFixedUpdate()
	{
		// If we're hurt (falling back) 
		// cannot attack or move.
		if(pushBackTimer < pushBackDuration)
		{
			HandleHurtMovement();
		}
		else 
		{
			HandleMovement();
		}
		
		if(hurtTimer < invulnerabilityDuration)
		{
			hurtTimer += Time.fixedDeltaTime;
		}
		
		if(pushBackTimer < pushBackDuration)
		{
			pushBackTimer += Time.fixedDeltaTime;
		}
	}
	
	
	void HandleHurtMovement()
	{
		base.MoveWithSliding(
			new Vector3(
				hurtMovement.x * Time.fixedDeltaTime,
				hurtMovement.y * Time.fixedDeltaTime,
				0));
	}
	
	void HandleMovement()
	{
		Vector3 direction = target.transform.position - this.transform.position;
		direction = direction.normalized * moveSpeed * Time.fixedDeltaTime;
		
		base.MoveWithSliding(
			new Vector3(
				direction.x,
				direction.y,
				0));
	}
	
	public void TakeDamage(int damage)
	{
	}
	
	public override void TouchedByEntity (Entity other)
	{
		// do nothing
	}
	
	public override void TouchedByWeapon (Weapon other)
	{
		if(other.owner.Faction != this.Faction && !IsInvulnerable)
		{
			// take damage, knockback
			TakeDamage(other.Strength);
			
			// fly backward
			hurtTimer = 0;
			pushBackTimer = 0;
			Vector2 direction;
			direction.x = this.transform.position.x - other.transform.position.x;
			direction.y = this.transform.position.y - other.transform.position.y;
			hurtMovement = direction.normalized * hurtSpeed;
		}
	}
	
	public override void WeaponTouchedByWeapon (Weapon other)
	{
		if(other.owner.Faction != this.Faction && 
			!IsInvulnerable)
		{			
			// fly backward
			pushBackTimer = 0;
			Vector2 direction;
			direction.x = this.transform.position.x - other.transform.position.x;
			direction.y = this.transform.position.y - other.transform.position.y;
			hurtMovement = direction.normalized * hurtSpeed;
		}
	}
	
	public override void TouchedByWall(Collider other)
	{
		
	}
}