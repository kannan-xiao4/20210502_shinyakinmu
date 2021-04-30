using System.Collections.Generic;
using UnityEngine;

public class BreadFactory : MonoBehaviour
{
    [SerializeField] private Transform laneParent;
    [SerializeField] private List<GameObject> labelSource;
    [SerializeField] private List<GameObject> breadSource;
    [SerializeField] private List<GameObject> labeledBreadSource;
    
    private void Start()
    {
    }
}