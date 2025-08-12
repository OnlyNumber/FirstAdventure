using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathfindingSearcher
{
    public const int Forward_Value = 10;

    public const int Diagonal_Value = 14;

    public Grid gridController;

    public List<Cell> SettedCells = new();
    public List<Cell> AlreadyChecked = new();

    public int ForNoEternity = 150;

    public List<Cell> GetPath(Grid grid, Vector2Int startPosition, Vector2Int endPosition)
    {
        if (grid.GetCell(startPosition) == null || grid.GetCell(endPosition) == null ||
            startPosition == endPosition || grid.GetCell(endPosition).PathfindingCell.IsObstacle)
            return null;

        gridController = grid;

        int counter = 0;

        ///InitializeFirstCell
        SettedCells.Clear();
        AlreadyChecked.Clear();

        Cell currentPathCell = gridController.GetCell(startPosition);
        int left = GetValueOfShorterPath(currentPathCell.Position, endPosition);
        currentPathCell.PathfindingCell.SetValues(0, left, left);
        SettedCells.Add(currentPathCell);
        AlreadyChecked.Add(currentPathCell);
        ///

        while (currentPathCell.Position != endPosition)
        {
            var cellsAround = GetCellsAround(currentPathCell.Position);

            foreach (var item in AlreadyChecked)
            {
                if (cellsAround.Contains(item))
                    cellsAround.Remove(item);
            }

            RemoveAllObstacles(ref cellsAround);

            if (AlreadyChecked.Count == SettedCells.Count && cellsAround.Count == 0)
            {
                return null;
            }


            foreach (var cell in cellsAround)
            {
                int passedValue = GetValueOfShorterPath(currentPathCell.Position, cell.Position) + currentPathCell.PathfindingCell.PassedPathValue;

                int leftValue = GetValueOfShorterPath(cell.Position, endPosition);

                if (SettedCells.Contains(cell) && (passedValue + leftValue) > cell.PathfindingCell.AllPathValue)
                    continue;

                cell.PathfindingCell.SetValues(passedValue, passedValue + leftValue, leftValue);

                cell.PathfindingCell.LastCell = currentPathCell.PathfindingCell;

                cell.PathfindingCell.lastCellCheck = currentPathCell;

                SettedCells.Add(cell);
            }

            currentPathCell = FindSmallestAllValue();

            counter++;
            if (counter >= ForNoEternity)
            {
                Debug.Log("Eternity");
                break;
            }

        }

        List<Cell> path = new();
        path.Add(currentPathCell);
        currentPathCell.MakeCellPath();
        do
        {
            currentPathCell = currentPathCell.PathfindingCell.lastCellCheck;
            currentPathCell.MakeCellPath();
            path.Add(currentPathCell);
        }
        while (currentPathCell.Position != startPosition);

        path.Reverse();

        //grid.Clear();

        return path;

    }

    public IEnumerator GetPathFuck(Grid grid, Vector2Int startPosition, Vector2Int endPosition)
    {
        /*if (grid.GetCell(startPosition) == null || grid.GetCell(endPosition) == null ||
            startPosition == endPosition || grid.GetCell(endPosition).PathfindingCell.IsObstacle)
            yield return null;
          */  
            //return null;

        gridController = grid;

        int counter = 0;

        ///InitializeFirstCell
        SettedCells.Clear();
        AlreadyChecked.Clear();

        Cell currentPathCell = gridController.GetCell(startPosition);
        int left = GetValueOfShorterPath(currentPathCell.Position, endPosition);
        currentPathCell.PathfindingCell.SetValues(0, left, left);
        SettedCells.Add(currentPathCell);
        AlreadyChecked.Add(currentPathCell);
        ///

        while (currentPathCell.Position != endPosition)
        {
            var cellsAround = GetCellsAround(currentPathCell.Position);

            foreach (var item in AlreadyChecked)
            {
                if (cellsAround.Contains(item))
                    cellsAround.Remove(item);
            }

            RemoveAllObstacles(ref cellsAround);

            if (AlreadyChecked.Count == SettedCells.Count && cellsAround.Count == 0)
            {
                //return null;
            }


            foreach (var cell in cellsAround)
            {
                int passedValue = GetValueOfShorterPath(currentPathCell.Position, cell.Position) + currentPathCell.PathfindingCell.PassedPathValue;

                int leftValue = GetValueOfShorterPath(cell.Position, endPosition);

                if (SettedCells.Contains(cell) && (passedValue + leftValue) > cell.PathfindingCell.AllPathValue)
                    continue;

                cell.PathfindingCell.SetValues(passedValue, passedValue + leftValue, leftValue);

                cell.PathfindingCell.LastCell = currentPathCell.PathfindingCell;

                cell.PathfindingCell.lastCellCheck = currentPathCell;

                SettedCells.Add(cell);
            }

            currentPathCell = FindSmallestAllValue();

            counter++;
            if (counter >= ForNoEternity)
            {
                Debug.Log("Eternity");
                break;
            }

            yield return new WaitForSeconds(1);
        }

        List<Cell> path = new();
        path.Add(currentPathCell);
        currentPathCell.MakeCellPath();
        do
        {
            currentPathCell = currentPathCell.PathfindingCell.lastCellCheck;
            currentPathCell.MakeCellPath();
            path.Add(currentPathCell);
        }
        while (currentPathCell.Position != startPosition);

        path.Reverse();

        //grid.Clear();

        

    }



    private void RemoveAllObstacles(ref List<Cell> cells)
    {
        List<Cell> obstacles = new();

        foreach (var item in cells)
        {
            if (item.PathfindingCell.IsObstacle)
                obstacles.Add(item);
        }

        foreach (var item in obstacles)
        {
            cells.Remove(item);
        }

    }

    private Cell FindSmallestAllValue()
    {
        Cell min = SettedCells[0];

        foreach (var item in SettedCells)
        {
            if (!AlreadyChecked.Contains(item))
            {
                min = item;
                break;
            }
        }

        foreach (var item in SettedCells)
        {
            if (item.PathfindingCell.AllPathValue < min.PathfindingCell.AllPathValue && !AlreadyChecked.Contains(item))
            {
                min = item;
            }
        }

        if (!AlreadyChecked.Contains(min))
            AlreadyChecked.Add(min);

        return min;
    }

    private int GetValueOfShorterPath(Vector2Int startPosition, Vector2Int endPosition)
    {
        int shorterValue = 0;

        Vector2Int distance = endPosition - startPosition;

        if (distance.x < 0)
            distance.x *= -1;


        if (distance.y < 0)
            distance.y *= -1;

        Vector2Int checkDistance;


        while (distance.x > 0 || distance.y > 0)
        {
            /*checkDistance = distance - Vector2Int.one;

            if (checkDistance.x >= 0 && checkDistance.y >= 0)
            {
                shorterValue += 14;
                distance = checkDistance;

                continue;
            }*/

            checkDistance = distance;

            checkDistance.x -= 1;

            if (checkDistance.x >= 0)
            {
                shorterValue += 10;
                distance = checkDistance;

                continue;

            }

            checkDistance = distance;

            checkDistance.y -= 1;

            if (checkDistance.y >= 0)
            {
                shorterValue += 10;
                distance = checkDistance;

                continue;
            }
        }

        return shorterValue;
    }

    private float GetPassedValueOfNeighbour(Vector2Int startPosition, Vector2Int secondPosition)
    {
        Vector2Int substract = startPosition - secondPosition;

        if (Mathf.Abs(substract.x) + Mathf.Abs(substract.y) == 1)
        {
            return Forward_Value;
        }
        else if (Mathf.Abs(substract.x) + Mathf.Abs(substract.y) == 2)
        {
            return Diagonal_Value;
        }
        else
            return 0;


    }

    private List<Cell> GetCellsAround(Vector2Int position)
    {
        List<Cell> cellsAround = new();

        Cell cellAround;

        //GetGridCell(position.x - 1, position.y - 1);
        GetGridCell(position.x - 1, position.y);
        // GetGridCell(position.x - 1, position.y + 1);
        GetGridCell(position.x, position.y + 1);
        //GetGridCell(position.x + 1, position.y + 1);
        GetGridCell(position.x + 1, position.y);
        //GetGridCell(position.x + 1, position.y - 1);
        GetGridCell(position.x, position.y - 1);


        void GetGridCell(int x, int y)
        {
            cellAround = gridController.GetCell(x, y);

            if (cellAround != null)
                cellsAround.Add(cellAround);
        }

        return cellsAround;
    }
}
