﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {
	const float GridSize = 32f;
	const float OneOverGridSize = 1f / GridSize;

	float waittime = 0;
	void OnEnable()
	{
		//EditorApplication.update += SnapUpdate;
	}

	void OnDisable()
	{
		//EditorApplication.update -= SnapUpdate;
	}
	
	// Update is called once per frame
	void SnapUpdate () {
		//if(Selection.instanceIDs.Length > 1)
		//{
		//	return;
		//}

		float x = Mathf.Round(this.transform.position.x * OneOverGridSize) * GridSize;
		float y = Mathf.Round(this.transform.position.y * OneOverGridSize) * GridSize;
		float z = this.transform.position.z;

		this.transform.position = new Vector3(x, y, z);
	}

	void OnRenderObject()
	{
		float newtime = Time.realtimeSinceStartup - waittime;
		if(newtime > 0.5f)
		{
			SnapUpdate();
			waittime = Time.realtimeSinceStartup;
		}
	}
}
