using UnityEngine;
using System.Collections;


public class PlayerAnimation : MonoBehaviour {
	Animation myAnimation;

	void Start()
	{
		Animator animator = this.GetComponent<Animator>();
//		animator.Play("linkwalkright");
	}
}
