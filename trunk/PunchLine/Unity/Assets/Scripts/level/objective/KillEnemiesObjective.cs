using UnityEngine;
using System.Collections.Generic;

public class KillEnemiesObjective : AbstractObjective 
{
	public List<Enemy> Enemies;
	bool[] deadEnemies;

	void Awake()
	{
		deadEnemies = new bool[Enemies.Count];
	}

	void OnEnable()
	{
		Enemy.OnKilled += EnemyKilledEvent;
	}

	void OnDisable()
	{
		Enemy.OnKilled -= EnemyKilledEvent;
	}

	void EnemyKilledEvent(Enemy target)
	{
		// find the dead enemy and mark it
		for(int i = 0; i < Enemies.Count; i++)
		{
			if(Enemies[i].Equals(target))
			{
				Debug.Log("Objective marked enemy as dead " + i);
				deadEnemies[i] = true;
				break;
			}
		}

		// are all enemies dead?
		bool allDead = true;
		for(int i = 0; i < deadEnemies.Length; i++)
		{
			allDead &= deadEnemies[i];
		}

		if(allDead)
		{
			OnComplete();
		}
	}
}

