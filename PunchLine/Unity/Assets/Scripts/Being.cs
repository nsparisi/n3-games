using UnityEngine;
using System.Collections;

/// <summary>
/// Player or enemy.
/// </summary>
public abstract class Being : MonoBehaviour 
{
	public int Faction { get; protected set; }
	public int Strength { get; protected set; }
	public int Health  { get; protected set; }
	
	public abstract void TouchedByBeing(Being other);
	public abstract void TouchedByWeapon(Weapon other);
	public abstract void WeaponTouchedByWeapon(Weapon other);
}