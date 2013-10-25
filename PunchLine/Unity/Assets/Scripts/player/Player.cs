using UnityEngine;
using System.Collections;

public class Player : Entity 
{
	// public fields
	public float horizontalSpeed = 400;
	public float verticalSpeed = 400;
	public PlayerSword sword;
	public float hurtSpeed = 600;
	public float hurtDuration = 1;
	public float invulnerabilityDuration = 2;
	
	public bool IsInvulnerable 
	{
		get 
		{
			return hurtTimer < invulnerabilityDuration;
		}
	}
	
	// private fields
	InputController inputController;
	
	const float diagonalSpeedModifier = 0.707106f;
	const string actionMoveDown = "move_down";
	const string actionMoveUp = "move_up";
	const string actionMoveLeft = "move_left";
	const string actionMoveRight = "move_right";
	const string actionBasicAttack = "basic_attack";
	
	Vector2 inputMovement;
	Vector2 hurtMovement;
	bool attackWasIssued;
	float hurtTimer;
	
	enum PlayerFacingType { Up, Down, Left, Right }
	PlayerFacingType facing;
	
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
	
	void FixedUpdate()
	{
		if(attackWasIssued)
		{
			BasicAttack();
			attackWasIssued = false;
		}
		
		// If we're hurt (falling back) 
		// cannot attack or move.
		if(hurtTimer < hurtDuration)
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
		this.transform.Translate(
			hurtMovement.x * Time.fixedDeltaTime,
			hurtMovement.y * Time.fixedDeltaTime,
			0);
	}
	
	void HandleMovement()
	{
		inputMovement.x = 0;
		inputMovement.y = 0;
		
		//  Move player around
		if(inputController.GetAction(actionMoveRight))
		{
			inputMovement.x = horizontalSpeed;
		}
		
		if(inputController.GetAction(actionMoveLeft))
		{
			inputMovement.x = -horizontalSpeed;
		}
		
		if(inputController.GetAction(actionMoveDown))
		{
			inputMovement.y = -horizontalSpeed;
		}
		
		if(inputController.GetAction(actionMoveUp))
		{
			inputMovement.y = horizontalSpeed;
		}
		
		// Move Diagonal = cut speed
		if(inputMovement.x != 0 && inputMovement.y != 0)
		{
			inputMovement.x *= diagonalSpeedModifier;
			inputMovement.y *= diagonalSpeedModifier;
		}
		
		this.transform.Translate(
			inputMovement.x * Time.fixedDeltaTime,
			inputMovement.y * Time.fixedDeltaTime,
			0);
		
		HandleFacing();
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
	
	public void TakeDamage(int damage)
	{
	}
	
	public override void TouchedByEntity (Entity other)
	{
		if(other.Faction != this.Faction &&
			!this.IsInvulnerable)
		{
			// take damage
			TakeDamage(other.Strength);
			
			// fly backward
			hurtTimer = 0;
			Vector2 direction;
			direction.x = this.transform.position.x - other.transform.position.x;
			direction.y = this.transform.position.y - other.transform.position.y;
			hurtMovement = direction.normalized * hurtSpeed;
		}
	}
	
	public override void TouchedByWeapon (Weapon other)
	{
		
	}
	
	public override void WeaponTouchedByWeapon (Weapon other)
	{
		
	}
}
