using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HarvestProgram : AbstractProgram
{
    public HarvestProgram(Automaton owner)
        : base(owner)
    {
    }

    protected override void  DoAction()
    {
        automaton.HarvestAction();
    }

    protected override bool MeetCriteria(SoilBin bin)
    {
        return bin.CanHarvest();
    }
}
