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
	
	public void DoEnable()
	{
		//this.GetComponent<Collider>().enabled = true;
		this.GetComponent<Collider>().gameObject.SetActive(true);
		//this.transform.localPosition = onScreen;
	}
	
	public void DoDisable()
	{
		//this.GetComponent<Collider>().enabled = false;
		this.GetComponent<Collider>().gameObject.SetActive(false);
		//this.transform.position = offScreen;
	}
}
