using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]

public class CameraControl : MonoBehaviour {
	public CameraArea CurrentCameraArea;
	
	public Player target;
	
	void OnTriggerEnter(Collider other)
	{
		CameraArea newArea = other.GetComponent<CameraArea>();
		
		if (newArea)
		{
			CurrentCameraArea = newArea;
		}
	}
	
	void LateUpdate()
	{
		Vector3 position = transform.position;
		position.x = target.transform.position.x;
		position.y = target.transform.position.y;
		transform.position = position;
	}
}
