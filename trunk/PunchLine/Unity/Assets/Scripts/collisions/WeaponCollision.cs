using UnityEngine;
using System.Collections;

public class WeaponCollision : BaseCollision 
{
	public Weapon weapon;
	
	void OnTriggerStay (Collider other)
	{
		// tell the other weapon's owner that this weapon touched it.
		BaseCollision collision = other.GetComponent<BaseCollision>();
		if(collision is WeaponCollision)
		{
			WeaponCollision weaponCollision = (WeaponCollision)collision;
			weapon.owner.WeaponTouchedByWeapon(weaponCollision.weapon);
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		// tell the other weapon's owner that this weapon touched it.
		BaseCollision collision = other.GetComponent<BaseCollision>();
		if(collision is WeaponCollision)
		{
			WeaponCollision weaponCollision = (WeaponCollision)collision;
			weapon.owner.WeaponTouchedByWeapon(weaponCollision.weapon);
		}
	}
}