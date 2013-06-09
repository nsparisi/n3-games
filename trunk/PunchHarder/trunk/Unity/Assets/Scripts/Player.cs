using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Humanoid 
{    
    public static Player Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    new void Update()
    {
        base.Update();

        // let base class know when to move
        if (InputController.GetUp())
        {
            base.MoveUp();
        }
        else if (InputController.GetDown())
        {
            base.MoveDown();
        }
        else if (InputController.GetRight())
        {
            base.MoveRight();
        }
        else if (InputController.GetLeft())
        {
            base.MoveLeft();
        }

        // let base class know when to take action
        if (InputController.GetPlayerInteractDown())
        {
            base.HarvestAction();
            base.WaterAction();
            base.SeedAction();
        }
    }
}
