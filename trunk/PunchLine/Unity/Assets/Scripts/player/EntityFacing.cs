using UnityEngine;

public enum EntityFacing
{
	Up, Down, Left, Right 
}

public class Facing
{
	public static EntityFacing DirectionToFacing(Vector3 direction)
	{
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
		{
			if (direction.x > 0)
			{
				return EntityFacing.Right;
			}
			else
			{
				return EntityFacing.Left;
			}
		}
		else
		{
			if(direction.y > 0)
			{
				return EntityFacing.Up;
			}
			else
			{
				return EntityFacing.Down;
			}
		}
	}

	public Vector3 FacingToUnitVector3(EntityFacing facing)
	{
		switch(facing)
		{
		case EntityFacing.Up:
			return new Vector3(0,1);
		case EntityFacing.Down:
			return new Vector3(0,-1);
		case EntityFacing.Left:
			return new Vector3(-1,0);
		case EntityFacing.Right:
			return new Vector3(1,0);
		default:
			return new Vector3(0,0);
		}
	}
}