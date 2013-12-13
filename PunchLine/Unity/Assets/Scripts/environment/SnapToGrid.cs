using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {
	const int GridSize = 64;
	const float OneOverGridSize = 1f / GridSize;
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.RoundToInt(this.transform.position.x * OneOverGridSize) * GridSize;
		float y = Mathf.RoundToInt(this.transform.position.y * OneOverGridSize) * GridSize;
		float z = this.transform.position.z;

		this.transform.position = new Vector3(x, y, z);
	}
}
