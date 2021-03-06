using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeedProgram : AbstractProgram
{
    public SeedProgram(Automaton owner)
        : base(owner)
    {
    }

    protected override void  DoAction()
    {
        automaton.SeedAction();
    }

    protected override bool MeetCriteria(SoilBin bin)
    {
        return bin.CanPlantSeed();
    }
}
