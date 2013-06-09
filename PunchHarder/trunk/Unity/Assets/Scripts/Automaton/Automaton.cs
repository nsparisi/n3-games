using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Automaton : Humanoid 
{
    AbstractProgram program;

    void Start()
    {
        program = new SeedProgram(this);
    }

    new void Update()
    {
        base.Update();
        program.ProgramUpdate();
    }
}
