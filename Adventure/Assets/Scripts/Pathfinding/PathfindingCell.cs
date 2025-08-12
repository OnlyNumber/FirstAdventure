using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PathfindingCell
{
    public TMP_Text PassedPass;

    public TMP_Text AllPass;

    public TMP_Text LeftPass;

    public int PassedPathValue;

    public int AllPathValue;

    public int LeftPathValue;

    [field: SerializeField] public PathfindingCell LastCell;

    public Cell lastCellCheck;

    public bool IsObstacle;

    public void SetValues(int passed, int All, int left)
    {
        PassedPathValue = passed;
        PassedPass.text = passed.ToString();

        AllPathValue = All;
        AllPass.text = All.ToString();

        LeftPathValue = left;
        LeftPass.text = left.ToString();

        SetTextActivity(true);

    }
    
    public void SetTextActivity(bool textState)
    {

        PassedPass.gameObject.SetActive(textState);
        AllPass.gameObject.SetActive(textState);
        LeftPass.gameObject.SetActive(textState);
    }
}
