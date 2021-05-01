using UnityEngine;
using Zenject;

public class Label : Draggable
{
    [SerializeField] private Type _type;
    
    public enum Type
    {
        Cream,
        Redbeans
    }

    public class Factory : PlaceholderFactory<Label>
    {
    }
}