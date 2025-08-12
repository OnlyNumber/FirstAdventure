using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public SquareGrid squareGridl;

    public Vector2Int StartPoint;

    public Vector2Int SearchPoint;

    public int PathSearchRadius;

    [ContextMenu("Search Path")]
    public void SearchPath()
    {
        PathfindingSystem.PathfindingSearcher.GetPath(squareGridl, StartPoint, SearchPoint);
    }

    [ContextMenu("Search Path Coroutine")]
    public void SearchPathCoroutine()
    {
        StartCoroutine(PathfindingSystem.PathfindingSearcher.GetPathFuck(squareGridl, StartPoint, SearchPoint));
    }


    [ContextMenu("Get All Paths From Point")]
    public void GetAllPathsFromPoint()
    {
        //StartCoroutine(PathfindingSystem.GetAllPathesFuck(squareGridl, StartPoint, PathSearchRadius));

        PathfindingSystem.GetAllPathes(squareGridl, StartPoint, PathSearchRadius);
    }
}
