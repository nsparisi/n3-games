using UnityEngine;
using System.Collections;

public class PlayerSword : MonoBehaviour {
	
	public enum SwordStateType { Swinging, NotSwinging }
	public SwordStateType SwordState { get; private set; }

	public Animator playerAnimator;
	public Motion upMotion;
	public Motion downMotion;
	public Motion leftMotion;
	public Motion rightMotion;

	private string downAnimaitonName = "attackdown";
	private string upAnimaitonName = "attackup";
	private string leftAnimaitonName = "attackleft";
	private string rightAnimaitonName = "attackright";
	private string currectAttack;
	private const string copySuffix = "_2";

	float attackLength = 0;
	float attackTimer = 0;

	public void CancelSwing()
	{
		this.SwordState = SwordStateType.NotSwinging;
	}
	
	public void SwingUp()
	{
		this.currectAttack = GetAttackName(upAnimaitonName);
		this.attackLength = upMotion.averageDuration;
		Swing();
	}
	
	public void SwingDown()
	{
		this.currectAttack = GetAttackName(downAnimaitonName);
		this.attackLength = downMotion.averageDuration;
		Swing();
	}
	
	public void SwingLeft()
	{
		this.currectAttack = GetAttackName(leftAnimaitonName);
		this.attackLength = leftMotion.averageDuration;
		Swing();
	}
	
	public void SwingRight()
	{
		this.currectAttack = GetAttackName(rightAnimaitonName);
		this.attackLength = rightMotion.averageDuration;
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
			attackTimer+= Time.fixedDeltaTime;
			if(attackTimer >= attackLength)
			{
				Debug.Log(attackLength);
				CancelSwing();
			}
		}
	}
}
