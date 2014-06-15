using UnityEngine;
using System.Collections;

public class Pot : MonoBehaviour 
{
	public void PickUp(Player player)
	{
		Debug.Log("Picked up by player");

		foreach(Collider collider in this.GetComponentsInChildren<Collider>())
		{
			collider.enabled = false;
		}

		transform.parent = player.potHolder;
		transform.localPosition = Vector3.zero;
	}

	public void Throw()
	{
		transform.parent = null;
		Destroy(this.gameObject);
	}
}
