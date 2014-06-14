using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {
	const float GridSize = 32f;
	const float OneOverGridSize = 1f / GridSize;

	void OnEnable()
	{
		EditorApplication.update += SnapUpdate;
	}

	void OnDisable()
	{
		EditorApplication.update -= SnapUpdate;
	}
	
	// Update is called once per frame
	void SnapUpdate () {
		if(Selection.instanceIDs.Length > 1)
		{
			return;
		}

		float x = Mathf.Round(this.transform.position.x * OneOverGridSize) * GridSize;
		float y = Mathf.Round(this.transform.position.y * OneOverGridSize) * GridSize;
		float z = this.transform.position.z;

		this.transform.position = new Vector3(x, y, z);
	}
}
