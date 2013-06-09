using UnityEngine;
using System.Collections;
using System;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 50), "Total Harvest: " + Player.Instance.plantsHarvested);
        GUI.Label(new Rect(10, 25, 150, 50), "Seeds in Inventory: " + Player.Instance.numberOfSeeds);
    }
}
