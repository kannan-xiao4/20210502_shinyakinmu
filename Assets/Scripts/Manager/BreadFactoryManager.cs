using System;
using System.Collections.Generic;
using LabeledBread;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Manager
{
    public class BreadFactoryManager : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _composite = new CompositeDisposable();
        private readonly GameManager _gameManager;
        private readonly SoundManager _soundManager;
        private readonly Label.LabelBase.Factory _labelFactory;
        private readonly Bread.BreadBase.Factory _breadFactory;
        private readonly LabeledBreadBase.Factory _labeledBreadFactory;

        private static readonly Vector3[] initialPositons =
        {
            new Vector3(-2f, 0f, 0f),
            new Vector3(-5.5f, 0f, 0f),
            new Vector3(-9f, 0f, 0f),
            new Vector3(-12.5f, 0f, 0f)
        };

        private List<Bread.BreadBase> currentBread = new List<Bread.BreadBase>();
        private List<GameObject> currentObject = new List<GameObject>();

        public BreadFactoryManager(
            GameManager gameManager,
            SoundManager soundManager,
            Label.LabelBase.Factory labelFactory,
            Bread.BreadBase.Factory breadFactory,
            LabeledBreadBase.Factory labeledBreadFactory
        )
        {
            _gameManager = gameManager;
            _soundManager = soundManager;
            _labelFactory = labelFactory;
            _breadFactory = breadFactory;
            _labeledBreadFactory = labeledBreadFactory;
        }

        public void CreateNewLabel(Label.Type type)
        {
            var go = _labelFactory.Create(type);
            currentObject.Add(go.gameObject);
        }

        public void CreateNewBread()
        {
            var newOne = _breadFactory.Create();
            currentBread.Add(newOne);
            currentObject.Add(newOne.gameObject);

            if (currentBread.Count > initialPositons.Length)
            {
                currentBread.RemoveAt(0);
            }

            newOne.Initialize(initialPositons[currentBread.Count - 1]);

            if (currentBread.Count == initialPositons.Length)
            {
                _soundManager.PlaySE(SE.Lane);
                for (var i = 0; i < initialPositons.Length; i++)
                {
                    currentBread[i].MoveWithAnimation(initialPositons[i]);
                }

                currentBread[0].SetReady();
            }
        }

        public void CreateNewLabeledBread(Label.Type labelType, Bread.Type breadType, Vector3 position)
        {
            var newOne = _labeledBreadFactory.Create(labelType, breadType);
            newOne.transform.position = position;
            currentObject.Add(newOne.gameObject);
            _soundManager.PlaySE(SE.Label);
        }

        public void RemoveGameObject(GameObject go)
        {
            currentObject.Remove(go);
            Object.DestroyImmediate(go);
        }

        void IInitializable.Initialize()
        {
            _gameManager.GameStart.Subscribe(_ =>
            {
                CreateNewLabel(Label.Type.Cream);
                CreateNewLabel(Label.Type.Redbeans);

                for (var i = 0; i < initialPositons.Length; i++)
                {
                    CreateNewBread();
                }
            }).AddTo(_composite);

            _gameManager.GameEnd.Subscribe(_ =>
            {
                foreach (var go in currentObject)
                {
                    Object.DestroyImmediate(go);
                }

                currentBread.Clear();
                currentObject.Clear();
            }).AddTo(_composite);
        }

        void IDisposable.Dispose()
        {
            _composite?.Dispose();
        }
    }
}