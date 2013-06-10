using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterProgram : AbstractProgram
{
    float waterThreshold = 0.2f;

    public WaterProgram(Automaton owner)
        : base(owner)
    {
    }

    protected override void  DoAction()
    {
        automaton.WaterAction();
    }

    protected override bool MeetCriteria(SoilBin bin)
    {
        if (bin.CanAddWater() && 
            bin.PercentFull <= waterThreshold && 
            !bin.IsGettingWateredSoon &&
            !bin.CanHarvest())
        {
            bin.IsGettingWateredSoon = true;
            return true;
        }

        return false;
    }
}
