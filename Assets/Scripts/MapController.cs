using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPoints;

    public void ActivateCheckPoint(int index)
    {
        if(index == 0 || index >= checkPoints.Count) return;
        checkPoints[index].SetActive(true);
    }
}
