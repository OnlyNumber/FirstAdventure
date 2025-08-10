using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public PathfindingCell PathfindingCell;

    public Vector2 CellSize = new Vector2(2, 2);

    public Transform CharacterPoint;

    public Vector2 Position;

    public MeshRenderer meshRenderer;

    public Material Default;

    public Material Obstacle;

    public Material Path;


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

    public void ClearTexts()
    {
        if(meshRenderer.material != Obstacle)
        meshRenderer.material = Default;

        PathfindingCell.SetTextActivity(false);

    }
}
