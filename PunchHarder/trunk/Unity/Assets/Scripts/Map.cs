using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    // globals
    public Vector3 origin = new Vector3(0, 0, 0);
    public float tileWidth = 10;
    public float tileHeight = 10;

    public static Map Instance { get; private set; }

    // purely visual objects
    public GameObject goBin;
    public GameObject goBinSeed;
    public GameObject goBinSprout;
    public GameObject goBinFullGrown;
    public GameObject goDesk;
    public GameObject goFloor;

    public enum TileType { 
        Default = 0,
        Bin = 1,
        BinSeed = 2,
        BinSprout = 3,
        BinFullGrown = 4,
        Desk = 5,
    }

    private TileObject[][] tiles;
    private int gridWidth;
    private int gridHeight;

    public Vector2 GetClosestCoordinate(Vector3 position)
    {
        float x = Mathf.Round(position.x / tileWidth);
        float y = Mathf.Round(position.z / tileHeight);

        return new Vector2(
            Mathf.Clamp(x, 0, gridWidth), 
            Mathf.Clamp(y, 0, gridHeight));
    }

    public Rect? GetTileBounds(int x, int y)
    {
        if (IsInBounds(x, y))
        {
            return tiles[x][y].bounds;
        }

        return null;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        goBin.transform.localScale = 0.1f * new Vector3(tileWidth, 1, tileHeight);
        goBinSeed.transform.localScale = 0.1f * new Vector3(tileWidth, 1, tileHeight);
        goBinSprout.transform.localScale = 0.1f * new Vector3(tileWidth, 1, tileHeight);
        goBinFullGrown.transform.localScale = 0.1f * new Vector3(tileWidth, 1, tileHeight);
        goDesk.transform.localScale = 0.1f * new Vector3(tileWidth, 1, tileHeight);
        goFloor.transform.localScale = 0.1f * new Vector3(tileWidth, 1, tileHeight);


        int[] prototypeMap = 
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            1, 0, 0, 1, 0, 0, 1, 0, 0, 1,
            1, 0, 0, 1, 0, 0, 1, 0, 0, 1,
            1, 0, 0, 1, 0, 0, 1, 0, 0, 1,
            1, 0, 0, 1, 0, 0, 1, 0, 0, 1,
            1, 0, 0, 1, 0, 0, 1, 0, 0, 1,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 5, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };

        CreateMap(prototypeMap, 10, 10);
    }


    private void CreateMap(int[] map, int width, int height)
    {
        this.gridWidth = width;
        this.gridHeight = height;

        tiles = new TileObject[width][];
        for(int i = 0; i < width; i++)
        {
            tiles[i] = new TileObject[height];
            for (int j = 0; j < height; j++)
            {
                tiles[i][j] = new TileObject(i, j);
            }
        }

        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                SetTile(i, height - j - 1, (TileType)map[j * width + i]);
            }
        }
    }


    private void SetTile(int x, int y, TileType tileType)
    {
        if (IsInBounds(x, y))
        {
            // set type
            tiles[x][y].type = tileType;
            if (tileType == TileType.Default)
            {
                tiles[x][y].bounds = null;
            }
            else
            {
                tiles[x][y].bounds = new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
            }

            // create gameobject tile
            GameObject template = GetTemplate(tileType);
            GameObject newGo = (GameObject)GameObject.Instantiate(template);
            newGo.transform.position = GetTileLocation(x, y);
            newGo.transform.parent = this.transform;
            newGo.name = string.Format("({0},{1}) {2}", x, y, tileType.ToString());

            // clean up
            if (tiles[x][y] != null)
            {
                GameObject.Destroy(tiles[x][y].gameObject);
            }

            // set 
            tiles[x][y].gameObject = newGo;
        }
    }

    private Vector3 GetTileLocation(int x, int y)
    {
        return origin + new Vector3(x * tileWidth, 0, y * tileHeight);
    }


    private GameObject GetTemplate(TileType tileType)
    {
        if (tileType == TileType.Bin)
        {
            return goBin;
        }
        else if (tileType == TileType.BinSeed)
        {
            return goBinSeed;
        }
        else if (tileType == TileType.BinSprout)
        {
            return goBinSprout;
        }
        else if (tileType == TileType.BinFullGrown)
        {
            return goBinFullGrown;
        }
        else if (tileType == TileType.Desk)
        {
            return goDesk;
        }
        
        return goFloor;
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < gridWidth && y < gridHeight;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1);
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                if (tiles[i][j].bounds.HasValue)
                {
                    Rect bounds = tiles[i][j].bounds.Value;

                    Vector3 center = new Vector3(bounds.x, origin.y, bounds.y);
                    Vector3 size = new Vector3(bounds.width, 1, bounds.height);

                    Gizmos.DrawWireCube(center, size);
                }
            }
        }
    }
}
