using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BaseCollision : MonoBehaviour 
{
	void Awake()
	{
		this.collider.isTrigger = true;
	}
	
	void OnTriggerEnter (Collider other)
	{
		Debug.Log(string.Format("{0} touched other: {1}", this.collider.name, other.name));	
	}
}