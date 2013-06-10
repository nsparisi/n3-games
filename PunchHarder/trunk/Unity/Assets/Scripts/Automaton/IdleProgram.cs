using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdleProgram : AbstractProgram
{
    public IdleProgram(Automaton owner)
        : base(owner)
    {
    }

    protected override void  DoAction()
    {
        
    }

    protected override bool MeetCriteria(SoilBin bin)
    {
        return false;
    }
}
