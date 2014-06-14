using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public int EnemiesLeft = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	public void LevelStart()
	{
		StartCoroutine(WaitAndComplete());
	}

	public void LevelComplete()
	{
		World.CurrentWorld.LevelComplete();
	}

	IEnumerator WaitAndComplete()
	{
		yield return new WaitForSeconds(5.0f);
		LevelComplete();
	}
	
	// ************************
	// Animate the level exploding into pieces
	// ************************
	public IEnumerator AnimateLevelComplete()
	{
		Debug.Log("AnimateLevelComplete");
		yield return StartCoroutine(AnimateTiles());
	}

	IEnumerator AnimateTiles()
	{
		Transform background = transform.FindChild("Background");
		Debug.Log("background: " + background);
		for(int i = 0; i < background.childCount; i++)
		{
			Transform tile = background.GetChild(i);
			Debug.Log("tile: " + tile);
			StartCoroutine(FlyTileAway(tile));
			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(3);
	}

	IEnumerator FlyTileAway(Transform tile)
	{
		Vector3 velocity = new Vector3(5,5,0);
		float startTime = Time.fixedTime;
		float duration = 5;

		while(Time.fixedTime - startTime < duration)
		{
			tile.Translate(velocity);
			yield return new WaitForFixedUpdate();
		}

		yield break;
	}
}
