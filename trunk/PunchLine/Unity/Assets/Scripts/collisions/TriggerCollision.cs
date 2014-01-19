using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class TriggerCollision : BaseCollision {

	public int TimesToTrigger;
	public bool UnlimitedTriggers;
	public float TriggerDelay;
	public float TriggerRepeatTime;

	int timesTriggered;
	bool triggering;
	float triggerDelay;
	float triggerRepeatTime;

	public TriggeredAction[] actions;

	new void Start()
	{
		base.Start();
		triggerRepeatTime = TriggerRepeatTime;
	}

	void Update()
	{
		if(triggering)
		{
			triggerDelay = Mathf.Min(triggerDelay + Time.deltaTime, TriggerDelay);
			if(triggerDelay == TriggerDelay)
			{
				Fire();
				triggerDelay = 0f;
				triggering = false;
			}
		}
		triggerRepeatTime = Mathf.Min(triggerRepeatTime + Time.deltaTime, TriggerRepeatTime);
	}

	void OnTriggerStay(Collider other)
	{
		if(!other.transform.parent)
			return;
		if (other.transform.parent.name != "Player")
			return;


		if(triggerRepeatTime == TriggerRepeatTime && (timesTriggered < TimesToTrigger || UnlimitedTriggers))
		{
			if(triggerDelay > 0f)
			{
				triggering = true;
			}
			else
			{
				Fire();
			}

			timesTriggered += 1;
			triggerRepeatTime = 0f;
		}
	}
	void Fire()
	{
		for(int i = 0; i < actions.Length; i++)
		{
			if(actions[i])
				actions[i].Act();
		}
	}
}
