using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Humanoid : MonoBehaviour 
{
    public float speed = 5;
    public Rect bounds = new Rect(0, 0, 10, 10);
    public float actionDuration = 1;

    public GameObject DefaultVisual;
    public GameObject SeedVisual;
    public GameObject WaterVisual;
    public GameObject HarvestVisual;

    private enum VisualType { Default, Planting, Watering, Harvesting }
    private VisualType currentVisual;
    private bool isPerformingAction = false;

    private bool moveUp;
    private bool moveDown;
    private bool moveRight;
    private bool moveLeft;
    private bool actionWater;
    private bool actionSeed;
    private bool actionHarvest;

    // public methods 
    public void MoveUp() { moveUp = true; }
    public void MoveDown() { moveDown = true; }
    public void MoveRight() { moveRight = true; }
    public void MoveLeft() { moveLeft = true; }
    public void WaterAction() { actionWater = true; }
    public void SeedAction() { actionSeed = true; }
    public void HarvestAction() { actionHarvest = true; }

    private void ResetActions()
    {
        moveUp = false;
        moveDown = false;
        moveRight = false;
        moveLeft = false;
        actionWater = false;
        actionSeed = false;
        actionHarvest = false;
    }

    void Awake()
    {
        SetVisual(VisualType.Default);
    }

    protected void Update()
    {
        // performing an action halts simultaneous movement and actions
        if (!isPerformingAction)
        {
            // movement logic
            Vector3 movement = new Vector3();
            float distance = speed * Time.deltaTime * Map.Instance.tileWidth;

            if (moveUp)
            {
                movement.z += distance;
            }
            else if (moveDown)
            {
                movement.z -= distance;
            }
            else if (moveRight)
            {
                movement.x += distance;
            }
            else if (moveLeft)
            {
                movement.x -= distance;
            }

            if (movement != Vector3.zero)
            {
                TryMove(movement);
            }

            // action logic
            if (actionSeed && DoSeedAction())
            {
                StartCoroutine(DoAction(VisualType.Planting));
            }
            else if (actionHarvest && DoHarvestAction())
            {
                StartCoroutine(DoAction(VisualType.Harvesting));
            }
            else if (actionWater && DoWaterAction())
            {
                StartCoroutine(DoAction(VisualType.Watering));
            }
        }

        ResetActions();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1);

        Vector3 center = new Vector3();
        center.x = transform.localPosition.x + bounds.x;
        center.y = transform.localPosition.y;
        center.z = transform.localPosition.z + bounds.y;
        Vector3 size = new Vector3(bounds.width, 1, bounds.height);

        Gizmos.DrawWireCube(center, size);
    }


    protected Rect GetActualBounds(Vector3 position)
    {
        return new Rect(
            position.x + bounds.x,
            position.z + bounds.y,
            bounds.width,
            bounds.height);
    }

    private void TryMove(Vector3 amountToMove)
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

    private bool DoWaterAction()
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

    private bool DoSeedAction()
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

    private bool DoHarvestAction()
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

    private bool TryWaterSeed(SoilBin bin)
    {
        bin.FillWithWater();
        return true;
    }

    private bool TryPlantSeed(SoilBin bin)
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

    private bool TryHarvestPlant(SoilBin bin)
    {
        Seed seed = bin.Harvest();
        if (seed != null)
        {
            HarvestedFullyGrownPlant(seed);
            return true;
        }

        return false;
    }

    public List<SoilBin> GetAdjacentBins()
    {
        // find what tile I'm standing on
        int gridX, gridY;
        Map.Instance.GetClosestCoordinate(this.transform.position, out gridX, out gridY);

        return Map.Instance.GetAdjacentBins(gridX, gridY);
    }

    private Seed UseSeedFromInventory()
    {
        if (Inventory.NumberOfSeeds > 0)
        {
            Inventory.NumberOfSeeds--;
            return new Seed();
        }

        return null;
    }

    private void HarvestedFullyGrownPlant(Seed seed)
    {
        Inventory.NumberOfSeeds += seed.SeedYield;
        Inventory.PlantsHarvested++;
    }

    private bool Colliding(Rect? A, Rect? B)
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

    private IEnumerator DoAction(VisualType actionVisual)
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

    private void SetVisual(VisualType type)
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
