using UnityEngine;

public class Bread : Acceptable
{
    [SerializeField] private GameObject prefab;
    
    public override void OnDropped(Draggable draggable)
    {
        Instantiate(prefab, transform.position, transform.rotation);
        base.OnDropped(draggable);
        DestroyImmediate(gameObject);
    }
}
