using UnityEngine;
using System.Collections;

public class BeingCollision : BaseCollision 
{
	public Being being;
	
	void OnTriggerStay (Collider other)
	{
		// Debug.Log(string.Format("Being {0} touched other: {1}", this.collider.name, other.name));	
		
		BaseCollision collision = other.GetComponent<BaseCollision>();
		if(collision is BeingCollision)
		{
			BeingCollision beingCollision = (BeingCollision)collision;
			being.TouchedByBeing(beingCollision.being);
		} 
		else if(collision is WeaponCollision)
		{
			WeaponCollision weaponCollision = (WeaponCollision)collision;
			being.TouchedByWeapon(weaponCollision.weapon);
		}
	}
}