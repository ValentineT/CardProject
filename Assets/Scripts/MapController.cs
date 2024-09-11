using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPoints;

    private int _currentIndex = 0;

    public void ActivateCheckPoint()
    {
        checkPoints[_currentIndex].SetActive(true);
        if (_currentIndex < checkPoints.Count)
        {
            CheckPoint checkPoint = checkPoints[_currentIndex].GetComponent<CheckPoint>();
            checkPoint.Index = _currentIndex;
            _currentIndex++;
        }
    }

    public void DeactivateCheckPoint(int index)
    {
        for (int i = index; i < checkPoints.Count; i++)
        {
            checkPoints[i].SetActive(false);
        }

        _currentIndex = index;
    }
}
