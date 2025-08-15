using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitSpawner
{
    public static GridObject CreateUnit(Grid grid, int x, int y, UnitConfigure unitConfigure)
    {
        var cell = grid.GetCell(x, y);

        var gridObject = GameObject.Instantiate(unitConfigure.Prefab, Vector3.zero, Quaternion.identity).GetComponent<GridObject>();

        cell.CellGridObject = gridObject;
        gridObject.transform.position = cell.CharacterPoint.position;


        return GameObject.Instantiate(unitConfigure.Prefab, Vector3.zero, Quaternion.identity).GetComponent<GridObject>(); 
        
    }
}
