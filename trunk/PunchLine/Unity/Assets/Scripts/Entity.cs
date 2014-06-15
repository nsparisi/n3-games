using UnityEngine;
using System.Collections;

/// <summary>
/// Player or enemy.
/// </summary>
public abstract class Entity : MonoBehaviour 
{
	public float moveSpeed = 200;
	public float hurtSpeed = 600;
	public float pushBackDuration = 0.5f;
	public float invulnerabilityDuration = 2;
	public int Health = 2;
	
	public int Faction { get; protected set; }
	public int Strength { get; protected set; }
	
	public abstract void TouchedByEntity(Entity other);
	public abstract void TouchedByWeapon(Weapon other);
	public abstract void WeaponTouchedByWeapon(Weapon other);
	public abstract void WeaponTouchedByEntity(Entity other);
	public abstract void TouchedByWall(Collider other);
	
	protected CharacterController characterController;
	protected Vector3 previousPosition;
	
	protected abstract void EntityFixedUpdate();
	
	protected const float diagonalSpeedModifier = 0.707106f;
	
	public void Awake()
	{		
		characterController = this.GetComponent<CharacterController>();
	}
	
	void FixedUpdate()
	{
		if (GlobalPauser.actionPaused)
			return;

		EntityFixedUpdate();
		
		previousPosition = this.transform.position;
	}
	
	protected void MoveWithSliding(Vector3 movement)
	{
		characterController.Move(
			new Vector3(
				movement.x,
				movement.y,
				0));
		
		// if we're only moving vertical, 
		// see if we can slide on a slope
		if(movement.y != 0 && movement.x == 0)
		{
			if(this.transform.position == previousPosition)
			{
				// try going right
				characterController.Move(
					new Vector3(
						diagonalSpeedModifier * moveSpeed * Time.fixedDeltaTime,
						0,
						0));
				
				// we didn't slide, so this is correct
				if(UsefulStuff.CloseEnough(this.transform.position.y, previousPosition.y, 0.01f))
				{
					characterController.Move(
						new Vector3(
							0,
							movement.y * Time.fixedDeltaTime,
							0));
					
					// if we didn't move, we must be hitting a wall
					if(UsefulStuff.CloseEnough(this.transform.position.y, previousPosition.y, 0.01f))
					{
						this.transform.position = previousPosition;
					}
				}
				else
				{
					this.transform.position = previousPosition;
					
					// try going left
					characterController.Move(
						new Vector3(
							-diagonalSpeedModifier * moveSpeed * Time.fixedDeltaTime,
							0,
							0));
					
					// we didn't slide, so this is correct
					if(UsefulStuff.CloseEnough(this.transform.position.y, previousPosition.y, 0.01f))
					{
						characterController.Move(
							new Vector3(
								0,
								movement.y * Time.fixedDeltaTime,
								0));
						
						// if we didn't move, we must be hitting a wall
						if(UsefulStuff.CloseEnough(this.transform.position.y, previousPosition.y, 0.01f))
						{
							this.transform.position = previousPosition;
						}
					}
					else 
					{
						this.transform.position = previousPosition;
					}
				}
			}
		}
	}
}