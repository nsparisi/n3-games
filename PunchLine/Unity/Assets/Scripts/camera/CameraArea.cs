using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]

public class CameraArea : MonoBehaviour {
	BoxCollider boxCollider;
	void Awake()
	{
		boxCollider = collider as BoxCollider;
	}

	void OnTriggerEnter(Collider other)
	{
	}

	public Rect GetRect()
	{
		float left = boxCollider.center.x - boxCollider.size.x/2 + gameObject.transform.position.x;
		float top = boxCollider.center.y - boxCollider.size.y/2 + gameObject.transform.position.y;
		return new Rect(left, top, boxCollider.size.x, boxCollider.size.y);
	}
}
