using UnityEngine;
using Zenject;

public abstract class Acceptable : MonoBehaviour
{
    protected BreadFactoryManager _manager;

    [Inject]
    private void Construct(BreadFactoryManager manager)
    {
        _manager = manager;
    }
    
    public virtual void OnDropped(Draggable draggable)
    {
        Destroy(draggable.gameObject);
    }
}