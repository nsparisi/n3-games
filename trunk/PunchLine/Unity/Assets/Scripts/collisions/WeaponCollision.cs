using UnityEngine;
using System.Collections;

public class WeaponCollision : BaseCollision 
{
	public Weapon weapon;
	
	void OnTriggerStay (Collider other)
	{
		// tell the this weapon's owner that another weapon touched it.
		BaseCollision collision = other.GetComponent<BaseCollision>();
		if(collision is WeaponCollision)
		{
			WeaponCollision weaponCollision = (WeaponCollision)collision;
			weapon.owner.WeaponTouchedByWeapon(weaponCollision.weapon);
		}
		
		// tell the this weapon's owner that someone touched it.
		else if(collision is EntityCollision)
		{
			EntityCollision entityCollision = (EntityCollision)collision;
			weapon.owner.WeaponTouchedByEntity(entityCollision.entity);
		}
		
		else if(collision is PotCollision)
		{
			PotCollision potCollision = (PotCollision)collision;
			weapon.owner.WeaponTouchedByPot(potCollision.pot);
		}
	}
}