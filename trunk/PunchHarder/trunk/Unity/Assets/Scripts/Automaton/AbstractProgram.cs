using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractProgram 
{
    protected Automaton automaton;
    protected abstract void DoAction();
    protected abstract bool MeetCriteria(SoilBin bin);

    Stack<Vector3> path;
    HashSet<Vector2> checkedTiles;
    Queue<SearchPathNode> checkQueue;

    Vector3? currentTarget;

    bool metHorizontal = false;
    bool metVertical = false;

    float closeEnough = 0.5f;
    float timeSinceIStartedWaiting;

    public AbstractProgram(Automaton owner)
    {
        automaton = owner;
        path = new Stack<Vector3>();
        checkedTiles = new HashSet<Vector2>();
        checkQueue = new Queue<SearchPathNode>();
    }

    public virtual void ProgramUpdate()
    {
        if (currentTarget.HasValue)
        {
            WalkPath();
        }
        else
        {
            Wander();
        }
    }

    class SearchPathNode
    {
        public int x, y;
        public SearchPathNode previous;

        public SearchPathNode(int x, int y, SearchPathNode previous)
        {
            this.x = x;
            this.y = y;
            this.previous = previous;
        }
    }

    void BreadthFirstFindBin()
    {
        path.Clear();
        checkedTiles.Clear();
        checkQueue.Clear();

        // find my position
        int myX, myY;
        Map.Instance.GetClosestCoordinate(automaton.transform.position, out myX, out myY);

        // setup process
        SearchPathNode myPositionNode = new SearchPathNode(myX, myY, null);
        AddAdjacentTilesToQueue(myPositionNode);
        checkedTiles.Add(new Vector2(myX, myY));

        SearchPathNode destination = null;

        while (checkQueue.Count > 0)
        {
            SearchPathNode current = checkQueue.Dequeue();

            // been here before
            if (!checkedTiles.Add(new Vector2(current.x, current.y)))
            {
                continue;
            }

            // is it non-traversable?
            if (Map.Instance.GetTileBounds(current.x, current.y).HasValue)
            {
                continue;
            }

            // see if this location matches our criteria
            bool criteriaMet = false;
            List<SoilBin> adjacentBins = Map.Instance.GetAdjacentBins(current.x, current.y);
            for (int i = 0; i < adjacentBins.Count; i++)
            {
                if(MeetCriteria(adjacentBins[i]))
                {
                    // yes, we're done
                    criteriaMet = true;
                    break;
                }
            }

            // are we finished looking?
            if (criteriaMet)
            {
                destination = current;
                break;
            }

            // keep going - add adjacent tiles to queue
            AddAdjacentTilesToQueue(current);
        }

        if (destination != null)
        {
            //build path to desintation
            while (destination.previous != null)
            {
                path.Push(Map.Instance.TileToWorldCoordinate(destination.x, destination.y));
                destination = destination.previous;
            }

            PopNextLocation();
        }
        else
        {
            // walk around or something
        }
    }

    private void AddAdjacentTilesToQueue(SearchPathNode current)
    {
        if (Map.Instance.IsInBounds(current.x, current.y + 1))
        {
            SearchPathNode up = new SearchPathNode(current.x, current.y + 1, current);
            checkQueue.Enqueue(up);
        }

        if (Map.Instance.IsInBounds(current.x, current.y - 1))
        {
            SearchPathNode down = new SearchPathNode(current.x, current.y - 1, current);
            checkQueue.Enqueue(down);
        }

        if (Map.Instance.IsInBounds(current.x - 1, current.y))
        {
            SearchPathNode left = new SearchPathNode(current.x - 1, current.y, current);
            checkQueue.Enqueue(left);
        }

        if (Map.Instance.IsInBounds(current.x + 1, current.y))
        {
            SearchPathNode right = new SearchPathNode(current.x + 1, current.y, current);
            checkQueue.Enqueue(right);
        }
    }

    void WalkPath()
    {
        Vector3 displacement = currentTarget.Value - automaton.transform.position;

        if (!metVertical)
        {
            if (displacement.z > closeEnough)
            {
                automaton.MoveUp();
            }
            else if (displacement.z < -closeEnough)
            {
                automaton.MoveDown();
            }
            else
            {
                metVertical = true;
            }
        }

        else if (!metHorizontal)
        {
            if (displacement.x > closeEnough)
            {
                automaton.MoveRight();
            }
            else if (displacement.x < -closeEnough)
            {
                automaton.MoveLeft();
            }
            else
            {
                metHorizontal = true;
            }
        }

        else
        {
            currentTarget = null;
            metHorizontal = false;
            metVertical = false;

            if (path.Count > 0)
            {
                // keep going
                PopNextLocation();
            }
            else
            {
                // do action
                DoAction();

                // we'll find another bin once we're done actioning
                timeSinceIStartedWaiting = 0;
            }
        }
    }

    void PopNextLocation()
    {
        currentTarget = path.Pop();
    }

    void Wander()
    {
        float timeToWait = automaton.actionDuration;

        //wait a couple of seconds and look again
        timeSinceIStartedWaiting += Time.deltaTime;
        if (timeSinceIStartedWaiting > timeToWait)
        {
            BreadthFirstFindBin();
            timeSinceIStartedWaiting -= timeToWait;
        }
    }

    bool MovedPast(float current, float delta, float target)
    {
        if (current < target)
        {
            return current + delta >= target;
        }
        else
        {
            return current + delta <= target;
        }
    }
}
