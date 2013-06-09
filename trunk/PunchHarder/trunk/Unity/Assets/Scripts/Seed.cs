using UnityEngine;
using System.Collections;

public class Seed
{
    public string Description
    {
        get
        {
            return descriptions[currentLevel];
        }
    }

    public bool ReadyToHarvest
    {
        get
        {
            return currentLevel == maxLevel;
        }
    }

    public bool HasSprouted
    {
        get
        {
            return currentLevel > 0;
        }
    }

    public int SeedYield
    {
        get
        {
            return seedYield;
        }
    }


    protected float consumeRate = 10; // per second
    protected float waterNeededToLevel = 60;
    protected int maxLevel = 2;
    protected string[] descriptions = {"It was just planted.", "It looks healthy.", "It's ready to harvest."};
    protected int seedYield = 2;

    protected SoilBin bin;
    protected float waterConsumed;
    protected int currentLevel;
    
    public Seed()
    {
        currentLevel = 0;
        waterConsumed = 0;
    }

    public void AddToBin(SoilBin bin)
    {
        this.bin = bin;
    }

    public void DrinkUpdate()
    {
        if (currentLevel < maxLevel)
        {
            // drink available water from bin
            float amountConsumed = Mathf.Min(consumeRate * Time.deltaTime, bin.WaterAvailable);
            bin.WaterAvailable -= amountConsumed;
            waterConsumed += amountConsumed;

            // level up
            if (waterConsumed >= waterNeededToLevel)
            {
                currentLevel++;
                waterConsumed -= waterNeededToLevel;
            }
        }
    }
}
