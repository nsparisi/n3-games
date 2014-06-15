using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaitObjective : AbstractObjective 
{
	public float TimeToWait = 5; 

	void Start()
	{
		StartCoroutine(WaitAndComplete());
	}

	IEnumerator WaitAndComplete()
	{
		yield return new WaitForSeconds(TimeToWait);
		OnComplete();
	}
}

