public class DustBox : Acceptable
{
    public override void OnDropped(Draggable draggable)
    {
        if (draggable is LabeledBread)
        {
            base.OnDropped(draggable);
            _manager.CreateNewBread();
        }
    }
}