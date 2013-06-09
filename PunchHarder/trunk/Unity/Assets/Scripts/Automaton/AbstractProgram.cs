using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractProgram 
{
    protected Automaton automaton;
    abstract public void ProgramUpdate();
}
