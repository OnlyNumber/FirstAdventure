using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Grid
{
    public abstract Cell GetCell(int x, int y);

    public abstract Cell GetCell(Vector2Int position);

    public abstract void Clear();
}
