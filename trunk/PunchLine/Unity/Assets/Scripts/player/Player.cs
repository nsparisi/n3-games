using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float horizontalSpeed = 4;
	public float verticalSpeed = 4;
	public float diagonalSpeedModifier = 0.5f;
	
	InputController inputController;
	
	const string actionMoveDown = "move_down";
	const string actionMoveUp = "move_up";
	const string actionMoveLeft = "move_left";
	const string actionMoveRight = "move_right";
	
	Vector2 inputMovement;
	
	void Start () {
		inputController = new InputController();
		inputController.RegisterAction(actionMoveDown, 
			KeyCode.S, 
			KeyCode.DownArrow);
		
		inputController.RegisterAction(actionMoveUp, 
			KeyCode.W, 
			KeyCode.UpArrow);
		
		inputController.RegisterAction(actionMoveLeft, 
			KeyCode.A, 
			KeyCode.LeftArrow);
		
		inputController.RegisterAction(actionMoveRight, 
			KeyCode.D, 
			KeyCode.RightArrow);
	}
	
	
	void FixedUpdate()
	{
		inputMovement.x = 0;
		inputMovement.y = 0;
		
		//  Move player around
		if(inputController.GetAction(actionMoveLeft))
		{
			inputMovement.x = -horizontalSpeed;
		}
		
		if(inputController.GetAction(actionMoveRight))
		{
			inputMovement.x = horizontalSpeed;
		}
		
		if(inputController.GetAction(actionMoveUp))
		{
			inputMovement.y = horizontalSpeed;
		}
		
		if(inputController.GetAction(actionMoveDown))
		{
			inputMovement.y = -horizontalSpeed;
		}
		
		// Move Diagonal = cut speed
		if(inputMovement.x != 0 && inputMovement.y != 0)
		{
			inputMovement.x *= diagonalSpeedModifier;
			inputMovement.y *= diagonalSpeedModifier;
		}
		
		this.transform.Translate(
			inputMovement.x,
			inputMovement.y,
			0);
	}
	
	void GatherInputs()
	{
		
	}
}
