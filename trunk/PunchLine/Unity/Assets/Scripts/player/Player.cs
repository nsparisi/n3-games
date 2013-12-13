﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : Entity 
{
	public PlayerSword sword;
	
	public bool IsInvulnerable 
	{
		get 
		{
			return hurtTimer < invulnerabilityDuration;
		}
	}
	
	// private fields
	InputController inputController;
	
	const string actionMoveDown = "move_down";
	const string actionMoveUp = "move_up";
	const string actionMoveLeft = "move_left";
	const string actionMoveRight = "move_right";
	const string actionBasicAttack = "basic_attack";
	
	Vector2 inputMovement;
	Vector2 hurtMovement;
	bool attackWasIssued;
	bool attackHeldDown;
	float hurtTimer;
	float pushBackTimer;
	
	enum PlayerFacingType { Up, Down, Left, Right }
	PlayerFacingType facing;
	bool touchingWall;

	Animator animator;
    
    public float holeSpeedCoefficient = 0.5f;
    public int holeDamage = 1;
    HoleSensorController holeSensor;
    bool isFalling = false;
    bool fellInHole = false;
    Vector3 lastSafePosition;
	
	new void Awake()
	{
		base.Awake();

		animator = GetComponent<Animator>();
        holeSensor = this.GetComponentInChildren<HoleSensorController>();
	}
	
	void Start () {
		inputController = new InputController();
		inputController.RegisterAction(actionMoveDown, 
			KeyCode.S, 
			KeyCode.DownArrow);
		
		inputController.RegisterAction(actionMoveUp, 
			KeyCode.W, 
			KeyCode.UpArrow);
		
		inputController.RegisterAction(actionMoveLeft, 
			KeyCode.A, 
			KeyCode.LeftArrow);
		
		inputController.RegisterAction(actionMoveRight, 
			KeyCode.D, 
			KeyCode.RightArrow);
		
		inputController.RegisterAction(actionBasicAttack, 
			KeyCode.Slash,
			KeyCode.Mouse0);
		
		sword.CancelSwing();
		
		Faction = 1;
		hurtTimer = invulnerabilityDuration;
		pushBackTimer = pushBackDuration;
        lastSafePosition = this.transform.position;
	}
	
	// KeyDown/KeyUp events need to occur in Update()
	// otherwise they might be skipped.
	void Update()
	{
		if(inputController.GetActionDown(actionBasicAttack))
		{
			attackWasIssued = true;
		}

		attackHeldDown = inputController.GetAction(actionBasicAttack);
	}
	
	protected override void EntityFixedUpdate()
	{
        // Falling animation, can't control character
        if (fellInHole)
        {
            return;
        }

		if(attackWasIssued && !isFalling)
		{
			BasicAttack();
			attackWasIssued = false;
		}

		if(attackHeldDown)
		{
			sword.HoldSword();
		}
		
		// If we're hurt (falling back) 
		// cannot attack or move.
		if(pushBackTimer < pushBackDuration)
		{
			HandleHurtMovement();
		}
		
		// If we're in attack animation, don't move. 
		// Probably should change this in the future
		else if(sword.SwordState == PlayerSword.SwordStateType.NotSwinging || 
		        sword.SwordState == PlayerSword.SwordStateType.Hold)
		{
			HandleMovement();
		}

        // Make checks for hole and falling movement
        HandleHoleMovement();
		
		if(hurtTimer < invulnerabilityDuration)
		{
			hurtTimer += Time.fixedDeltaTime;
		}
		
		if(pushBackTimer < pushBackDuration)
		{
			pushBackTimer += Time.fixedDeltaTime;
		}

	}
	
	void BasicAttack()
	{
		if(facing == PlayerFacingType.Up)
		{
			sword.SwingUp();
		} 
		else if(facing == PlayerFacingType.Down)
		{
			sword.SwingDown();
		} 
		else if(facing == PlayerFacingType.Left)
		{
			sword.SwingLeft();
		} 
		else if(facing == PlayerFacingType.Right)
		{
			sword.SwingRight();
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
		inputMovement.x = 0;
		inputMovement.y = 0;
		
		//  Move player around
		if(inputController.GetAction(actionMoveRight))
		{
			inputMovement.x = moveSpeed;
		}
		
		if(inputController.GetAction(actionMoveLeft))
		{
			inputMovement.x = -moveSpeed;
		}
		
		if(inputController.GetAction(actionMoveDown))
		{
			inputMovement.y = -moveSpeed;
		}
		
		if(inputController.GetAction(actionMoveUp))
		{
			inputMovement.y = moveSpeed;
		}
		
		// Move Diagonal = cut speed
		if(inputMovement.x != 0 && inputMovement.y != 0)
		{
			inputMovement.x *= diagonalSpeedModifier;
			inputMovement.y *= diagonalSpeedModifier;
		}		
		
		base.MoveWithSliding(inputMovement * Time.fixedDeltaTime);

		if(sword.SwordState == PlayerSword.SwordStateType.NotSwinging)
		{
			HandleFacing();
			HandleAnimationFacing();
		}
	}
	
	void HandleFacing()
	{
		// Edge case
		if( inputMovement.x < 0 && 
			inputMovement.y > 0 && 
			facing == PlayerFacingType.Right)
		{
			facing = PlayerFacingType.Up;
			return;
		}
		
		// Don't change if moving diagonal
		if(inputMovement.x != 0 && inputMovement.y != 0)
		{
			return;
		}
		
		// Basic facings
		if(inputMovement.x > 0)
		{
			facing = PlayerFacingType.Right;
		}
		
		else if(inputMovement.x < 0)
		{
			facing = PlayerFacingType.Left;
		}
		
		else if(inputMovement.y > 0)
		{
			facing = PlayerFacingType.Up;
		}
		
		else if(inputMovement.y < 0)
		{
			facing = PlayerFacingType.Down;
		}
	}

	public void HandleAnimationFacing()
	{
		switch(facing)
		{
		case PlayerFacingType.Down:
			if (inputMovement.y != 0)
			{
				animator.Play("walkdown");
			}
			else
			{
				animator.Play("standdown");
			}
			break;
		case PlayerFacingType.Up:
			if (inputMovement.y != 0)
			{
				animator.Play("walkup");
			}
			else
			{
				animator.Play("standup");
			}
			break;
		case PlayerFacingType.Right:
			if (inputMovement.x != 0)
			{
				animator.Play("walkright");
			}
			else
			{
				animator.Play("standright");
			}
			break;
		case PlayerFacingType.Left:
			if (inputMovement.x != 0)
			{
				animator.Play("walkleft");
			}
			else
			{
				animator.Play("standleft");
			}
			break;
		}

		if (inputMovement.y != 0)
		{

		}
	}

	public void HandleHoleMovement()
	{
        if (!isFalling && CenterTouchingHole())
        {
            // just started to fall in a hole
            isFalling = true;
        }
        else if(!isFalling)
        {
            // Remember position if not falling.
            // Will teleport here if we fall in the future

            // doesn't really work.
            //lastSafePosition = this.transform.position;
        }

        if (isFalling)
        {
            Vector2 holeMovement = Vector2.zero;
            float holeSpeed = moveSpeed * holeSpeedCoefficient;

            if (UpTouchingHole() && !DownTouchingHole())
            {
                holeMovement.y = holeSpeed;
            }
            else if (DownTouchingHole() && !UpTouchingHole())
            {
                holeMovement.y = -holeSpeed;
            }

            if (LeftTouchingHole() && !RightTouchingHole())
            {
                holeMovement.x = -holeSpeed;
            }
            else if (RightTouchingHole() && !LeftTouchingHole())
            {
                holeMovement.x = holeSpeed;
            }

            // Got away from the hole!
            if (!UpTouchingHole() &&
                !DownTouchingHole() &&
                !LeftTouchingHole() &&
                !RightTouchingHole())
            {
                isFalling = false;
            }

            // Fell in the hole! =(
            if (UpTouchingHole() &&
                DownTouchingHole() &&
                LeftTouchingHole() &&
                RightTouchingHole())
            {
                FellInHole();
            }
            
            if(isFalling)
            {
                // Move Diagonal = cut speed
                if (holeMovement.x != 0 && holeMovement.y != 0)
                {
                    holeMovement.x *= diagonalSpeedModifier;
                    holeMovement.y *= diagonalSpeedModifier;
                }

                // Don't want to increase movement speed 
                // past it's normal speed
                if(inputMovement.x > 0 && holeMovement.x > 0)
                {
                    holeMovement.x = 0;
                }
                else if(inputMovement.x < 0 && holeMovement.x < 0)
                {
                    holeMovement.x = 0;
                }

                if(inputMovement.y > 0 && holeMovement.y > 0)
                {
                    holeMovement.y = 0;
                }
                else if(inputMovement.y < 0 && holeMovement.y < 0)
                {
                    holeMovement.y = 0;
                }

                // Move towards the hole (can move diagonal)            
                base.MoveWithSliding(holeMovement * Time.fixedDeltaTime);
            }

        }
    }

    void FellInHole()
    {
        isFalling = false;
        StartCoroutine(FellInHoleRoutine());
    }

    IEnumerator FellInHoleRoutine()
    {
        animator.Play("fall");
        fellInHole = true;
        yield return new WaitForSeconds(1.95f);

        // reset and take damage
        this.transform.position = lastSafePosition;
        TakeDamage(holeDamage);
        hurtTimer = 0;
        facing = PlayerFacingType.Down;

        yield return new WaitForFixedUpdate();
        fellInHole = false;
    }

    bool CenterTouchingHole()
    {
        return holeSensor.CenterTouching;
    }
	
	bool UpTouchingHole()
	{
        return holeSensor.UpTouching;
	}
	
	bool DownTouchingHole()
	{
        return holeSensor.DownTouching;
	}
	
	bool LeftTouchingHole()
	{
        return holeSensor.LeftTouching;
	}
	
	bool RightTouchingHole()
	{
        return holeSensor.RightTouching;
	}
	
	public void TakeDamage(int damage)
	{
		this.sword.UnHoldSword();
	}
	
	public override void TouchedByEntity (Entity other)
	{
		if(other.Faction != this.Faction &&
			!IsInvulnerable)
		{
			// take damage
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
	
	public override void TouchedByWeapon(Weapon other)
	{
		if(other.owner.Faction != this.Faction && 
			!IsInvulnerable)
		{
			// take damage, knockback
			TakeDamage(other.Strength);
			
			// fly backward
			hurtTimer = 0;
			pushBackTimer = 0;
			Vector2 direction;
			direction.x = this.transform.position.x - other.owner.transform.position.x;
			direction.y = this.transform.position.y - other.owner.transform.position.y;
			hurtMovement = direction.normalized * hurtSpeed;
		}
	}
	
	public override void WeaponTouchedByWeapon(Weapon other)
	{
		if(other.owner.Faction != this.Faction && 
			!IsInvulnerable)
		{			
			// fly backward
			pushBackTimer = pushBackDuration * 0.5f;
			Vector2 direction;
			direction.x = this.transform.position.x - other.transform.position.x;
			direction.y = this.transform.position.y - other.transform.position.y;
			hurtMovement = direction.normalized * hurtSpeed;
			this.sword.UnHoldSword();
		}
	}
	
	public override void WeaponTouchedByEntity(Entity other)
	{
		if(other.Faction != this.Faction)
		{
			this.sword.UnHoldSword();
		}
	}
	
	public override void TouchedByWall(Collider other)
	{
		
	}
	
	void OnCollisionEnter(Collision other)
	{
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
	}
}