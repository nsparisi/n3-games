using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Humanoid 
{
    // This is just for fun :)
    public enum PlayerSlot { Player1, Player2 }
    public PlayerSlot slot;

    public static Player Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    new void Update()
    {
        base.Update();

        // let base class know when to move
        if (InputController.GetUp() && slot == PlayerSlot.Player1 ||
            InputController2.GetUp() && slot == PlayerSlot.Player2)
        {
            base.MoveUp();
        }
        else if (InputController.GetDown() && slot == PlayerSlot.Player1 ||
            InputController2.GetDown() && slot == PlayerSlot.Player2)
        {
            base.MoveDown();
        }
        else if (InputController.GetRight() && slot == PlayerSlot.Player1 ||
            InputController2.GetRight() && slot == PlayerSlot.Player2)
        {
            base.MoveRight();
        }
        else if (InputController.GetLeft() && slot == PlayerSlot.Player1 ||
            InputController2.GetLeft() && slot == PlayerSlot.Player2)
        {
            base.MoveLeft();
        }

        // let base class know when to take action
        if (InputController.GetPlayerInteractDown() && slot == PlayerSlot.Player1 ||
            InputController2.GetPlayerInteractDown() && slot == PlayerSlot.Player2)
        {
            base.HarvestAction();
            base.WaterAction();
            base.SeedAction();
        }
    }
}
