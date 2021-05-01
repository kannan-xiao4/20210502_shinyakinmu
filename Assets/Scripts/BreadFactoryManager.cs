using UnityEngine;

public class BreadFactoryManager
{
    private readonly Label.Factory _labelFactory;
    private readonly Bread.Factory _breadFactory;
    private readonly LabeledBread.Factory _labeledBreadFactory;

    private Vector3 labelInitialPosition = new Vector3(-2, 3, 0);
    private Vector3 breadInitialPosition = new Vector3(0, 0, 0);

    public BreadFactoryManager(
        Label.Factory labelFactory,
        Bread.Factory breadFactory,
        LabeledBread.Factory labeledBreadFactory
    )
    {
        _labelFactory = labelFactory;
        _breadFactory = breadFactory;
        _labeledBreadFactory = labeledBreadFactory;
    }

    public void CreateNewLabel()
    {
        var newOne = _labelFactory.Create();
        newOne.transform.position = labelInitialPosition;
    }

    public void CreateNewBread()
    {
        var newOne = _breadFactory.Create();
        newOne.transform.position = breadInitialPosition;
    }

    public void CreateNewLabeledBread(Vector3 position)
    {
        var newOne = _labeledBreadFactory.Create();
        newOne.transform.position = position;
    }
}