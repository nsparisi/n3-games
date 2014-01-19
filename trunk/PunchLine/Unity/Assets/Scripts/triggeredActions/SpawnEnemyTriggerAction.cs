using UnityEngine;
using System.Collections;

public class SpawnEnemyTriggerAction : TriggeredAction {
	public GameObject enemyToSpawn;

	public override void Act()
	{
		Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
	}
}
