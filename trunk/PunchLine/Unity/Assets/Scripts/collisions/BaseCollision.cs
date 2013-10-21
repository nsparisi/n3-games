using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BaseCollision : MonoBehaviour 
{
	void Awake()
	{
		this.collider.isTrigger = true;
	}
}