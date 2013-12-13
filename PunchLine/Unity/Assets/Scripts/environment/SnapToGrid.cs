using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {
	const float GridSize = 64f;
	const float OneOverGridSize = 1f / GridSize;
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Round(this.transform.position.x * OneOverGridSize) * GridSize;
		float y = Mathf.Round(this.transform.position.y * OneOverGridSize) * GridSize;
		float z = this.transform.position.z;

		this.transform.position = new Vector3(x, y, z);
	}
}
