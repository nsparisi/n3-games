using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class WallCollision : BaseCollision 
{
	void OnTriggerStay (Collider other)
	{
		// Debug.Log(string.Format("Entity {0} touched other: {1}", this.collider.name, other.name));	
		
		BaseCollision collision = other.GetComponent<BaseCollision>();
		if(collision is EntityCollision)
		{
			EntityCollision entityCollision = (EntityCollision)collision;
			entityCollision.entity.TouchedByWall(this.collider);
		} 
	}
}