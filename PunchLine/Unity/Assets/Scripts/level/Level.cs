using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	public List<AbstractObjective> Objectives;
	bool[] completedObjectives;
	bool shouldBeAwake = false;

	void Awake()
	{
		if(!shouldBeAwake)
		{
			this.gameObject.SetActive(false);
		}
	}

	void Start()
	{
		completedObjectives = new bool[Objectives.Count];
	}
	
	public void LevelStart()
	{
		// initialization logic here
		shouldBeAwake = true;
	}

	// ************************
	// Objectives stuff
	// ************************
	void OnEnable()
	{
		AbstractObjective.OnCompleteEvent += ObjectiveCompleteEvent;
	}

	void OnDisable()
	{
		AbstractObjective.OnCompleteEvent -= ObjectiveCompleteEvent;
	}

	void ObjectiveCompleteEvent(AbstractObjective target)
	{
		for(int i = 0; i < Objectives.Count; i++)
		{
			if(Objectives[i].Equals(target))
			{
				Debug.Log("Level marked objective " + i);
				completedObjectives[i] = true;
				break;
			}
		}

		bool allComplete = true;
		for(int i = 0; i < completedObjectives.Length; i++)
		{
			allComplete &= completedObjectives[i];
		}
		
		if(allComplete)
		{
			LevelComplete();
		}	
	}

	public void LevelComplete()
	{
		Debug.Log("Level Complete!");
		World.CurrentWorld.LevelComplete();
		shouldBeAwake = false;
	}
	
	// ************************
	// Animate the level exploding into pieces
	// ************************
	public IEnumerator AnimateLevelComplete()
	{
		yield return StartCoroutine(AnimateTiles());
	}

	IEnumerator AnimateTiles()
	{
		Transform background = transform.FindChild("Background");
		for(int i = 0; i < background.childCount; i++)
		{
			Transform tile = background.GetChild(i);
			StartCoroutine(FlyTileAway(tile));
			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(3);
	}

	IEnumerator FlyTileAway(Transform tile)
	{
		float speed = 10.0f;
		float x = Random.Range(-1.0f, 1.0f);
		float y = Random.Range(-1.0f, 1.0f);
		Vector3 velocity = new Vector3(x,y,0);
		velocity = velocity.normalized * speed;
		float startTime = Time.fixedTime;
		float duration = 2;

		while(Time.fixedTime - startTime < duration)
		{
			tile.Translate(velocity);
			yield return new WaitForFixedUpdate();
		}

		yield break;
	}
}
