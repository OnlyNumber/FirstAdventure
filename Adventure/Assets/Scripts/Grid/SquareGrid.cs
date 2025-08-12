using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SquareGrid : MonoBehaviour, Grid
{
    public const int GRID_WIDTH = 20;
    public const int GRID_HIGHT = 10;

    public Cell CellPrefab;

    public List<Cell> AllCells = new();

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

                cell.Position = new Vector2Int(x, y);

                AllCells.Add(cell);
            }
        }
    }

    [ContextMenu("Delete grid")]
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

    public Cell GetCell(Vector2Int position) => GetCell(position.x, position.y);


    public Vector2Int GetPositionOfCell(Cell cell) => cell.Position;

    [ContextMenu("Clear All Text")]
    public void ClearAllText()
    {
        foreach (var item in AllCells)
        {
            item.ClearTexts();
        }
    }
    
    public void Clear()
    {
        foreach (var item in AllCells)
        {
            item.PathfindingCell.SetValues(0, 0, 0);

            item.PathfindingCell.LastCell = null;

            item.PathfindingCell.lastCellCheck = null;
        }
    }
}
