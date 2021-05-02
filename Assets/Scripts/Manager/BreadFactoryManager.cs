using System.Collections.Generic;
using LabeledBread;
using UnityEngine;
using Zenject;

namespace Manager
{
    public class BreadFactoryManager : IInitializable
    {
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

        public BreadFactoryManager(
            SoundManager soundManager,
            Label.LabelBase.Factory labelFactory,
            Bread.BreadBase.Factory breadFactory,
            LabeledBreadBase.Factory labeledBreadFactory
        )
        {
            _soundManager = soundManager;
            _labelFactory = labelFactory;
            _breadFactory = breadFactory;
            _labeledBreadFactory = labeledBreadFactory;
        }

        public void CreateNewLabel(Label.Type type)
        {
            _labelFactory.Create(type);
        }

        public void CreateNewBread()
        {
            var newOne = _breadFactory.Create();
            currentBread.Add(newOne);
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
            _soundManager.PlaySE(SE.Label);
        }

        void IInitializable.Initialize()
        {
            CreateNewLabel(Label.Type.Cream);
            CreateNewLabel(Label.Type.Redbeans);

            for (var i = 0; i < initialPositons.Length; i++)
            {
                CreateNewBread();
            }
        }
    }
}