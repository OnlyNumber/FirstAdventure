using UnityEngine;

public class CellSearcher : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastActivator(Input.mousePosition);
        }
    }

    private void RaycastActivator(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            var hitCell = hit.collider.gameObject.GetComponent<Cell>();
            hitCell.ActivateCell();

        }

    }
}
