using UnityEngine;
using System.Collections;

public class Pot : MonoBehaviour 
{
	public void PickUp(Player player)
	{
		Debug.Log("Picked up by player");
		this.gameObject.SetActive(false);
	}
}
