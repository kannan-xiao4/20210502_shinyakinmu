using Base;
using LabeledBread;
using UnityEngine;

namespace Box
{
    enum BoxType
    {
        Cream,
        Redbeans
    }

    public class DeliveryBox : Acceptable
    {
        [SerializeField] private BoxType _boxType;

        public override void OnDropped(Draggable draggable)
        {
            if (draggable is LabeledBreadBase breadBase)
            {
                EstimateDeliver(breadBase);
                base.OnDropped(draggable);
                _breadFactoryManager.CreateNewBread();
            }
        }

        private void EstimateDeliver(LabeledBreadBase breadBase)
        {
            if (_boxType == BoxType.Cream && breadBase is CreamLabeledCreamBread)
            {
                _gameManager.AddCreamBreadScore();
                return;
            }

            if (_boxType == BoxType.Redbeans && breadBase is RedBeansLabeledRedBeansBread)
            {
                _gameManager.AddRedBeansBreadScore();
                return;
            }

            _gameManager.AddFailedScore();
        }
    }
}