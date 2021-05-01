using System.Collections.Generic;
using UnityEngine;

public class BreadFactoryManager : MonoBehaviour
{
    [SerializeField] private Transform laneParent;
    [SerializeField] private List<GameObject> labelSource;
    [SerializeField] private List<GameObject> breadSource;
    [SerializeField] private List<GameObject> labeledBreadSource;

    private const int INITIAL_BREAD_COUNT = 3;

    private void Start()
    {
        // for (var i = 0; i < INITIAL_BREAD_COUNT; i++)
        // {
        //     Instantiate(breadSource[Random.Range(0, 2)], laneParent);
        // }
    }
}