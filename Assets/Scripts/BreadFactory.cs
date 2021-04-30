using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadFactory : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private List<GameObject> breadSource;

    private void Start()
    {
    }
}