using Base;
using Label;
using UnityEngine;
using Zenject;

namespace Bread
{
    public enum Type
    {
        Cream,
        Redbeans
    }

    public abstract class BreadBase : Acceptable
    {
        public override void OnDropped(Draggable draggable)
        {
            if (draggable is LabelBase)
            {
                var labelType = draggable is CreamLabel ? Label.Type.Cream : Label.Type.Redbeans;
                var breadType = this is CreamBread ? Type.Cream : Type.Redbeans;
                _manager.CreateNewLabeledBread(labelType, breadType, transform.position);
                _manager.CreateNewLabel(labelType);
                base.OnDropped(draggable);
                DestroyImmediate(gameObject);
            }
        }

        public class Factory : PlaceholderFactory<BreadBase>
        {
        }
    }


    public class CustomBreadFactory : IFactory<BreadBase>
    {
        private readonly CreamBread.Factory _creamBreadFactory;
        private readonly RedBeansBread.Factory _redbeansBreadFactory;

        CustomBreadFactory(
            CreamBread.Factory creamBreadFactory,
            RedBeansBread.Factory redbeansBreadFactory
        )
        {
            _creamBreadFactory = creamBreadFactory;
            _redbeansBreadFactory = redbeansBreadFactory;
        }

        public BreadBase Create()
        {
            var rand = Random.Range(0, 2) == 0;
            return rand ? (BreadBase) _creamBreadFactory.Create() : _redbeansBreadFactory.Create();
        }
    }
}