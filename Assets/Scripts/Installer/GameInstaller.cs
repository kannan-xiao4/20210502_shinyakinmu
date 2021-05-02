using Bread;
using Label;
using LabeledBread;
using Manager;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject creamLabelPrefab;
    [SerializeField] private GameObject redbeansLabelPrefab;
    [SerializeField] private GameObject creamBreadPrefab;
    [SerializeField] private GameObject redBeansBreadPrefab;
    [SerializeField] private GameObject creamLabeledPrefab;
    [SerializeField] private GameObject redbeansLabeledPrefab;
    [SerializeField] private GameObject creamLabeledRedbeansBreadPrefab;
    [SerializeField] private GameObject redbeansLabeledCreamBreadPrefab;

    [SerializeField] private GameObject soundManagerPrefab;

    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<SoundManager>().FromComponentInNewPrefab(soundManagerPrefab).AsSingle().NonLazy();

        Container
            .Bind(typeof(BreadFactoryManager), typeof(IInitializable))
            .To<BreadFactoryManager>()
            .AsSingle();

        // label install
        Container.BindFactory<CreamLabel, CreamLabel.Factory>().FromComponentInNewPrefab(creamLabelPrefab);
        Container.BindFactory<RedBeansLabel, RedBeansLabel.Factory>().FromComponentInNewPrefab(redbeansLabelPrefab);
        Container.Bind<LabelBase.Factory>().AsSingle();

        // bread install
        Container.BindFactory<CreamBread, CreamBread.Factory>().FromComponentInNewPrefab(creamBreadPrefab);
        Container.BindFactory<RedBeansBread, RedBeansBread.Factory>().FromComponentInNewPrefab(redBeansBreadPrefab);
        Container.BindFactory<BreadBase, BreadBase.Factory>().FromFactory<CustomBreadFactory>();

        // labeled bread install
        Container.BindFactory<CreamLabeledCreamBread, CreamLabeledCreamBread.Factory>()
            .FromComponentInNewPrefab(creamLabeledPrefab);
        Container.BindFactory<RedBeansLabeledRedBeansBread, RedBeansLabeledRedBeansBread.Factory>()
            .FromComponentInNewPrefab(redbeansLabeledPrefab);
        Container.BindFactory<CreamLabeledRedBeansBread, CreamLabeledRedBeansBread.Factory>()
            .FromComponentInNewPrefab(creamLabeledRedbeansBreadPrefab);
        Container.BindFactory<RedBeansLabeledCreamBread, RedBeansLabeledCreamBread.Factory>()
            .FromComponentInNewPrefab(redbeansLabeledCreamBreadPrefab);
        Container.Bind<LabeledBreadBase.Factory>().AsSingle();
    }
}