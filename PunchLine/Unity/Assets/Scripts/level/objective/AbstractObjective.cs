using UnityEngine;

public abstract class AbstractObjective : MonoBehaviour 
{	
	public delegate void ObjectiveEvent(AbstractObjective target);
	public static event ObjectiveEvent OnCompleteEvent;

	protected void OnComplete()
	{
		if(OnCompleteEvent != null)
		{
			OnCompleteEvent(this);
		}
	}
}

