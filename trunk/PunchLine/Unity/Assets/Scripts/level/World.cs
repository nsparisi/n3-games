using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public List<Level> AllLevels = new List<Level>();
	public int CurrentLevelIndex = 0;
	private Vector3 LevelPosition = new Vector3(0, 0, 100);

	public static World CurrentWorld
	{
		get 
		{
			return GameObject.FindObjectOfType<World>();
		}
	}

	// Use this for initialization
	void Start()
	{
		if(AllLevels.Count == 0)
		{
			Debug.LogError("World has no levels.");
		}
		
		AllLevels[CurrentLevelIndex].transform.position = LevelPosition;
		AllLevels[CurrentLevelIndex].LevelStart();
	}
	
	public void WorldComplete()
	{
		Debug.Log("World Defeated!");
	}

	public void LevelComplete()
	{
		CurrentLevelIndex++;

		if(CurrentLevelIndex >= AllLevels.Count)
		{
			WorldComplete();
			return;
		}

		StartCoroutine( 
		    AnimateNextLevel(
				AllLevels[CurrentLevelIndex - 1], 
				AllLevels[CurrentLevelIndex]));
	}

	IEnumerator AnimateNextLevel(Level previousLevel, Level nextLevel)
	{
		Debug.Log("AnimateNextLevel");
		yield return StartCoroutine(previousLevel.AnimateLevelComplete());
		previousLevel.gameObject.SetActive(false);

		yield return new WaitForSeconds(1.0f);

		nextLevel.gameObject.SetActive(true);
		nextLevel.transform.position = LevelPosition;
		nextLevel.LevelStart();
		yield break;
	}
}
