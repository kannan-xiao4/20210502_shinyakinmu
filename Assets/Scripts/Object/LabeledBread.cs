using UnityEngine;
using Zenject;

public class LabeledBread : Draggable
{
    [SerializeField] private Label.Type _labelType;
    [SerializeField] private Bread.Type _breadType;

    public class Factory : PlaceholderFactory<LabeledBread>
    {
    }
}
