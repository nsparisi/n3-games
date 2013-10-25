using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class GridSprite : Sprite {
	
	public int framesWide = 1;
	public int FramesWide {
		get {
			return framesWide;
		}
		
		set {
			framesWide = 0;
		}
	}
	
	public int framesHigh = 1;
	public int FramesHigh {
		get {
			return framesHigh;
		}
		set {
			framesHigh = value;
		}
	}

	public int currentFrame;
	public int CurrentFrame {
		get {
			return currentFrame;
		}
		
		set {
			currentFrame = value;
			
			if (currentFrame >= (FramesHigh*FramesWide)-1)
			{
				currentFrame = 0;
			}
			
			int spriteRow = currentFrame / FramesWide;
			int spriteColumn = currentFrame % FramesWide;
			
			float frameWidth = 1f / FramesWide;
			float frameHeight = 1f / FramesHigh;
			
			float left = spriteColumn*frameWidth;
			float bottom = spriteRow*frameHeight;
			float right = (spriteColumn+1)*frameWidth;
			float top = (spriteRow+1)*frameHeight;
			UV = new Vector4(left, bottom, right, top);
		}
	}
	
	public new void Awake()
	{
		base.Awake();
		CurrentFrame = currentFrame;
	}
}
