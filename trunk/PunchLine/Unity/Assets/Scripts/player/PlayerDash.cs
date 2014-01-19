using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour {
	
	public enum DashStateType { Preparing, Dashing, Nothing }
	public DashStateType DashState { get; private set; }

	private Player player;
	private Animator playerAnimator;

	private const string downAnimaitonName = "playerdashdown";
	private const string upAnimaitonName = "playerdashup";
	private const string leftAnimaitonName = "playerdashleft";
	private const string rightAnimaitonName = "playerdashright";
	private const string prepareSuffix = "_prep";

	float prepTimer = 0;
	public void IgniteDash()
	{
		// begin prepping
		prepTimer = 0;
		this.DashState = DashStateType.Preparing;
	}

	public void IsHoldingDash()
	{
		// show prep animation
		// it can change direction
		if(DashState == DashStateType.Preparing)
		{
			playerAnimator.Play(GetPrepAnimation());
		}
	}

	public void CancelDash()
	{
		DashState = DashStateType.Nothing;
	}

	void Awake()
	{
		this.playerAnimator = this.GetComponent<Animator>();
		this.player = this.GetComponent<Player>();
		DashState = DashStateType.Nothing;
	}

	void FixedUpdate()
	{
		if(DashState == DashStateType.Preparing)
		{
			playerAnimator.Play(GetPrepAnimation());

			prepTimer += Time.fixedDeltaTime;
			if(prepTimer > player.dashPrepDuration)
			{
				StartDashing();
			}
		}
	}

	private void StartDashing()
	{
		DashState = DashStateType.Dashing;
		playerAnimator.Play(GetDashAnimation());
	}

	private string GetPrepAnimation()
	{
		return GetDashAnimation() + prepareSuffix;
	}

	private string GetDashAnimation()
	{
		if(player.facing == EntityFacing.Up)
		{
			return upAnimaitonName;
		} 
		else if(player.facing == EntityFacing.Down)
		{
			return downAnimaitonName;
		}
		else if(player.facing == EntityFacing.Left)
		{
			return leftAnimaitonName;
		}
		else
		{
			return rightAnimaitonName;
		}
	}
}
