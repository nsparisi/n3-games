using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Automaton : Humanoid
{
    //edit in unity
    public WorkMode currentMode = WorkMode.Idle;

    public enum WorkMode { Idle, Seed, Water, Harvest }

    AbstractProgram currentProgram;
    IdleProgram idleProgram;
    HarvestProgram harvestProgram;
    SeedProgram seedProgram;
    WaterProgram waterProgram;


    void Start()
    {
        idleProgram = new IdleProgram(this);
        harvestProgram = new HarvestProgram(this);
        seedProgram = new SeedProgram(this);
        waterProgram = new WaterProgram(this);
        SetWorkMode(currentMode);
    }

    new void Update()
    {
        base.Update();
        currentProgram.ProgramUpdate();

        SetWorkMode(currentMode);
    }

    public void SetWorkMode(WorkMode mode)
    {
        switch (mode)
        {
            case WorkMode.Harvest:
                currentProgram = harvestProgram;
                break;
            case WorkMode.Idle:
                currentProgram = idleProgram;
                break;
            case WorkMode.Seed:
                currentProgram = seedProgram;
                break;
            case WorkMode.Water:
                currentProgram = waterProgram;
                break;
        }
    }
}
