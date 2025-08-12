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

                if (currentPath != null && currentPath.Count <= radius)
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

    public static IEnumerator GetAllPathesFuck(Grid grid, Vector2Int startPosition, int radius)
    {
        List<Cell> getAllCells = new();

        List<Cell> currentPath;

        int yRadius = 0;

        for (int x = -radius; x <= radius; x++)
        {
            yRadius = radius - Mathf.Abs(x);

            for (int y = -yRadius; y <= yRadius; y++)
            {
                if (getAllCells.Contains(grid.GetCell(x, y)))
                    continue;

                currentPath = PathfindingSearcher.GetPath(grid, startPosition, startPosition + new Vector2Int(x, y));

                if (currentPath.Count > radius)
                    Debug.Log("path " + currentPath.Count);

                if (currentPath != null && currentPath.Count <= radius)
                {


                    foreach (var item in currentPath)
                    {
                        if (!getAllCells.Contains(item))
                        {
                            getAllCells.Add(item);
                        }
                    }
                }
            }

            foreach (var item in getAllCells)
            {
                item.MakeAllCellPath();
            }


            yield return new WaitForSeconds(1);
        }


    }
}
