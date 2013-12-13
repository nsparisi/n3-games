using UnityEngine;
using UnityEditor;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public Camera mainCamera;
	public CameraArea CurrentCameraArea;
	
	public Player target;

	void Awake()
	{
		if(!mainCamera)
			this.mainCamera = Camera.main;
	}

	void OnTriggerStay(Collider other)
	{
		CameraArea newArea = other.GetComponent<CameraArea>();
		if (newArea)
		{
			CurrentCameraArea = newArea;
		}
	}

	void OnTriggerExit(Collider other)
	{
		CurrentCameraArea = null;
	}

//	void OnTriggerEnter(Collider other)
//	{
//		if(!mainCamera)
//			this.mainCamera = Camera.main;
//		print("entered");
//		CameraArea newArea = other.GetComponent<CameraArea>();
//		if (newArea)
//		{
//			CurrentCameraArea = newArea;
//		}
//	}
	
	void LateUpdate()
	{
		if (target)
		{
			Vector3 position = mainCamera.transform.position;
			position.x = target.transform.position.x;
			position.y = target.transform.position.y;
			mainCamera.transform.position = position;

			this.transform.position = target.transform.position;

			RestrictToArea();
		}
	}

	Rect CalculateViewableCameraArea()
	{
		Vector2 position = mainCamera.transform.position;
		float ySize = mainCamera.orthographicSize;
		float xSize = mainCamera.aspect*ySize;
		return new Rect(position.x - xSize, position.y - ySize, xSize*2, ySize*2);
	}

	void RestrictToArea()
	{
		Vector3 cameraPosition = mainCamera.transform.position;
		Rect cameraViewArea = CalculateViewableCameraArea();
		print(cameraViewArea.xMin + ", " + cameraViewArea.xMax);

		if (!CurrentCameraArea)
			return;
		Rect cameraMoveArea = CurrentCameraArea.GetRect();
		print(cameraMoveArea.xMin + ", " + cameraMoveArea.xMax);
		//x
		if (cameraViewArea.width >= cameraMoveArea.width)
		{
			cameraPosition.x = cameraMoveArea.center.x;
		}
		else if (cameraViewArea.xMin <= cameraMoveArea.xMin)
		{
			cameraPosition.x = cameraMoveArea.xMin + mainCamera.orthographicSize*mainCamera.aspect;
		}
		else if (cameraViewArea.xMax > cameraMoveArea.xMax)
		{
			cameraPosition.x = cameraMoveArea.xMax - mainCamera.orthographicSize*mainCamera.aspect;
		}

		//y
		if (cameraViewArea.height >= cameraMoveArea.height)
		{
			cameraPosition.y = cameraMoveArea.center.y;
		}
		else
			if (cameraViewArea.yMin <= cameraMoveArea.yMin)
		{
			cameraPosition.y = cameraMoveArea.yMin + mainCamera.orthographicSize;
		}
		else if (cameraViewArea.yMax >= cameraMoveArea.yMax)
		{
			cameraPosition.y = cameraMoveArea.yMax - mainCamera.orthographicSize;
		}

		mainCamera.transform.position = cameraPosition;
	}
}
