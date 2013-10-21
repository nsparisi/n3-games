using UnityEngine;
using System.Collections;

public class WeaponCollision : BaseCollision 
{
	public Weapon weapon;
	
	void OnTriggerStay (Collider other)
	{
		//Debug.Log(string.Format("Weapon {0} touched other: {1}", this.collider.name, other.name));	
	}
}