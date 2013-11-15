using UnityEngine;
using System.Collections;


public class PlayerAnimation : MonoBehaviour {
	Animator animator;

	void Start()
	{
		animator = this.GetComponent<Animator>();
		animator.Play("linkwalkright");
	}

	void Update()
	{
		animator.Play ("linkwalkright");
	}
}
