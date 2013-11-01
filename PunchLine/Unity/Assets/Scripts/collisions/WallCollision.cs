using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class WallCollision : BaseCollision 
{
	void OnTriggerStay (Collider other)
	{
	}
}