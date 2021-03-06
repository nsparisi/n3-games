﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridSprite))]

public class SpriteAnimationController : MonoBehaviour {
	public Dictionary<string, SpriteAnimation> animations;
	
	public bool paused = false;
	
	GridSprite sprite;
	SpriteAnimation currentAnimation;
	int currentAnimationFrame = 0;
	int frameCount = 0;
	int animationDirection = 1;
	
	void Awake()
	{
		this.sprite = GetComponent<GridSprite>();
		animations = new Dictionary<string, SpriteAnimation>();
	}
	
	void Start()
	{
		SpriteAnimation newSpriteAnimation = new SpriteAnimation();
		newSpriteAnimation.frameIndices = new int[]{ 0,1,2,3,4,5,6,7};
		newSpriteAnimation.frameCounts = new int[]{ 1,1,1,1,1,1,1,1 };
		newSpriteAnimation.reverseOnLoop = false;
		newSpriteAnimation.loop = true;
		AddAnimation("walk up", newSpriteAnimation);
		PlayAnimation("walk up");
		
		newSpriteAnimation = new SpriteAnimation();
		newSpriteAnimation.frameIndices = new int[]{ 8, 9, 10, 11, 12, 13};
		newSpriteAnimation.frameCounts = new int[]{ 1,1,1,1,1,1 };
		newSpriteAnimation.reverseOnLoop = false;
		newSpriteAnimation.loop = true;
		AddAnimation("walk right", newSpriteAnimation);
		PlayAnimation("walk right");
	}
	
	void SetCurrentAnimation(SpriteAnimation newAnimation)
	{
		currentAnimation = newAnimation;
		frameCount = 0;
		currentAnimationFrame = 0;
		SetFrame(currentAnimation.frameIndices[0]);
	}
	
	void Update()
	{
		if (paused)
		{
			return;
		}
		
		if (++frameCount >  currentAnimation.frameCounts[currentAnimationFrame])
		{
			currentAnimationFrame += animationDirection;
			if (currentAnimationFrame > currentAnimation.frameIndices.Length-1 || currentAnimationFrame < 0)
			{
				if(currentAnimation.reverseOnLoop)
				{
					animationDirection *= -1;
					if (animationDirection < 0)
					{
						currentAnimationFrame = currentAnimation.frameIndices.Length-2;
					}
					else
					{
						if (currentAnimation.frameIndices.Length > 1)
						{
							currentAnimationFrame = 1;
						}
						else
						{
							currentAnimationFrame = 0;
						}
					}
				}
				else if (currentAnimation.loop)
				{
					currentAnimationFrame = 0;
				}
				else
				{
					currentAnimationFrame = currentAnimation.frameIndices.Length-1;
				}
			}
			
			frameCount = 0;
			SetFrame(currentAnimation.frameIndices[currentAnimationFrame]);
		}
	}
				
	void SetFrame(int newFrame)
	{
		sprite.CurrentFrame = newFrame;
	}
	
	public void Pause()
	{
		paused = true;
	}
	
	public void Play()
	{
		paused = false;
	}
	
	public void AddAnimation(string animationName, SpriteAnimation animation)
	{
		if (animations.ContainsKey(animationName))
		{
			animations[animationName] = animation;
		}
		else
		{
			animations.Add(animationName, animation);
		}	
	}
	
	public bool PlayAnimation(string animationName)
	{
		paused = false;
		if(animations.ContainsKey(animationName))
		{
			SetCurrentAnimation(animations[animationName]);
			return true;
		}
		else
		{
			return false;
		}
	}
}

public class SpriteAnimation
{
	public bool loop;
	public bool reverseOnLoop;
			
	public int[] frameIndices;
	public int[] frameCounts;
}
