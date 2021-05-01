using Base;
using LabeledBread;

namespace Box
{
    public class DustBox : Acceptable
    {
        public override void OnDropped(Draggable draggable)
        {
            if (draggable is LabeledBreadBase)
            {
                base.OnDropped(draggable);
                _gameManager.AddScrapScore();
                _breadFactoryManager.CreateNewBread();
            }
        }
    }
}