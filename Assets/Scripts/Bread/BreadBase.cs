using System;
using Base;
using Label;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Bread
{
    public enum Type
    {
        Cream,
        Redbeans
    }

    public abstract class BreadBase : Acceptable
    {
        private bool isReady;
        private bool animationReady;
        private Vector3 startPos;
        private Vector3 targetPos;
        private float startTime;
        private float distance;

        public void Initialize(Vector3 initPosition)
        {
            transform.position = initPosition;
        }

        public void MoveWithAnimation(Vector3 target)
        {
            startPos = transform.position;
            targetPos = target;
            distance = targetPos.x - startPos.x;
            startTime = Time.time;
            animationReady = true;
        }

        public void SetReady()
        {
            isReady = true;
        }

        private void Update()
        {
            if (!animationReady || distance < 0.001f)
            {
                return;
            }

            var distCovered = (Time.time - startTime) * 3f;
            var time = distCovered / distance;
            transform.position = Vector3.Lerp(startPos, targetPos, time);
        }

        public override void OnDropped(Draggable draggable)
        {
            if (!isReady)
            {
                return;
            }

            if (draggable is LabelBase)
            {
                var labelType = draggable is CreamLabel ? Label.Type.Cream : Label.Type.Redbeans;
                var breadType = this is CreamBread ? Type.Cream : Type.Redbeans;
                _breadFactoryManager.CreateNewLabeledBread(labelType, breadType, transform.position);
                _breadFactoryManager.CreateNewLabel(labelType);
                base.OnDropped(draggable);
                _breadFactoryManager.RemoveGameObject(gameObject);
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