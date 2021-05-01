using System;
using Base;
using Zenject;

namespace LabeledBread
{
    public abstract class LabeledBreadBase : Draggable
    {
        public class Factory : CustomLabeledBreadFactory
        {
            protected Factory(
                CreamLabeledCreamBread.Factory creamLabeledCreamBreadFactory,
                RedBeansLabeledRedBeansBread.Factory redbeansLabeledRedbeansBreadFactory)
                : base(creamLabeledCreamBreadFactory, redbeansLabeledRedbeansBreadFactory)
            {
            }
        }
    }

    public class CustomLabeledBreadFactory : IFactory<Label.Type, Bread.Type, LabeledBreadBase>
    {
        private readonly CreamLabeledCreamBread.Factory _creamLabeledCreamBreadFactory;
        private readonly RedBeansLabeledRedBeansBread.Factory _redbeansLabeledRedbeansBreadFactory;

        protected CustomLabeledBreadFactory(
            CreamLabeledCreamBread.Factory creamLabeledCreamBreadFactory,
            RedBeansLabeledRedBeansBread.Factory redbeansLabeledRedbeansBreadFactory
        )
        {
            _creamLabeledCreamBreadFactory = creamLabeledCreamBreadFactory;
            _redbeansLabeledRedbeansBreadFactory = redbeansLabeledRedbeansBreadFactory;
        }

        public LabeledBreadBase Create(Label.Type labelType, Bread.Type breadType)
        {
            if (labelType == Label.Type.Cream && breadType == Bread.Type.Cream)
            {
                return _creamLabeledCreamBreadFactory.Create();
            }

            if (labelType == Label.Type.Cream && breadType == Bread.Type.Redbeans)
            {
                return _redbeansLabeledRedbeansBreadFactory.Create();
            }

            if (labelType == Label.Type.Redbeans && breadType == Bread.Type.Cream)
            {
                return _creamLabeledCreamBreadFactory.Create();
            }

            if (labelType == Label.Type.Redbeans && breadType == Bread.Type.Redbeans)
            {
                return _redbeansLabeledRedbeansBreadFactory.Create();
            }

            throw new NotSupportedException();
        }
    }
}