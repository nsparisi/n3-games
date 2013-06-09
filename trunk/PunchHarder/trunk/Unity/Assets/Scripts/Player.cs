using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    public float speed = 5;
    public Rect bounds = new Rect(0, 0, 10, 10);
    public int numberOfSeeds = 10;
    public int plantsHarvested = 0;
    public float actionDuration = 1;

    public GameObject DefaultVisual;
    public GameObject SeedVisual;
    public GameObject WaterVisual;
    public GameObject HarvestVisual;
    
    public static Player Instance { get; private set; }

    private bool isPerformingAction = false;

    void Awake()
    {
        Instance = this;
        SetVisual(VisualType.Default);
    }

    void Update()
    {
        Vector3 movement = new Vector3();
        float distance = speed * Time.deltaTime * Map.Instance.tileWidth;

        if (InputController.GetUp())
        {
            movement.z += distance;
        }
        else if (InputController.GetDown())
        {
            movement.z -= distance;
        }
        else if (InputController.GetRight())
        {
            movement.x += distance;
        }
        else if (InputController.GetLeft())
        {
            movement.x -= distance;
        }
        
        // performing an action halts simultaneous movement and actions
        if (!isPerformingAction)
        {
            // try to move around
            if (movement != Vector3.zero)
            {
                TryMove(movement);
            }

            // try to perform an action
            if (InputController.GetPlayerInteractDown())
            {
                if (PlantSeedAction())
                {
                    Debug.Log("Planted a seed!");
                    StartCoroutine(DoAction(VisualType.Planting));
                }
                else if (HarvestPlantAction())
                {
                    Debug.Log("Harvested a plant!");
                    StartCoroutine(DoAction(VisualType.Harvesting));
                }
                else if (WaterPlantAction())
                {
                    Debug.Log("Watered a plant!");
                    StartCoroutine(DoAction(VisualType.Watering));
                }
            }
        }
    }


    void TryMove(Vector3 amountToMove)
    {
        // where I want to be
        Vector3 goToHere = this.transform.localPosition + amountToMove;

        // find what tile I'm standing on
        int gridX, gridY;
        Map.Instance.GetClosestCoordinate(goToHere, out gridX, out gridY);

        // check in a box around the player
        int xMin = Mathf.RoundToInt(gridX - 1);
        int xMax = Mathf.RoundToInt(gridX + 1);
        int yMin = Mathf.RoundToInt(gridY - 1);
        int yMax = Mathf.RoundToInt(gridY + 1);

        Rect futureBounds = GetActualBounds(goToHere);

        bool isColliding = false;
        for (int i = xMin; i <= xMax; i++)
        {
            for (int j= yMin; j <= yMax; j++)
            {
                Rect? other = Map.Instance.GetTileBounds(i, j);
                isColliding = isColliding || Colliding(futureBounds, other);
            }
        }

        // didn't run into anything?
        if (!isColliding)
        {
            this.transform.localPosition = goToHere;
        }
    }


    bool WaterPlantAction()
    {
        List<SoilBin> adjacentBins = GetAdjacentBins();
        foreach (SoilBin bin in adjacentBins)
        {
            if (TryWaterSeed(bin))
            {
                return true;
            }
        }

        return false;
    }

    bool TryWaterSeed(SoilBin bin)
    {
        bin.FillWithWater();
        return true;
    }

    bool PlantSeedAction()
    {
        List<SoilBin> adjacentBins = GetAdjacentBins();
        foreach (SoilBin bin in adjacentBins)
        {
            if (TryPlantSeed(bin))
            {
                return true;
            }
        }

        return false;
    }

    bool TryPlantSeed(SoilBin bin)
    {
        if (bin.CanPlantSeed())
        {
            Seed seed = UseSeedFromInventory();
            if (seed != null)
            {
                bin.PlantSeed(seed);
                return true;
            }
        }

        return false;
    }

    bool HarvestPlantAction()
    {
        List<SoilBin> adjacentBins = GetAdjacentBins();
        foreach (SoilBin bin in adjacentBins)
        {
            if (TryHarvestPlant(bin))
            {
                return true;
            }
        }

        return false;
    }

    bool TryHarvestPlant(SoilBin bin)
    {
        Seed seed = bin.Harvest();
        if (seed != null)
        {
            HarvestedFullyGrownPlant(seed);
            return true;
        }

        return false;
    }
    
    List<SoilBin> GetAdjacentBins()
    {
        // find what tile I'm standing on
        int gridX, gridY;
        Map.Instance.GetClosestCoordinate(this.transform.position, out gridX, out gridY);

        //check up down left right
        GameObject[] adjacentTiles = new GameObject[4];
        adjacentTiles[0] = Map.Instance.GetTileGameObject(gridX, gridY + 1);
        adjacentTiles[1] = Map.Instance.GetTileGameObject(gridX, gridY - 1);
        adjacentTiles[2] = Map.Instance.GetTileGameObject(gridX - 1, gridY);
        adjacentTiles[3] = Map.Instance.GetTileGameObject(gridX + 1, gridY);

        //find bins
        List<SoilBin> adjacentBins = new List<SoilBin>();
        for (int i = 0; i < adjacentTiles.Length; i++)
        {
            SoilBin bin = TryGetBin(adjacentTiles[i]);
            if (bin != null)
            {
                adjacentBins.Add(bin);
            }
        }
        
        return adjacentBins;
    }

    SoilBin TryGetBin(GameObject go)
    {
        if (go != null)
        {
            return go.GetComponent<SoilBin>();
        }

        return null;
    }

    Seed UseSeedFromInventory()
    {
        if (numberOfSeeds > 0)
        {
            numberOfSeeds--;
            return new Seed();
        }

        return null;
    }

    void HarvestedFullyGrownPlant(Seed seed)
    {
        numberOfSeeds += seed.SeedYield;
        plantsHarvested++;
    }

    bool Colliding(Rect? A, Rect? B)
    {
        if (!A.HasValue || !B.HasValue)
        {
            return false;
        }

        Rect A_Rect = A.Value;
        float A_MinX = A_Rect.x - A_Rect.width * 0.5f;
        float A_MinY = A_Rect.y - A_Rect.height * 0.5f;
        float A_MaxX = A_Rect.x + A_Rect.width * 0.5f;
        float A_MaxY = A_Rect.y + A_Rect.height * 0.5f;

        Rect B_Rect = B.Value;
        float B_MinX = B_Rect.x - B_Rect.width * 0.5f;
        float B_MinY = B_Rect.y - B_Rect.height * 0.5f;
        float B_MaxX = B_Rect.x + B_Rect.width * 0.5f;
        float B_MaxY = B_Rect.y + B_Rect.height * 0.5f;

        return !(A_MinX > B_MaxX || B_MinX > A_MaxX ||
            A_MinY > B_MaxY || B_MinY > A_MaxY);
    }

    List<Rect> gizmoColliding = new List<Rect>();
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1);

        Vector3 center = new Vector3();
        center.x = transform.localPosition.x + bounds.x;
        center.y = transform.localPosition.y;
        center.z = transform.localPosition.z + bounds.y;
        Vector3 size = new Vector3(bounds.width, 1, bounds.height);

        Gizmos.DrawWireCube(center, size);

        foreach (Rect other in gizmoColliding)
        {
            Gizmos.color =new Color(1,0,0,0.3f);

            center = new Vector3(other.x, 0, other.y);
            size = new Vector3(other.width, 1, other.height);

            Gizmos.DrawCube(center, size);
        }
    }

    Rect GetActualBounds(Vector3 position)
    {
        return new Rect(
            position.x + bounds.x,
            position.z + bounds.y,
            bounds.width,
            bounds.height);
    }

    IEnumerator DoAction(VisualType actionVisual)
    {
        // pre action
        isPerformingAction = true;
        SetVisual(actionVisual);

        // do action
        yield return new WaitForSeconds(actionDuration);

        // post action
        SetVisual(VisualType.Default);
        isPerformingAction = false;
    }

    protected enum VisualType { Default, Planting, Watering, Harvesting }
    protected VisualType currentVisual;
    protected void SetVisual(VisualType type)
    {
        currentVisual = type;

        HarvestVisual.SetActive(false);
        SeedVisual.SetActive(false);
        WaterVisual.SetActive(false);
        DefaultVisual.SetActive(false);

        switch (type)
        {
            case VisualType.Default:
                DefaultVisual.SetActive(true);
                break;
            case VisualType.Planting:
                SeedVisual.SetActive(true);
                break;
            case VisualType.Watering:
                WaterVisual.SetActive(true);
                break;
            case VisualType.Harvesting:
                HarvestVisual.SetActive(true);
                break;
        }
    }

}
