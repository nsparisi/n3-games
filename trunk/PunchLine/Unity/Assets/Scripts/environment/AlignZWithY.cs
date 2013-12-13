using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AlignZWithY : MonoBehaviour {

	const float zStep = 0.01f;
	const float zOrigin = 0f;
	
	// Update is called once per frame
	void Update () {
		float x = this.transform.position.x;
		float y = this.transform.position.y;
		float z = y * zStep + zOrigin;

		this.transform.position = new Vector3(x, y, z);
	}
}
