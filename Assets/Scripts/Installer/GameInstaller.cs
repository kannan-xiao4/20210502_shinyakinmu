using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<GameObject> labelPrefabs;
    [SerializeField] private List<GameObject> breadPrefabs;
    [SerializeField] private List<GameObject> labeledPrefabs;

    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<BreadFactoryManager>().AsSingle();
        Container.BindFactory<Label, Label.Factory>().FromComponentInNewPrefab(labelPrefabs[0]);
        Container.BindFactory<Bread, Bread.Factory>().FromComponentInNewPrefab(breadPrefabs[0]);
        Container.BindFactory<LabeledBread, LabeledBread.Factory>().FromComponentInNewPrefab(labeledPrefabs[0]);
    }
}