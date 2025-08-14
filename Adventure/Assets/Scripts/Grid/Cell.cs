using UnityEngine;

public class Cell : MonoBehaviour
{
    public PathfindingCell PathfindingCell = new();

    public Vector2 CellSize = new Vector2(2, 2);

    public Transform CharacterPoint;

    public Vector2Int Position;

    public GridObject CellGridObject;

    public MeshRenderer meshRenderer;

    #region ChangeMaterial
    public Material Default;
    public Material Obstacle;
    public Material Path;
    public Material AllPath;
    #endregion

    [ContextMenu("MakeCellObstacle")]
    public void MakeCellObstacle()
    {
        meshRenderer.material = Obstacle;
        PathfindingCell.IsObstacle = true;

        PathfindingCell.SetTextActivity(false);
    }

    public void MakeCellPath()
    {
        meshRenderer.material = Path;
    }

    public void MakeAllCellPath()
    {
        meshRenderer.material = AllPath;
    }

    public void ClearTexts()
    {
        if(!PathfindingCell.IsObstacle)
        meshRenderer.material = Default;

        PathfindingCell.SetTextActivity(false);

    }
}
