using UnityEngine;
using Zenject;

public class BreadFactoryStarter : MonoBehaviour
{
    private BreadFactoryManager _manager;

    [Inject]
    private void Construct(BreadFactoryManager manager)
    {
        _manager = manager;
    }
    
    private void Start()
    {
        _manager.CreateNewLabel();
        _manager.CreateNewBread();
    }
}