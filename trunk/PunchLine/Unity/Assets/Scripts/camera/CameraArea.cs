using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]

public class CameraArea : MonoBehaviour {
	int x;
	void OnTriggerEnter(Collider other)
	{
		print(x++);
	}
}
