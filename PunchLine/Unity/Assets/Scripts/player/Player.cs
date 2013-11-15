using UnityEngine;
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
	float hurtTimer;
	float pushBackTimer;
	
	enum PlayerFacingType { Up, Down, Left, Right }
	PlayerFacingType facing;
	bool touchingWall;

	Animator animator;
	
	new void Awake()
	{
		base.Awake();

		animator = GetComponent<Animator>();
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
	}
	
	// KeyDown/KeyUp events need to occur in Update()
	// otherwise they might be skipped.
	void Update()
	{
		if(inputController.GetActionDown(actionBasicAttack))
		{
			attackWasIssued = true;
		}
	}
	
	protected override void EntityFixedUpdate()
	{
		if(attackWasIssued)
		{
			BasicAttack();
			attackWasIssued = false;
		}
		
		// If we're hurt (falling back) 
		// cannot attack or move.
		if(pushBackTimer < pushBackDuration)
		{
			HandleHurtMovement();
		}
		
		// If we're in attack animation, don't move. 
		// Probably should change this in the future
		else if(sword.SwordState == PlayerSword.SwordStateType.NotSwinging)
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
		
		HandleFacing();
		HandleAnimationFacing();
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
	
	public void TakeDamage(int damage)
	{
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
			direction.x = this.transform.position.x - other.transform.position.x;
			direction.y = this.transform.position.y - other.transform.position.y;
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
