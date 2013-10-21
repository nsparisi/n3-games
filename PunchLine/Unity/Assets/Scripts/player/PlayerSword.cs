using UnityEngine;
using System.Collections;

public class PlayerSword : MonoBehaviour {
	
	public enum SwordStateType { Swinging, NotSwinging }
	public SwordStateType SwordState { get; private set; }
	
	public AttackSequence upAttackSequence;
	public AttackSequence downAttackSequence;
	public AttackSequence leftAttackSequence;
	public AttackSequence rightAttackSequence;
	
	private AttackSequence currentAttackSequence;
	
	public void CancelSwing()
	{
		this.SwordState = SwordStateType.NotSwinging;
		this.upAttackSequence.StopSequence();
		this.downAttackSequence.StopSequence();
		this.leftAttackSequence.StopSequence();
		this.rightAttackSequence.StopSequence();
	}
	
	public void SwingUp()
	{
		this.currentAttackSequence = upAttackSequence;
		Swing();
	}
	
	public void SwingDown()
	{
		this.currentAttackSequence = downAttackSequence;
		Swing();
	}
	
	public void SwingLeft()
	{
		this.currentAttackSequence = leftAttackSequence;
		Swing();
	}
	
	public void SwingRight()
	{
		this.currentAttackSequence = rightAttackSequence;
		Swing();
	}
	
	private void Swing()
	{
		CancelSwing();
		this.SwordState = SwordStateType.Swinging;
		this.currentAttackSequence.StartSequence();
	}
	
	void FixedUpdate()
	{
		if(this.SwordState == SwordStateType.Swinging)
		{
			this.currentAttackSequence.AdvanceOneFrame();
			if(this.currentAttackSequence.IsFinished)
			{
				CancelSwing();
			}
		}
	}
}
