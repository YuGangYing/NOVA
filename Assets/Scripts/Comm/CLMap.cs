using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLMap : MonoBehaviour
{
    public int Width = 50;
    public int Height = 100;
    public float GridWidth = 10;
    public GameObject StaticColliderRoot;
    private CLMapGrid[,] mGrids;
    private List<CLMapGrid> mOpenList = new List<CLMapGrid>();
    private List<CLMapGrid> mCloseList = new List<CLMapGrid>();
    private CLMapGrid mEndGrid;

    void Awake()
    {
        InitGrids(Width, Height);
    }

    void InitGrids(int width, int height)
    {
        // create grids
        mGrids = new CLMapGrid[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                CLMapGrid newGrid = new CLMapGrid();
                newGrid.X = x;
                newGrid.Y = y;
                newGrid.Center = GridCenter(newGrid);
                mGrids[x, y] = newGrid;
            }
        }
        // init static unwalkable
        RaycastHit hitInfo;
        foreach (CLMapGrid grid in mGrids)
        {
            for (int i = 0; i < StaticColliderRoot.transform.childCount; i++)
            {
                Ray ray = new Ray(grid.Center + new Vector3(0, -2000f, 0), Vector3.up);
                if (StaticColliderRoot.transform.GetChild(i).GetComponent<Collider>().Raycast(ray, out hitInfo, 2100f))
                {
                    grid.Walkable = false;
                }
            }
        }
    }

    public CLMapGrid Grid(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return mGrids[x, y];
        }
        else
        {
            return null;
        }
    }

    public CLMapGrid Grid(Vector3 worldPosition)
    {
        int xIndex = (int)(worldPosition.x / GridWidth);
        int yIndex = (int)(worldPosition.z / GridWidth);
        return Grid(xIndex, yIndex);
    }

    CLMapGrid FindMinFGrid()
    {
        if (mOpenList.Count != 0)
        {
            CLMapGrid result = mOpenList[0];
            foreach (CLMapGrid grid in mOpenList)
            {
                if (grid.F < result.F)
                {
                    result = grid;
                }
            }
            return result;
        }
        else
        {
            return null;
        }
    }

    public Vector3 GridCenter(CLMapGrid grid)
    {
        float offset = GridWidth / 2f;
        return new Vector3(
            grid.X * GridWidth + offset,
            transform.position.y,
            grid.Y * GridWidth + offset);
    }

    void AddNaborGridsToOpenList(CLMapGrid curGrid)
    {
        // left up
        AddNaborGridToOpenList(curGrid.X - 1, curGrid.Y - 1, curGrid);
        // up 
        AddNaborGridToOpenList(curGrid.X, curGrid.Y - 1, curGrid);
        // right up
        AddNaborGridToOpenList(curGrid.X + 1, curGrid.Y - 1, curGrid);
        // left
        AddNaborGridToOpenList(curGrid.X - 1, curGrid.Y, curGrid);
        // right
        AddNaborGridToOpenList(curGrid.X + 1, curGrid.Y, curGrid);
        // left down
        AddNaborGridToOpenList(curGrid.X - 1, curGrid.Y + 1, curGrid);
        // down
        AddNaborGridToOpenList(curGrid.X, curGrid.Y + 1, curGrid);
        // right down
        AddNaborGridToOpenList(curGrid.X + 1, curGrid.Y + 1, curGrid);
    }

    void AddNaborGridToOpenList(int x, int y, CLMapGrid curGrid)
    {
        CLMapGrid naborGrid = Grid(x, y);
        if (naborGrid != null)
        {
            if (!naborGrid.Walkable ||
                naborGrid.State == CLMapGrid.GridState.InCloseList)
            {
                return;
            }
            if (naborGrid.State != CLMapGrid.GridState.InOpenList)
            {
                // left up corner
                if (naborGrid.X == curGrid.X - 1
                    && naborGrid.Y == curGrid.Y + 1)
                {
                    if (!mGrids[naborGrid.X, naborGrid.Y - 1].Walkable ||
                        mGrids[naborGrid.X, naborGrid.Y - 1].State == CLMapGrid.GridState.InCloseList
                        )
                    {
                        return;
                    }
                    if (!mGrids[naborGrid.X + 1, naborGrid.Y].Walkable ||
                        mGrids[naborGrid.X + 1, naborGrid.Y].State == CLMapGrid.GridState.InCloseList
                        )
                    {
                        return;
                    }
                }
                // right up corner
                if (naborGrid.X == curGrid.X + 1
                 && naborGrid.Y == curGrid.Y + 1)
                {
                    if (!mGrids[naborGrid.X - 1, naborGrid.Y].Walkable ||
                        mGrids[naborGrid.X - 1, naborGrid.Y].State == CLMapGrid.GridState.InCloseList
                        )
                    {
                        return;
                    }
                    if (!mGrids[naborGrid.X, naborGrid.Y - 1].Walkable ||
                       mGrids[naborGrid.X, naborGrid.Y - 1].State == CLMapGrid.GridState.InCloseList
                       )
                    {
                        return;
                    }
                }
                // left down corner
                if (naborGrid.X == curGrid.X - 1
                 && naborGrid.Y == curGrid.Y - 1)
                {
                    if (!mGrids[naborGrid.X, naborGrid.Y + 1].Walkable ||
                        mGrids[naborGrid.X, naborGrid.Y + 1].State == CLMapGrid.GridState.InCloseList
                        )
                    {
                        return;
                    }
                    if (!mGrids[naborGrid.X + 1, naborGrid.Y].Walkable ||
                       mGrids[naborGrid.X + 1, naborGrid.Y].State == CLMapGrid.GridState.InCloseList
                       )
                    {
                        return;
                    }
                }
                // right down corner
                if (naborGrid.X == curGrid.X + 1
                 && naborGrid.Y == curGrid.Y - 1)
                {
                    if (!mGrids[naborGrid.X, naborGrid.Y + 1].Walkable ||
                        mGrids[naborGrid.X, naborGrid.Y + 1].State == CLMapGrid.GridState.InCloseList
                        )
                    {
                        return;
                    }
                    if (!mGrids[naborGrid.X - 1, naborGrid.Y].Walkable ||
                       mGrids[naborGrid.X - 1, naborGrid.Y].State == CLMapGrid.GridState.InCloseList
                       )
                    {
                        return;
                    }
                }
                // add new
                if (!mOpenList.Contains(naborGrid))
                {
                    mOpenList.Add(naborGrid);
                    naborGrid.State = CLMapGrid.GridState.InOpenList;
                    naborGrid.Parent = curGrid;
                    naborGrid.CalculateFGH(mEndGrid);
                }

            }
            else
            {
                int betterG = curGrid.G + ((naborGrid.X == curGrid.X || naborGrid.Y == curGrid.Y) ? 10 : 14);
                if (betterG < naborGrid.G)
                {
                    naborGrid.Parent = curGrid;
                    naborGrid.G = betterG;
                    naborGrid.F = naborGrid.H + naborGrid.G;
                }
            }
        }
    }

    public List<CLMapGrid> FindPathIgnoreStartEnd(CLMapGrid start, CLMapGrid end)
    {
        // save start and end walkable, force to be true
        bool startPointWalkable = start.Walkable;
        start.Walkable = true;
        bool endPointWalkable = end.Walkable;
        end.Walkable = true;
        List<CLMapGrid> path = FindPath(start, end);
        // restore start and end walkable
        start.Walkable = startPointWalkable;
        end.Walkable = endPointWalkable;
        return path;
    }

    public List<CLMapGrid> FindPathIgnoreStartEnd(Transform start, Transform end)
    {
        return FindPathIgnoreStartEnd(
              Grid(start.position),
              Grid(end.position)
              );
    }

    public List<CLMapGrid> FindPath(CLMapGrid start, CLMapGrid end)
    {
        // null check
        if (start == null || end == null)
        {
            return null;
        }
        // clear first
        foreach (CLMapGrid grid in mOpenList)
        {
            grid.State = CLMapGrid.GridState.None;
        }
        mOpenList.Clear();
        foreach (CLMapGrid grid in mCloseList)
        {
            grid.State = CLMapGrid.GridState.None;
        }
        mCloseList.Clear();
       // mStartGrid = start;
        mEndGrid = end;
        List<CLMapGrid> path = null;
        // find
        mOpenList.Add(start);
        while (mOpenList.Count != 0)
        {
            CLMapGrid minFGrid = FindMinFGrid();
            mOpenList.Remove(minFGrid);
            mCloseList.Add(minFGrid);
            minFGrid.State = CLMapGrid.GridState.InCloseList;
            AddNaborGridsToOpenList(minFGrid);
            if (mOpenList.Contains(end))
            {
                // find path
                path = new List<CLMapGrid>();
                CLMapGrid curPathNode = end;
                while (true)
                {
                    path.Insert(0, curPathNode);
                    if (curPathNode == start)
                    {
                        break;
                    }
                    curPathNode = curPathNode.Parent;
                }
                break;
            }
        }
        return path;
    }

    public List<CLMapGrid> FindPath(Transform start, Transform end)
    {
        return FindPath(Grid(start.position), Grid(end.position));
    }

    void Update()
    {
       // DebugDraw();
    }

    void DebugDraw()
    {
        foreach (CLMapGrid grid in mGrids)
        {
            Color color;
            if (!grid.Walkable)
            {
                color = Color.black;
            }
            else
            {
                color = Color.red;
            }
            CLDebug.DrawCross(grid.Center, color);
        }
    }

    public static void DebugDrawPath(List<CLMapGrid> path)
    {
        foreach (CLMapGrid grid in path)
        {
            CLDebug.DrawCross(grid.Center, Color.blue);
        }
    }
}

public class CLMapGrid
{
    public enum GridState
    {
        None,
        InCloseList,
        InOpenList
    }
    public int F = 0;
    public int G = 0;
    public int H = 0;
    public int X;
    public int Y;
    public CLMapGrid Parent = null;
    public GridState State = GridState.None;
    public bool Walkable = true;
    public Vector3 Center;
    public void CalculateFGH(CLMapGrid end)
    {
        if (Parent != null)
        {
            G = Parent.G + ((X == Parent.X || Y == Parent.Y) ? 10 : 14);
            H = Mathf.Abs(X - end.X) + Mathf.Abs(Y - end.Y);
            F = G + H;
        }
    }
}

