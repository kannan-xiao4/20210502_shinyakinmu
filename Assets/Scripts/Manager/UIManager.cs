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
        [SerializeField] private Text creamBreadScore;
        [SerializeField] private Text redBeansBreadScore;
        [SerializeField] private Text scrapScore;
        [SerializeField] private Text failedScore;
        [SerializeField] private Button exitButton;
        [SerializeField] private Animator animator;
        
        private GameManager _manager;

        [Inject]
        private void Construct(GameManager manager)
        {
            _manager = manager;
        }

        private void Start()
        {
            _manager.CreamBreadScore.SubscribeToText(creamBreadScore).AddTo(this);
            _manager.RedBeansBreadScore.SubscribeToText(redBeansBreadScore).AddTo(this);
            _manager.ScrapScore.SubscribeToText(scrapScore).AddTo(this);
            _manager.FailedScore.SubscribeToText(failedScore).AddTo(this);
            exitButton.OnClickAsObservable().Subscribe(_ => _manager.Exit()).AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => Input.anyKeyDown)
                .Take(1)
                .Subscribe(_ => animator.enabled = true)
                .AddTo(this);
        }
    }
}