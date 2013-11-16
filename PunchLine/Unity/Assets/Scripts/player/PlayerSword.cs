using UnityEngine;
using System.Collections;

public class PlayerSword : MonoBehaviour {
	
	public enum SwordStateType { Swinging, NotSwinging }
	public SwordStateType SwordState { get; private set; }

	public Animator playerAnimator;
	private string downAnimaitonName = "attackdown";
	private string upAnimaitonName = "attackup";
	private string leftAnimaitonName = "attackleft";
	private string rightAnimaitonName = "attackright";
	private string currectAttack;
	private const string copySuffix = "_2";

	float attackLength = 0;
	float attackTimer = 0;

	public Motion downMotion;

	public void CancelSwing()
	{
		this.SwordState = SwordStateType.NotSwinging;
	}
	
	public void SwingUp()
	{
		this.currectAttack = GetAttackName(upAnimaitonName);
		Swing();
	}
	
	public void SwingDown()
	{
		this.currectAttack = GetAttackName(downAnimaitonName);
		Swing();
	}
	
	public void SwingLeft()
	{
		this.currectAttack = GetAttackName(leftAnimaitonName);
		Swing();
	}
	
	public void SwingRight()
	{
		this.currectAttack = GetAttackName(rightAnimaitonName);
		Swing();
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

	void FixedUpdate()
	{
		if(this.SwordState == SwordStateType.Swinging)
		{
			//AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
			//AnimationInfo[] infos = playerAnimator.GetCurrentAnimationClipState(0);

			attackTimer+= Time.fixedDeltaTime;
			if(attackTimer >= 0.25f)
			{
				CancelSwing();
			}
		}
	}
}
