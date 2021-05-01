using UnityEngine;

public abstract class Acceptable : MonoBehaviour
{
    public virtual void OnDropped(Draggable draggable)
    {
        Destroy(draggable.gameObject);
    }
}