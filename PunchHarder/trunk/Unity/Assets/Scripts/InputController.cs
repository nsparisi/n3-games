using UnityEngine;
using System.Collections;

public class InputController
{
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
        return Input.GetKey(KeyCode.O);
    }

    public static bool GetInventoryAccept()
    {
        return Input.GetKey(KeyCode.L);
    }

    public static bool GetInventoryDecline()
    {
        return Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.O);
    }


    // Player Action

    public static bool GetPlayerInspect()
    {
        return Input.GetKey(KeyCode.L);
    }

    public static bool GetPlayerInteract()
    {
        return Input.GetKey(KeyCode.P);
    }
}
