using UnityEngine;
using System.Collections;

public class AttackSegment : MonoBehaviour
{
	public int startFrame;
	public int frameCount;
	
	void Awake()
	{
		this.GetComponent<Collider>().enabled = true;
	}
	
	public void Enable()
	{
		//this.GetComponent<Collider>().enabled = true;
		this.GetComponent<Collider>().gameObject.SetActive(true);
	}
	
	public void Disable()
	{
		//this.GetComponent<Collider>().enabled = false;
		this.GetComponent<Collider>().gameObject.SetActive(false);
	}
}
