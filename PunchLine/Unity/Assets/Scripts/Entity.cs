using UnityEngine;
using System.Collections;

/// <summary>
/// Player or enemy.
/// </summary>
public abstract class Entity : MonoBehaviour 
{
	public int Faction { get; protected set; }
	public int Strength { get; protected set; }
	public int Health  { get; protected set; }
	
	public abstract void TouchedByEntity(Entity other);
	public abstract void TouchedByWeapon(Weapon other);
	public abstract void WeaponTouchedByWeapon(Weapon other);
	public abstract void TouchedByWall(Collider other);
}