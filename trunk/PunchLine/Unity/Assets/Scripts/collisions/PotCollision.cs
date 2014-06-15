using UnityEngine;
using System.Collections;

public class PotCollision : BaseCollision 
{
	public Pot pot {get; set;}

	void Awake()
	{
		this.pot = GetComponent<Pot>();
	}

	void OnTriggerStay (Collider other)
	{
		BaseCollision collision = other.GetComponent<BaseCollision>();
		if(collision is EntityCollision)
		{
			EntityCollision entityCollision = (EntityCollision)collision;
			pot.TouchedByEntity(entityCollision.entity);
		}

		else if(collision is WeaponCollision)
		{
			WeaponCollision weaponCollision = (WeaponCollision)collision;
			pot.TouchedByWeapon(weaponCollision.weapon);
		}
		
		else if (collision is WallCollision)
		{
			pot.TouchedByWall();
		}
	}
}