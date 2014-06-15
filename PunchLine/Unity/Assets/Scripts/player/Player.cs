using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : Entity 
{
	public static Player Instance { get; private set; }

	public PlayerSword sword;
	
	public bool IsInvulnerable 
	{
		get 
		{
			return hurtTimer < invulnerabilityDuration;
		}
	}

	public bool IsBeingKnockedBack
	{
		get
		{
			return pushBackTimer < pushBackDuration;
		}
	}
	
	// private fields
	InputController inputController;
	
	const string actionMoveDown = "move_down";
	const string actionMoveUp = "move_up";
	const string actionMoveLeft = "move_left";
	const string actionMoveRight = "move_right";
	const string actionBasicAttack = "basic_attack";
	const string actionDash = "dash";

	public EntityFacing facing;
	public float holeSpeedCoefficient = 0.5f;
	public int holeDamage = 1;
	public float dashPrepDuration = 1;
	public float dashSpeedCoefficient = 1.6f;

	Vector2 inputMovement;
	Vector2 hurtMovement;
	bool attackWasIssued;
	bool attackHeldDown;
	float hurtTimer;
	float pushBackTimer;
	bool dashWasIssued;
	bool dashHeldDown;
	bool touchingWall;
	Animator animator;
    HoleSensorController holeSensor;
    bool isFalling = false;
    bool fellInHole = false;
    Vector3 lastSafePosition;
	PlayerDash dash;
	bool isDying = false;
	
	new void Awake()
	{
		base.Awake();

		Instance = this;
		animator = GetComponent<Animator>();
		dash = GetComponent<PlayerDash>();
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

		inputController.RegisterAction(actionDash,
		    KeyCode.Quote, 
		    KeyCode.Mouse1, 
		    KeyCode.LeftControl);
		
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
		
		if(inputController.GetActionDown(actionDash))
		{
			dashWasIssued = true;
		}

		attackHeldDown = inputController.GetAction(actionBasicAttack);
		dashHeldDown = inputController.GetAction(actionDash);
	}
	
	protected override void EntityFixedUpdate()
	{
		// You are dead.
		if(isDying)
		{
			return;
		}

        // Falling animation, can't control character
        if (fellInHole)
		{
			dashWasIssued = false;
			attackWasIssued = false;
            return;
        }

		if(dashWasIssued && !isFalling && !IsBeingKnockedBack &&
		   sword.SwordState == PlayerSword.SwordStateType.NotSwinging)
		{
			//begin dashing			
			dash.IgniteDash();
		}

		if(attackWasIssued && !isFalling && !IsBeingKnockedBack && 
		   dash.DashState == PlayerDash.DashStateType.Nothing)
		{
			BasicAttack();
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
		
		dashWasIssued = false;
		attackWasIssued = false;
	}
	
	void BasicAttack()
	{
		AudioManager.Instance.PlaySound(AudioManager.SoundTypes.SwordSwipe1);
		if(facing == EntityFacing.Up)
		{
			sword.SwingUp();
		} 
		else if(facing == EntityFacing.Down)
		{
			sword.SwingDown();
		} 
		else if(facing == EntityFacing.Left)
		{
			sword.SwingLeft();
		} 
		else if(facing == EntityFacing.Right)
		{
			sword.SwingRight();
		} 
	}
	
	void HandleHurtMovement()
	{
		dash.CancelDash();
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

		if(dash.DashState == PlayerDash.DashStateType.Preparing)
		{
			if(dashHeldDown)
			{
				HandleFacing();
			} 
			else 
			{
				dash.CancelDash();
			}
		}
		else 
		{
			Vector2 facingAsAVector = GetFacingAsAVector();
			if(dash.DashState == PlayerDash.DashStateType.Dashing &&
			   (facingAsAVector.normalized == inputMovement.normalized ||
			   inputMovement == Vector2.zero))
			{
				float speed =  dashSpeedCoefficient * moveSpeed * Time.fixedDeltaTime;
				base.MoveWithSliding(facingAsAVector * speed);
			}
			else 
			{
				dash.CancelDash();

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
		}
	}

	Vector2 GetFacingAsAVector()
	{
		if(facing == EntityFacing.Down)
		{
			return new Vector3(0, -1, 0);
		} 
		else if(facing == EntityFacing.Up)
		{
			return new Vector3(0, 1, 0);
		} 
		else if(facing == EntityFacing.Left)
		{
			return new Vector3(-1, 0, 0);
		} 
		else 
		{
			return new Vector3(1, 0, 0);
		} 
	}
	
	void HandleFacing()
	{
		// Edge case
		if( inputMovement.x < 0 && 
			inputMovement.y > 0 && 
		   facing == EntityFacing.Right)
		{
			facing = EntityFacing.Up;
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
			facing = EntityFacing.Right;
		}
		
		else if(inputMovement.x < 0)
		{
			facing = EntityFacing.Left;
		}
		
		else if(inputMovement.y > 0)
		{
			facing = EntityFacing.Up;
		}
		
		else if(inputMovement.y < 0)
		{
			facing = EntityFacing.Down;
		}
	}

	public void HandleAnimationFacing()
	{
		switch(facing)
		{
		case EntityFacing.Down:
			if (inputMovement.y != 0)
			{
				animator.Play("walkdown");
			}
			else
			{
				animator.Play("standdown");
			}
			break;
		case EntityFacing.Up:
			if (inputMovement.y != 0)
			{
				animator.Play("walkup");
			}
			else
			{
				animator.Play("standup");
			}
			break;
		case EntityFacing.Right:
			if (inputMovement.x != 0)
			{
				animator.Play("walkright");
			}
			else
			{
				animator.Play("standright");
			}
			break;
		case EntityFacing.Left:
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
			dash.CancelDash();
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
		facing = EntityFacing.Down;

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

		Health -= damage;
		if(Health <= 0)
		{
			Die();
		} 
		else 
		{
			AudioManager.Instance.PlaySound(AudioManager.SoundTypes.PlayerHurt);
		}
	}
	
	void Die()
	{
		StartCoroutine(DieRoutine());		
		AudioManager.Instance.PlaySound(AudioManager.SoundTypes.PlayerDying);
	}
	
	IEnumerator DieRoutine()
	{
		animator.Play("playerdie");
		isDying = true;
		yield return new WaitForSeconds(1.95f);
		
		// reset and take damage
		Application.LoadLevel("World_1");
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

			if(other is StraightFlyEnemy)
			{
				direction = ((StraightFlyEnemy)other).velocity;
			}
			else 
			{
				direction.x = this.transform.position.x - other.transform.position.x;
				direction.y = this.transform.position.y - other.transform.position.y;
			}
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
