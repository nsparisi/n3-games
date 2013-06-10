using UnityEngine;
using System.Collections;
using System;

public class SoilBin : MonoBehaviour
{
    public GameObject EmptyVisual;
    public GameObject SeedVisual;
    public GameObject SproutVisual;
    public GameObject FullGrownVisual;
    public GameObject UsedVisual;

    public float WaterAvailable { get; set; }

    public Type SeedType { get; private set; }

    public float PercentFull
    {
        get
        {
            return WaterAvailable / maxWaterCapacity;
        }
    }

    private bool isGettingWateredSoon;
    public bool IsGettingWateredSoon
    {
        get
        {
            return isGettingWateredSoon;
        }

        set
        {
            if (value && !isGettingWateredSoon)
            {
                isGettingWateredSoon = true;
                StartCoroutine(BeginWaitOnWater());
            }

            if (!value)
            {
                StopCoroutine("BeginWaitOnWater");
                isGettingWateredSoon = false;
            }
        }
    }

    IEnumerator BeginWaitOnWater()
    {
        yield return new WaitForSeconds(timeToWaitOnWater);
        isGettingWateredSoon = false;
    }

    private float maxWaterCapacity = 60.1f;
    private float timeToWaitOnWater = 5;

    private Seed seed;

    void Start()
    {
        WaterAvailable = 0;
        SetVisual(VisualType.Empty);
    }

    void Update()
    {
        if (seed != null)
        {
            seed.DrinkUpdate();

            if (seed.HasSprouted && currentVisual == VisualType.Seed)
            {
                SetVisual(VisualType.Sprouted);
            }

            if (seed.ReadyToHarvest && currentVisual != VisualType.FullGrown)
            {
                SetVisual(VisualType.FullGrown);
            }
        }
    }

    /// <summary>
    /// Gets the status of the bin.
    /// </summary>
    /// <returns></returns>
    public string Check()
    {
        if (seed != null)
        {
            return seed.Description;
        }

        return "It's an empty soil bin.";
    }

    /// <summary>
    /// True if this bin is ready to receive a seed
    /// </summary>
    /// <returns></returns>
    public bool CanPlantSeed()
    {
        return seed == null;
    }

    /// <summary>
    /// Plants a seed in this bin if there is no seed already here.
    /// </summary>
    /// <param name="newSeed"></param>
    /// <returns></returns>
    public bool PlantSeed(Seed newSeed)
    {
        if (CanPlantSeed())
        {
            newSeed.AddToBin(this);
            this.seed = newSeed;
            this.SeedType = newSeed.GetType();
            SetVisual(VisualType.Seed);
            return true;
        }

        return false;
    }

    /// <summary>
    /// True if this bin is ready to receive water
    /// </summary>
    /// <returns></returns>
    public bool CanAddWater()
    {
        return seed != null && !seed.ReadyToHarvest;
    }

    /// <summary>
    /// Adds water to the bin. Returns the amount of water taken.
    /// </summary>
    public void FillWithWater()
    {
        WaterAvailable = maxWaterCapacity;
        IsGettingWateredSoon = false;
    }

    /// <summary>
    /// True if this bin is ready to harvest
    /// </summary>
    /// <returns></returns>
    public bool CanHarvest()
    {
        return seed != null && seed.ReadyToHarvest;
    }

    /// <summary>
    /// Harvest the seed from the bin, if there is a seed ready and available.
    /// </summary>
    /// <returns>The fully grown seed.</returns>
    public Seed Harvest()
    {
        if (seed != null && seed.ReadyToHarvest)
        {
            SetVisual(VisualType.Used);
            Seed toHarvest = this.seed;
            this.seed = null;
            return toHarvest;
        }

        return null;
    }


    private enum VisualType { Empty, Seed, Sprouted, FullGrown, Used }
    private VisualType currentVisual;
    private void SetVisual(VisualType type)
    {
        currentVisual = type;

        EmptyVisual.SetActive(false);
        SeedVisual.SetActive(false);
        SproutVisual.SetActive(false);
        FullGrownVisual.SetActive(false);
        UsedVisual.SetActive(false);

        switch (type)
        {
            case VisualType.Empty:
                EmptyVisual.SetActive(true);
                break;
            case VisualType.Seed:
                SeedVisual.SetActive(true);
                break;
            case VisualType.Sprouted:
                SproutVisual.SetActive(true);
                break;
            case VisualType.FullGrown:
                FullGrownVisual.SetActive(true);
                break;
            case VisualType.Used:
                UsedVisual.SetActive(true);
                break;
        }
    }
}
