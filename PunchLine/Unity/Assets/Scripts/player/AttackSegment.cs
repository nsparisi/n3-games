using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseCollision))]
public class AttackSegment : MonoBehaviour
{
	public int startFrame;
	public int frameCount;
	
	public void Enable()
	{
		this.GetComponent<Collider>().enabled = true;
	}
	
	public void Disable()
	{
		this.GetComponent<Collider>().enabled = false;
	}
}
