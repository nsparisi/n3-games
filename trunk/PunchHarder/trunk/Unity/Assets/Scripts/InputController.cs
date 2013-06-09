using UnityEngine;
using System.Collections;

public class InputController
{
    // Using this format:
    //
    //   W      I
    // A S D  J K L 
    //

    public static bool GetDown()
    {
        return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
    }

    public static bool GetUp()
    {
        return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
    }

    public static bool GetLeft()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    public static bool GetRight()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    //Inventory
    public static bool GetInventoryButton()
    {
        return Input.GetKey(KeyCode.I);
    }

    public static bool GetInventoryAccept()
    {
        return Input.GetKey(KeyCode.L);
    }

    public static bool GetInventoryDecline()
    {
        return Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K);
    }

    // Player Action
    public static bool GetPlayerInspectDown()
    {
        return Input.GetKeyDown(KeyCode.K);
    }

    public static bool GetPlayerInteractDown()
    {
        return Input.GetKeyDown(KeyCode.L);
    }
}
