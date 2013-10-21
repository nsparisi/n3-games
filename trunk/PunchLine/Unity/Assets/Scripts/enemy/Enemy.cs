using UnityEngine;
using System.Collections;

public class Enemy : Being 
{
	public float hurtSpeed = 400;
	public float hurtDuration = 0.3f;
	public float invulnerabilityDuration = 0.3f;
	
	public bool IsInvulnerable 
	{
		get 
		{
			return hurtTimer < invulnerabilityDuration;
		}
	}
	
	private Vector2 hurtMovement;
	private float hurtTimer;
	
	void Start()
	{
		Faction = -1;
		hurtTimer = invulnerabilityDuration;
	}
	
	void FixedUpdate()
	{
		// If we're hurt (falling back) 
		// cannot attack or move.
		if(hurtTimer < hurtDuration)
		{
			HandleHurtMovement();
		}
		
		if(hurtTimer < invulnerabilityDuration)
		{
			hurtTimer += Time.fixedDeltaTime;
		}
	}
	
	
	void HandleHurtMovement()
	{
		this.transform.Translate(
			hurtMovement.x * Time.fixedDeltaTime,
			hurtMovement.y * Time.fixedDeltaTime,
			0);
	}
	
	public void TakeDamage(int damage)
	{
	}
	
	public override void TouchedByBeing (Being other)
	{
		// do nothing
	}
	
	public override void TouchedByWeapon (Weapon other)
	{
		Debug.Log("Ouch!");
		if(other.owner.Faction != this.Faction && !IsInvulnerable)
		{
			// take damage, knockback
			TakeDamage(other.Strength);
			
			// fly backward
			hurtTimer = 0;
			Vector2 direction;
			direction.x = this.transform.position.x - other.transform.position.x;
			direction.y = this.transform.position.y - other.transform.position.y;
			hurtMovement = direction.normalized * hurtSpeed;
		}
	}
	
	public override void WeaponTouchedByWeapon (Weapon other)
	{
		
	}
}