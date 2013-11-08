using UnityEngine;
using System.Collections;

public class AttackSegment : MonoBehaviour
{
	public int startFrame;
	public int frameCount;
	
	private Vector3 onScreen;
	private static Vector3 offScreen = new Vector3(-100, -100, 0);
	
	void Awake()
	{
		this.GetComponent<Collider>().enabled = true;
		onScreen = this.transform.localPosition;
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
