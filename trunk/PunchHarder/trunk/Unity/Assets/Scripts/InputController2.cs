using UnityEngine;
using System.Collections;

public class InputController2
{
    // Using this format:
    //
    //   W      I
    // A S D  J K L 
    //

    // Ouya:
    //   Y
    // U   A 
    //   O
    //

    // ouya parameter string values
    // SYS, DPC, DPD, DPL, DPR, DPU, O, U, Y, A, LT, RT, LB, RB, L3, R3

    //ouya axes
    // Left Analog: LY LX,  Right Analog: RY RX,  Trigger: RT, LT

    public static bool GetDown()
    {
        return 
            Input.GetKey(KeyCode.DownArrow) || 
            Input.GetKey(KeyCode.S) ||
            OuyaInputManager.GetButton("DPD", OuyaSDK.OuyaPlayer.player2) || 
            OuyaInputManager.GetAxis("LY", OuyaSDK.OuyaPlayer.player2) < -0.25f;
    }

    public static bool GetUp()
    {
        return 
            Input.GetKey(KeyCode.UpArrow) || 
            Input.GetKey(KeyCode.W) || 
            OuyaInputManager.GetButton("DPU", OuyaSDK.OuyaPlayer.player2) ||
            OuyaInputManager.GetAxis("LY", OuyaSDK.OuyaPlayer.player2) > 0.25f;
    }

    public static bool GetLeft()
    {
        return 
            Input.GetKey(KeyCode.LeftArrow) || 
            Input.GetKey(KeyCode.A) ||
            OuyaInputManager.GetButton("DPL", OuyaSDK.OuyaPlayer.player2) ||
            OuyaInputManager.GetAxis("LX", OuyaSDK.OuyaPlayer.player2) < -0.25f;
    }

    public static bool GetRight()
    {
        return 
            Input.GetKey(KeyCode.RightArrow) || 
            Input.GetKey(KeyCode.D) || 
            OuyaInputManager.GetButton("DPR", OuyaSDK.OuyaPlayer.player2) ||
            OuyaInputManager.GetAxis("LX", OuyaSDK.OuyaPlayer.player2) > 0.25f;
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
        return Input.GetKeyDown(KeyCode.K) ||
            OuyaInputManager.GetButtonDown("O", OuyaSDK.OuyaPlayer.player2);
    }

    public static bool GetPlayerInteractDown()
    {
        return Input.GetKeyDown(KeyCode.L) || 
            OuyaInputManager.GetButtonDown("A", OuyaSDK.OuyaPlayer.player2);
    }
}
