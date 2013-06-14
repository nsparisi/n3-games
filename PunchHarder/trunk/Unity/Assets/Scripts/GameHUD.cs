using UnityEngine;
using System.Collections;
using System;

public class GameHUD : MonoBehaviour
{
    public float RefreshRate = 1;
    public static GameHUD Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    float lastFPS;

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 50), "Total Harvest: " + Inventory.PlantsHarvested);
        GUI.Label(new Rect(10, 25, 150, 50), "Seeds in Inventory: " + Inventory.NumberOfSeeds);
        GUI.Label(new Rect(10, 40, 150, 50), "FPS: " + lastFPS);

        //shows ouya controller raw input
        //GUI.Label(new Rect(10, 50, 200, 200), "OUYA Raw:\n " + OuyaGameObject.InputData);
    }

    int fpsCount;
    float timer;
    void Update()
    {
        fpsCount++;
        timer += Time.deltaTime;
        if (timer >= RefreshRate)
        {
            lastFPS = fpsCount / RefreshRate;

            timer -= RefreshRate;
            fpsCount = 0;
        }
    }
}
