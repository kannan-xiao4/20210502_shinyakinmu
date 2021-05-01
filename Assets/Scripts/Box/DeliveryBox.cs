using LabeledBread;

public class DeliveryBox : Acceptable
{
    public override void OnDropped(Draggable draggable)
    {
        if (draggable is LabeledBreadBase)
        {
            base.OnDropped(draggable);
            _manager.CreateNewBread();
        }
    }
}