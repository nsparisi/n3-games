using UnityEngine;
using UnityEditor;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public Camera mainCamera;
	public CameraArea CurrentCameraArea;
	
	public Player target;

	bool movingToTarget = false;

	Vector3 targetCameraPosition;
	float cameraMoveSpeed = 48f;

	void Awake()
	{
		if (!target)
		{
			target = GameObject.FindObjectOfType<Player>();
			this.transform.position = target.transform.position;
		}

		if(!mainCamera)
			this.mainCamera = Camera.main;

		if (rigidbody)
		{
			rigidbody.useGravity = false;
		}
	}
	void OnTriggerStay(Collider other)
	{
		if(!CurrentCameraArea)
		{
			CameraArea newArea = other.GetComponent<CameraArea>();
			CurrentCameraArea = newArea;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		bool instant = !CurrentCameraArea;

		CameraArea newArea = other.GetComponent<CameraArea>();
		CurrentCameraArea = newArea;
		if (instant)
		{
			if (target)
			{
				Vector3 position = target.transform.position;
				this.transform.position = position;
				position.z = mainCamera.transform.position.z;
				mainCamera.transform.position = position;
				mainCamera.transform.position = RestrictToArea();
			}
		}
		else
		{
			targetCameraPosition = RestrictToArea();
			movingToTarget = true;
			GlobalPauser.actionPaused = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
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
		if (movingToTarget)
		{
			mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetCameraPosition, cameraMoveSpeed);
			if (mainCamera.transform.position == targetCameraPosition)
			{
				movingToTarget = false;
				GlobalPauser.actionPaused = false;
			}
		}

		if (target && !movingToTarget)
		{
			Vector3 position = target.transform.position;
			this.transform.position = position;

			position.z = mainCamera.transform.position.z;
			mainCamera.transform.position = position;
			mainCamera.transform.position = RestrictToArea();
		}
	}

	Rect CalculateViewableCameraArea()
	{
		Vector2 position = mainCamera.transform.position;
		float ySize = mainCamera.orthographicSize;
		float xSize = mainCamera.aspect*ySize;
		return new Rect(position.x - xSize, position.y - ySize, xSize*2, ySize*2);
	}

	Vector3 RestrictToArea()
	{
		Vector3 cameraPosition = mainCamera.transform.position;
		Rect cameraViewArea = CalculateViewableCameraArea();

		if (!CurrentCameraArea)
			return cameraPosition;
		Rect cameraMoveArea = CurrentCameraArea.GetRect();
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
		else if (cameraViewArea.yMax > cameraMoveArea.yMax)
		{
			cameraPosition.y = cameraMoveArea.yMax - mainCamera.orthographicSize;
		}
		return cameraPosition;
	}
}
