using LabeledBread;

public class DustBox : Acceptable
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