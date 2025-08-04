using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastCasino : MonoBehaviour
{
    public float speed = 10;

    public List<RectTransform> Spins = new();

    public const float Ymin = -300;
    public const float Ymax = 500;

    private void Update()
    {
        foreach (var item in Spins)
        {
            if (item.transform.localPosition.y <= Ymin)
            {
                float yPos = item.transform.localPosition.y - Ymin;

                item.transform.localPosition = Vector3.up * (Ymax + yPos); 
            }

            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y - speed * Time.deltaTime, 0);
        }

    }

}
