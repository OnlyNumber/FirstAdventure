using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastCasino : MonoBehaviour
{
    public const float Ymin = -300;
    public const float Ymax = 500;
    public const float DistanceBetweenSlots = 100;

    public const float SpinningSpeed = 3600;
    public const float SmoothSpeed = 400;

    public float CurrentSpeed = 3600;

    public List<RectTransform> Spins = new();

    public float randomResult = 0;

    public bool IsStopped = false;

    private void Update()
    {
        if (IsStopped)
            return;

        foreach (var item in Spins)
        {
            if (item.transform.localPosition.y <= Ymin)
            {
                float yPos = item.transform.localPosition.y - Ymin;

                item.transform.localPosition = Vector3.up * (Ymax + yPos); 
            }

            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y - CurrentSpeed * Time.deltaTime, 0);
        }
    }

    public void GoAgain()
    {
        CurrentSpeed = SpinningSpeed;

        IsStopped = false;
    }

    [ContextMenu("GetRandomResultSharp")]
    public void GetRandomResultSharp()
    {
        randomResult = Random.Range(0, 4);

        Spins[0].transform.localPosition = Vector3.up * DistanceBetweenSlots * randomResult;

        if (randomResult == 0)
            Spins[1].transform.localPosition = Vector3.up * (Ymax - DistanceBetweenSlots);
        else
            Spins[1].transform.localPosition = Vector3.up * (Ymin + DistanceBetweenSlots * (randomResult - 1) );


        IsStopped = true;
            
    }

    [ContextMenu("GetRandomResultSmooth")]
    public void GetRandomResultSmooth()
    {
        randomResult = Random.Range(0, 4);

        CurrentSpeed = SmoothSpeed;
    }

    public IEnumerator SmoothStop()
    {
        yield return new WaitForSeconds(0);
    }

}
