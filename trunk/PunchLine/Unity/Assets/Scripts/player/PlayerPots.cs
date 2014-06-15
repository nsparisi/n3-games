using UnityEngine;
using System.Collections;

public class PlayerPots : MonoBehaviour {
	
	public enum PotsStateType { PickingUp, Holding, Throwing, Nothing }
	public PotsStateType PotsState { get; private set; }

	private Player player;
	private Animator playerAnimator;

	private const string downAnimaitonName = "playerpotsdown";
	private const string upAnimaitonName = "playerpotsup";
	private const string leftAnimaitonName = "playerpotsleft";
	private const string rightAnimaitonName = "playerpotsright";
	private const string pickupSuffix = "_pickup";
	private const string throwSuffix = "_throw";
	private const string walkSuffix = "_walk";

	private const float PickupDuration = 0.4f;
	private const float ThrowDuration = 0.1f;

	public void Pickup()
	{
		StartCoroutine(PickupRoutine());
	}

	public void Stand()
	{
		playerAnimator.Play(GetAnimation(string.Empty));
	}

	public void Walk()
	{
		playerAnimator.Play(GetAnimation(walkSuffix));
	}

	public void Throw()
	{
		StartCoroutine(ThrowRoutine());
	}

	public void Drop()
	{
		this.PotsState = PotsStateType.Nothing;
	}

	void Awake()
	{
		this.playerAnimator = this.GetComponent<Animator>();
		this.player = this.GetComponent<Player>();
		PotsState = PotsStateType.Nothing;
	}
	
	IEnumerator PickupRoutine()
	{
		this.PotsState = PotsStateType.PickingUp;
		playerAnimator.Play(GetAnimation(pickupSuffix));
		
		yield return new WaitForSeconds(PickupDuration);
		
		this.PotsState = PotsStateType.Holding;
	}
	
	IEnumerator ThrowRoutine()
	{
		this.PotsState = PotsStateType.Throwing;
		playerAnimator.Play(GetAnimation(throwSuffix));
		
		yield return new WaitForSeconds(ThrowDuration);
		
		this.PotsState = PotsStateType.Nothing;
	}

	private string GetAnimation(string suffix)
	{
		if(player.facing == EntityFacing.Up)
		{
			return upAnimaitonName + suffix;
		} 
		else if(player.facing == EntityFacing.Down)
		{
			return downAnimaitonName + suffix;
		}
		else if(player.facing == EntityFacing.Left)
		{
			return leftAnimaitonName + suffix;
		}
		else
		{
			return rightAnimaitonName + suffix;
		}
	}
}
