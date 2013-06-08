using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    /// <summary>
    /// Speed in tiles per second
    /// </summary>
    public float speed = 5;

    public Rect bounds = new Rect(0, 0, 10, 10);

    public static Player Instance { get; private set; }

    void Awake()
    {
        Instance = this;
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

        if (movement != Vector3.zero)
        {
            TryMove(movement);
        }
    }


    void TryMove(Vector3 amountToMove)
    {
        // where I want to be
        Vector3 goToHere = this.transform.localPosition + amountToMove;

        // find what tile I'm standing on
        Vector2 myCoordinate = Map.Instance.GetClosestCoordinate(goToHere);

        // check in a box around the player
        int xMin = Mathf.RoundToInt(myCoordinate.x - 1);
        int xMax = Mathf.RoundToInt(myCoordinate.x + 1);
        int yMin = Mathf.RoundToInt(myCoordinate.y - 1);
        int yMax = Mathf.RoundToInt(myCoordinate.y + 1);

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

}
