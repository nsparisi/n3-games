using UnityEngine;
using System.Collections;

public class TileObject
{
    public Map.TileType type;
    public GameObject gameObject;
    public int x, y;
    public Rect? bounds;

    public TileObject(int x, int y)
    {
        this.x = x;
        this.y = y;
        type = Map.TileType.Default;
    }
}
