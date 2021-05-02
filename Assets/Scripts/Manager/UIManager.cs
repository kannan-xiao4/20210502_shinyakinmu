using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject title;
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject result;

        // title object
        [SerializeField] private Button titleButton;

        // inGame object
        [SerializeField] private Text creamBreadScore;
        [SerializeField] private Text redBeansBreadScore;
        [SerializeField] private Text scrapScore;
        [SerializeField] private Text failedScore;
        [SerializeField] private Button exitButton;
        [SerializeField] private Animator startAnimator;

        // result object
        [SerializeField] private Text creamBreadResultScore;
        [SerializeField] private Text redBeansBreadResultScore;
        [SerializeField] private Text scrapResultScore;
        [SerializeField] private Text failedResultScore;
        [SerializeField] private Text wageResultScore;
        [SerializeField] private Button reportButton;
        [SerializeField] private Button returnTitleButton;


        private GameManager _manager;
        private SoundManager _soundManager;

        [Inject]
        private void Construct(GameManager manager, SoundManager soundManager)
        {
            _manager = manager;
            _soundManager = soundManager;
        }

        private void Start()
        {
            titleButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    startAnimator.gameObject.SetActive(true);
                    startAnimator.Play("StartAnimation");
                    title.SetActive(false);
                }).AddTo(this);


            InitInGame();
            InitResult();
        }

        private void InitInGame()
        {
            _manager.CreamBreadScore.SubscribeToText(creamBreadScore).AddTo(this);
            _manager.RedBeansBreadScore.SubscribeToText(redBeansBreadScore).AddTo(this);
            _manager.ScrapScore.SubscribeToText(scrapScore).AddTo(this);
            _manager.FailedScore.SubscribeToText(failedScore).AddTo(this);
            exitButton.OnClickAsObservable().Subscribe(_ =>
            {
                _manager.EndGame();
                result.SetActive(true);
            }).AddTo(this);

            var stateMachineTrigger = startAnimator.GetBehaviour<ObservableStateMachineTrigger>();

            stateMachineTrigger.OnStateEnterAsObservable()
                .Subscribe(_ => _soundManager.PlayBGM(BGM.Start))
                .AddTo(this);
            stateMachineTrigger.OnStateExitAsObservable()
                .Subscribe(_ =>
                {
                    _manager.StartGame();
                    _soundManager.PlayBGM(BGM.Others);
                    startAnimator.gameObject.SetActive(false);
                }).AddTo(this);
        }

        private void InitResult()
        {
            _manager.CreamBreadScore.SubscribeToText(creamBreadResultScore).AddTo(this);
            _manager.RedBeansBreadScore.SubscribeToText(redBeansBreadResultScore).AddTo(this);
            _manager.ScrapScore.SubscribeToText(scrapResultScore).AddTo(this);
            _manager.FailedScore.SubscribeToText(failedResultScore).AddTo(this);
            _manager.WageScore.Select(wage => $"{wage}円").SubscribeToText(wageResultScore).AddTo(this);
            reportButton.OnClickAsObservable().Subscribe(_ => _manager.Report()).AddTo(this);
            returnTitleButton.OnClickAsObservable().Subscribe(_ =>
            {
                _manager.ResetGame();
                title.SetActive(true);
                result.SetActive(false);
            }).AddTo(this);
        }
    }
}