using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public const int GRID_WIDTH = 20;
    public const int GRID_HIGHT = 10;

    public Cell CellPrefab;

    public List<Cell> AllCells = new();

    public PathfindingSearcher pathfindingSearcher = new();

    public Vector2 StartPoint;

    public Vector2 SearchPoint;

    [ContextMenu("Create grid")]
    public void CreateGrid()
    {
        float initialHight = GRID_HIGHT * CellPrefab.CellSize.y;

        Cell cell;

        for (int y = 0; y < GRID_HIGHT; y++)
        {
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                cell = PrefabUtility.InstantiatePrefab(CellPrefab) as Cell;

                cell.transform.parent = transform;

                cell.transform.localPosition = new Vector3(x * CellPrefab.CellSize.x, 0, initialHight - y * CellPrefab.CellSize.y);

                cell.name = CellPrefab.name;

                cell.Position = new Vector2(x, y);

                AllCells.Add(cell);
            }
        }
    }

    [ContextMenu("Clear grid")]
    public void ClearGrid()
    {
        foreach (var item in AllCells)
        {
            DestroyImmediate(item.gameObject);
        }

        AllCells.Clear();
    }

    [ContextMenu("Recreate")]
    public void Recreate()
    {
        ClearGrid();

        CreateGrid();
    }

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || y < 0 || x >= GRID_WIDTH || y >= GRID_HIGHT)
            return null;

        return AllCells[x + y * GRID_WIDTH];
    }

    public Cell GetCell(Vector2 position)
    {
        return GetCell((int)position.x, (int)position.y);
    }

    public Vector2 GetPositionOfCell(Cell cell)
    {
        return cell.Position;


    }
    
    [ContextMenu("First Cell Check")]
    public void FirstCellCheck()
    {
        pathfindingSearcher.SetAllValues(StartPoint, SearchPoint);
    }

    [ContextMenu("Clear All Text")]
    public void ClearAllText()
    {
        foreach (var item in AllCells)
        {
            item.ClearTexts();
        }
    }
}
