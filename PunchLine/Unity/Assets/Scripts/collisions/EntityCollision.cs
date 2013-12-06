using UnityEngine;
using System.Collections;

public class EntityCollision : BaseCollision 
{
	public Entity entity;
	
	void OnTriggerStay (Collider other)
	{
		// Debug.Log(string.Format("Entity {0} touched other: {1}", this.collider.name, other.name));	
		
		BaseCollision collision = other.GetComponent<BaseCollision>();

		// tell this entity that someone else touched it
		if(collision is EntityCollision)
		{
			EntityCollision entityCollision = (EntityCollision)collision;
			entity.TouchedByEntity(entityCollision.entity);
		} 

		// tell this entity that some one else's weapon touched it
		if(collision is WeaponCollision)
		{
			WeaponCollision weaponCollision = (WeaponCollision)collision;
			entity.TouchedByWeapon(weaponCollision.weapon);
		}
	}
}