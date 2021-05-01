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
                RedBeansLabeledRedBeansBread.Factory redbeansLabeledRedbeansBreadFactory,
                CreamLabeledRedBeansBread.Factory creamLabeledRedBeansBreadFactory,
                RedBeansLabeledCreamBread.Factory redbeansLabeledCreamBreadFactory)
                : base(creamLabeledCreamBreadFactory, redbeansLabeledRedbeansBreadFactory,
                    creamLabeledRedBeansBreadFactory, redbeansLabeledCreamBreadFactory)
            {
            }
        }
    }

    public class CustomLabeledBreadFactory : IFactory<Label.Type, Bread.Type, LabeledBreadBase>
    {
        private readonly CreamLabeledCreamBread.Factory _creamLabeledCreamBreadFactory;
        private readonly RedBeansLabeledRedBeansBread.Factory _redbeansLabeledRedbeansBreadFactory;
        private readonly CreamLabeledRedBeansBread.Factory _creamLabeledRedBeansBreadFactory;
        private readonly RedBeansLabeledCreamBread.Factory _redbeansLabeledCreamBreadFactory;

        protected CustomLabeledBreadFactory(
            CreamLabeledCreamBread.Factory creamLabeledCreamBreadFactory,
            RedBeansLabeledRedBeansBread.Factory redbeansLabeledRedbeansBreadFactory,
            CreamLabeledRedBeansBread.Factory creamLabeledRedBeansBreadFactory,
            RedBeansLabeledCreamBread.Factory redbeansLabeledCreamBreadFactory
        )
        {
            _creamLabeledCreamBreadFactory = creamLabeledCreamBreadFactory;
            _redbeansLabeledRedbeansBreadFactory = redbeansLabeledRedbeansBreadFactory;
            _creamLabeledRedBeansBreadFactory = creamLabeledRedBeansBreadFactory;
            _redbeansLabeledCreamBreadFactory = redbeansLabeledCreamBreadFactory;
        }

        public LabeledBreadBase Create(Label.Type labelType, Bread.Type breadType)
        {
            if (labelType == Label.Type.Cream && breadType == Bread.Type.Cream)
            {
                return _creamLabeledCreamBreadFactory.Create();
            }

            if (labelType == Label.Type.Cream && breadType == Bread.Type.Redbeans)
            {
                return _creamLabeledRedBeansBreadFactory.Create();
            }

            if (labelType == Label.Type.Redbeans && breadType == Bread.Type.Cream)
            {
                return _redbeansLabeledCreamBreadFactory.Create();
            }

            if (labelType == Label.Type.Redbeans && breadType == Bread.Type.Redbeans)
            {
                return _redbeansLabeledRedbeansBreadFactory.Create();
            }

            throw new NotSupportedException();
        }
    }
}