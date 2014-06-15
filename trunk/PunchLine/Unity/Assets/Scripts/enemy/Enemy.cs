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
	private float pushBackTimer;
    private HoleCollider holeCollider;
    private HoleSensor holeSensor;
	private bool fellInHole;
	
	protected delegate void EnemyAIUpdateFunction();
	
	new void Awake()
	{
		base.Awake();
	}
	
	void Start()
	{
		Faction = -1;
		hurtTimer = invulnerabilityDuration;
		pushBackTimer = pushBackDuration;
        holeCollider = this.GetComponent<HoleCollider>();
		holeSensor = this.GetComponentInChildren<HoleSensor>();
		Init();
	}

	protected virtual void RunAI()
	{
		return;
	}

	protected virtual void Init()
	{
		return;
	}
	
	protected override void EntityFixedUpdate()
	{
        // if the enemy is touching a hole
        // die immediately
        if (fellInHole)
        {
            return;
        }
        else if (IsTouchingHole())
        {
            FellInHole();
            return;
        }

		// If we're hurt (falling back) 
		// cannot attack or move.
		if(pushBackTimer < pushBackDuration)
		{
            holeCollider.DeactivateHoleCollisions();
			HandleHurtMovement();
		}
		else 
        {
            holeCollider.ActivateHoleCollisions();
			RunAI();
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

    bool IsTouchingHole()
    {
        if (this.holeSensor != null)
        {
            return this.holeSensor.IsTouching;
        }

        return false;
    }
    
    void FellInHole()
    {
        StartCoroutine(FellInHoleRoutine());
    }
    
    IEnumerator FellInHoleRoutine()
    {
        // animator.Play("fall");
        fellInHole = true;
        yield return new WaitForSeconds(1.95f);

        Kill();
    }

    public void Kill()
    {
        Debug.Log(this.name + " is Dead");
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
			pushBackTimer = pushBackDuration * 0.5f;
			Vector2 direction;
			direction.x = this.transform.position.x - other.owner.transform.position.x;
			direction.y = this.transform.position.y - other.owner.transform.position.y;
			hurtMovement = direction.normalized * hurtSpeed;
		}
	}
	
	public override void WeaponTouchedByEntity(Entity other)
	{
		if(other.Faction != this.Faction)
		{
			// ahha, i got em!
		}
	}
	
	public override void TouchedByWall(Collider other)
	{
		
	}
}