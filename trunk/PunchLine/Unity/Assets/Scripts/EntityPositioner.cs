using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityPositioner
{
	public static float CollisionZPosition = -1;
	
	// mess with these values 
	private static float zStart = -5;
	private static float zInterval = -30;
	
	// don't mess with these
	private static Queue<int> vacantIndexes = new Queue<int>();
	private static int nextIndex = 0;
	
	/// <summary>
	/// Requests for a position z-value to assign to an entity.
	/// </summary>
	/// <returns>
	/// A vacant z-value.
	/// </returns>
	public static float RequestPosition()
	{
		int indexToReturn;
		
		// reuse old positions if available
		if(vacantIndexes.Count != 0)
		{
			indexToReturn = vacantIndexes.Dequeue();
		}
		
		// otherwise claim more position spots 
		else 
		{
			indexToReturn = nextIndex;
			nextIndex++;
		}
		
		return ConvertIndexToFloat(indexToReturn);
	}
	
	/// <summary>
	/// Returns an entity's z-value used when an entity needs 
	/// to be cleaned up and no longer exists.
	/// </summary>
	/// <param name='zPosition'>
	/// The z-position of the entity.
	/// </param>
	public static void ReturnPosition(float zPosition)
	{
		int index = ConvertFloatToIndex(zPosition);
		vacantIndexes.Enqueue(index);
	}
	
	private static float ConvertIndexToFloat(int index)
	{
		return index * zInterval + zStart;
	}
	
	private static int ConvertFloatToIndex(float index)
	{
		return Mathf.RoundToInt((index - zStart) / zInterval);
	}
}

