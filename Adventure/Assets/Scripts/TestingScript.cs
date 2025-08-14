using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public SquareGrid squareGridl;

    public Vector2Int StartPoint;
    public Vector2Int SearchPoint;

    public int PathSearchRadius;

    public Hero HeroPrefab;

    [ContextMenu("Search Path")]
    public void SearchPath()
    {
        PathfindingSystem.PathfindingSearcher.GetPath(squareGridl, StartPoint, SearchPoint);
    }

    [ContextMenu("Get All Paths From Point")]
    public void GetAllPathsFromPoint()
    {
        foreach (var item in PathfindingSystem.BFSMovement(squareGridl, (StartPoint.x, StartPoint.y), PathSearchRadius))
            squareGridl.GetCell(item.Item1,item.Item2).MakeAllCellPath();
    }

    [ContextMenu("Spawn hero")]
    public void SpawnHero()
    {
        
    }
}
