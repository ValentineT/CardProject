using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private HPController hPController;
    [SerializeField] private List<GameObject> checkPoints;

    public void ActivateCheckPoint(int index)
    {
        if(index == 0 || index >= checkPoints.Count) return;
        checkPoints[index].SetActive(true);
    }
}
