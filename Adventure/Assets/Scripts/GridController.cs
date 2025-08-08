using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
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
                cell = Instantiate(CellPrefab);

                cell.transform.parent = transform;

                cell.transform.localPosition = new Vector3(x * CellPrefab.CellSize.x, 0, initialHight - y * CellPrefab.CellSize.y);

                cell.name = CellPrefab.name;

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

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || y < 0 || x > GRID_WIDTH || y > GRID_HIGHT)
        {
            Debug.LogError("Wrong cell [" + x + "][" + y + "] returning [0][0] instead"); 
            return AllCells[0];
        }

        return AllCells[x + y * GRID_WIDTH];
    }
}
