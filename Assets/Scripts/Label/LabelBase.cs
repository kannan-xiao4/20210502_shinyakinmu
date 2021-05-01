using UnityEngine;
using Zenject;

namespace Label
{
    public enum Type
    {
        Cream,
        Redbeans
    }

    public abstract class LabelBase : Draggable
    {
        public class Factory : CustomLabelFactory
        {
            public Factory(CreamLabel.Factory creamLabelFactory, RedBeansLabel.Factory redbeansLabelFactory) : base(
                creamLabelFactory, redbeansLabelFactory)
            {
            }
        }
    }

    public class CustomLabelFactory : IFactory<Type, LabelBase>
    {
        private readonly Vector3 creamInitialPosition = new Vector3(-2, 3, 0);
        private readonly Vector3 redbeansInitialPosition = new Vector3(2, 3, 0);
        private readonly CreamLabel.Factory _creamLabelFactory;
        private readonly RedBeansLabel.Factory _redbeansLabelFactory;

        protected CustomLabelFactory(
            CreamLabel.Factory creamLabelFactory,
            RedBeansLabel.Factory redbeansLabelFactory
        )
        {
            _creamLabelFactory = creamLabelFactory;
            _redbeansLabelFactory = redbeansLabelFactory;
        }

        public LabelBase Create(Type type)
        {
            LabelBase labelBase;
            if (type == Type.Cream)
            {
                labelBase = _creamLabelFactory.Create();
                labelBase.transform.position = creamInitialPosition;
            }
            else
            {
                labelBase = _redbeansLabelFactory.Create();
                labelBase.transform.position = redbeansInitialPosition;
            }

            return labelBase;
        }
    }
}