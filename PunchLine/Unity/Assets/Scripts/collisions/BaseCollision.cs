using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BaseCollision : MonoBehaviour 
{
	void Start()
	{
		this.collider.isTrigger = true;
		
		this.transform.position = new Vector3
			(this.transform.position.x,
				this.transform.position.y,
				EntityPositioner.CollisionZPosition);
				
	}
}