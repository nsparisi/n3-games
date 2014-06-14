using UnityEngine;
using System.Collections;

public class PlayerSword : MonoBehaviour {
	
	public enum SwordStateType { Swinging, NotSwinging, Hold }
	public SwordStateType SwordState { get; private set; }

	public Animator playerAnimator;
	public AnimationClip upMotion;
	public AnimationClip downMotion;
	public AnimationClip leftMotion;
	public AnimationClip rightMotion;

	private string downAnimaitonName = "attackdown";
	private string upAnimaitonName = "attackup";
	private string leftAnimaitonName = "attackleft";
	private string rightAnimaitonName = "attackright";
	private string currectAttack;
	private const string copySuffix = "_2";
	
	private string holdAnimationPrefix = "playerhold";

	float attackLength = 0;
	float attackTimer = 0;

	bool holdInput = false;
	bool unholdOverride = false;

	public void CancelSwing()
	{
		this.SwordState = SwordStateType.NotSwinging;
	}
	
	public void SwingUp()
	{
		this.currectAttack = GetAttackName(upAnimaitonName);
		this.attackLength = upMotion.length;
		Swing();
	}
	
	public void SwingDown()
	{
		this.currectAttack = GetAttackName(downAnimaitonName);
		this.attackLength = downMotion.length;
		Swing();
	}
	
	public void SwingLeft()
	{
		this.currectAttack = GetAttackName(leftAnimaitonName);
		this.attackLength = leftMotion.length;
		Swing();
	}
	
	public void SwingRight()
	{
		this.currectAttack = GetAttackName(rightAnimaitonName);
		this.attackLength = rightMotion.length;
		Swing();
	}

	public void HoldSword()
	{
		holdInput = true;
	}

	public void UnHoldSword()
	{
		unholdOverride = true;
	}

	private void DoHold()
	{
		SwordState = SwordStateType.Hold;
		playerAnimator.Play(GetHoldName());
	}
	
	private void Swing()
	{
		CancelSwing();
		this.SwordState = SwordStateType.Swinging;
		playerAnimator.Play(this.currectAttack);
		attackTimer = 0; 
	}

	private string GetAttackName(string newName)
	{
		if(this.currectAttack != null && 
		   this.currectAttack.Equals(newName))
		{
			return newName + copySuffix;
		}

		return newName;
	}

	private string GetHoldName()
	{
		if(this.currectAttack.Contains("_2"))
		{
			return this.holdAnimationPrefix + 
				this.currectAttack.Substring(0,this.currectAttack.Length - 2);
		}

		return this.holdAnimationPrefix + this.currectAttack;
	}

	void FixedUpdate()
	{
		if(this.SwordState == SwordStateType.Swinging)
		{
			attackTimer+= Time.fixedDeltaTime;
			if(attackTimer >= attackLength)
			{
				if(holdInput)
				{
					DoHold();
				}
				else 
				{
					CancelSwing();
				}
			}
		}

		if(this.SwordState == SwordStateType.Hold && !holdInput ||
		   this.SwordState == SwordStateType.Hold && unholdOverride)
		{
			CancelSwing();
		}

		unholdOverride = false;
		holdInput = false;
	}
}
