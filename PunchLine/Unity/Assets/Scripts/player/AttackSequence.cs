using UnityEngine;
using System.Collections;

public class AttackSequence : MonoBehaviour
{
	public Transform attackSegmentsParent;
	public int lastFrame;
	
	public bool IsFinished
	{
		get
		{
			return currentFrame > lastFrame;
		}
	}
	
	private int currentFrame;
	
	void Start()
	{
		StopSequence();
	}
	
	public void StartSequence()
	{
		currentFrame = 0;
		Refresh();
	}
	
	public void StopSequence()
	{
		currentFrame = -1;
		Refresh();
	}
	
	public void AdvanceOneFrame()
	{
		currentFrame++;
		Refresh();
	}
	
	void Refresh()
	{
		for(int i = 0; i < attackSegmentsParent.childCount; i++)
		{
			AttackSegment segment = attackSegmentsParent.GetChild(i).GetComponent<AttackSegment>();
			if( segment != null &&
				currentFrame >= segment.startFrame &&
				currentFrame < segment.startFrame + segment.frameCount)
			{
				segment.DoEnable();
			}
			else 
			{
				segment.DoDisable();
			}
		}
	}
}
