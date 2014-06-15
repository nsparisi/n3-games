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

	public static Vector3 FacingToUnitVector3(EntityFacing facing)
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

	public static EntityFacing RandomFacingThatIsntThisOne(EntityFacing thisOne)
	{
		int rand = Random.Range(0,3);
		EntityFacing[] choiceArray = new EntityFacing[3];
		int index = 0;
		if (thisOne != EntityFacing.Up)
		{
			choiceArray[index++] = EntityFacing.Up;
		}

		if (thisOne != EntityFacing.Down)
		{
			choiceArray[index++] = EntityFacing.Down;
		}

		if (thisOne != EntityFacing.Left)
		{
			choiceArray[index++] = EntityFacing.Left;
		}

		if(thisOne != EntityFacing.Right)
		{
			choiceArray[index++] = EntityFacing.Right;
		}

		return choiceArray[rand];
	}

	public static EntityFacing RotateClockwise(EntityFacing facing)
	{
		switch(facing)
		{
		case EntityFacing.Up:
			return EntityFacing.Right;
		case EntityFacing.Down:
			return EntityFacing.Left;
		case EntityFacing.Left:
			return EntityFacing.Up;
		case EntityFacing.Right:
			return EntityFacing.Down;
		default:
			return EntityFacing.Up;
		}
	}

	public static EntityFacing RotateCounterclockwise(EntityFacing facing)
	{
		switch(facing)
		{
		case EntityFacing.Up:
			return EntityFacing.Left;
		case EntityFacing.Down:
			return EntityFacing.Right;
		case EntityFacing.Left:
			return EntityFacing.Down;
		case EntityFacing.Right:
			return EntityFacing.Up;
		default:
			return EntityFacing.Up;
		}
	}

	public static EntityFacing OppositeFacing(EntityFacing facing)
	{
		switch(facing)
		{
		case EntityFacing.Up:
			return EntityFacing.Down;
		case EntityFacing.Down:
			return EntityFacing.Up;
		case EntityFacing.Left:
			return EntityFacing.Right;
		case EntityFacing.Right:
			return EntityFacing.Left;
		default:
			return EntityFacing.Up;
		}
		
	}
//	switch(facing)
//	{
//		case EntityFacing.Up:
//		break;
//		case EntityFacing.Down:
//		break;
//		case EntityFacing.Left:
//		break;
//		case EntityFacing.Right:
//		break;
//	}
}