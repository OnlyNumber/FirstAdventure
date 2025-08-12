using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathfindingSystem
{
    public static PathfindingSearcher PathfindingSearcher = new();

    public static void GetAllPathes(Grid grid, Vector2Int startPosition, int radius)
    {
        List<Cell> getAllCells = new();

        List<Cell> currentPath = new();

        int yRadius = 0;

        for (int x = -radius; x <= radius; x++)
        {
            yRadius = radius - Mathf.Abs(x);

            for (int y = -yRadius; y <= yRadius; y++)
            {
                if (getAllCells.Contains(grid.GetCell(x, y)))
                    continue;

                currentPath.Clear();

                currentPath = PathfindingSearcher.GetPath(grid, startPosition, startPosition + new Vector2Int(x, y));

                if (currentPath != null && currentPath.Count <= radius - 1)
                {
                    foreach (var item in currentPath)
                    {
                        if (!getAllCells.Contains(item))
                        {
                            getAllCells.Add(item);
                        }
                    }
                }
                else
                {
                    currentPath = new();
                }
            }

            foreach (var item in getAllCells)
            {
                item.MakeAllCellPath();
            }

        }


    }

    private static (int dy, int dx)[] directions = new (int, int)[]
    {
        (-1, 0), (1, 0), (0, -1), (0, 1)
    };

    public static HashSet<(int, int)> BFSMovement(Grid grid, (int, int) start, int maxMove)
    {
        var visited = new Dictionary<(int, int), int>();
        var reachable = new HashSet<(int, int)>();

        var queue = new Queue<((int, int) pos, int cost)>();
        queue.Enqueue((start, 0));
        visited[start] = 0;

        while (queue.Count > 0)
        {
            var (pos, cost) = queue.Dequeue();
            reachable.Add(pos);

            foreach (var (dy, dx) in directions)
            {
                int ny = pos.Item1 + dy;
                int nx = pos.Item2 + dx;
                int newCost = cost + 1;


                if (grid.GetCell(ny, nx) != null && grid.GetCell(ny, nx).PathfindingCell.IsObstacle == false && newCost <= maxMove)
                {
                    if (!visited.ContainsKey((ny, nx)) || newCost < visited[(ny, nx)])
                    {
                        visited[(ny, nx)] = newCost;
                        queue.Enqueue(((ny, nx), newCost));
                    }
                }

            }
        }

        return reachable;
    }

}
