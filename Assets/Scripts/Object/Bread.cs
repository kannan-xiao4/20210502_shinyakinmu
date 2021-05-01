using UnityEngine;
using Zenject;

public class Bread : Acceptable
{
    [SerializeField] private Type _type;
    
    public enum Type
    {
        Cream,
        Redbeans
    }

    public override void OnDropped(Draggable draggable)
    {
        if (draggable is Label)
        {
            _manager.CreateNewLabeledBread(transform.position);
            _manager.CreateNewLabel();
            base.OnDropped(draggable);
            DestroyImmediate(gameObject);
        }
    }

    public class Factory : PlaceholderFactory<Bread>
    {
    }
}