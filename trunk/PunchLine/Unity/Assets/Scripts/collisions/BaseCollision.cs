﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BaseCollision : MonoBehaviour 
{
	void Start()
	{
		this.collider.isTrigger = true;				
	}
}