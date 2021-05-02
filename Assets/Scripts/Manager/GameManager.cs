using System;
using System.Runtime.InteropServices;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Manager
{
    public class GameManager
    {
        private readonly SoundManager _soundManager;

        public GameManager(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private ReactiveProperty<int> creamBreadScore = new ReactiveProperty<int>(0);
        private ReactiveProperty<int> redBeansBreadScore = new ReactiveProperty<int>(0);
        private ReactiveProperty<int> scrapScore = new ReactiveProperty<int>(0);
        private ReactiveProperty<int> failedScore = new ReactiveProperty<int>(0);

        public IObservable<int> CreamBreadScore => creamBreadScore;

        public IObservable<int> RedBeansBreadScore => redBeansBreadScore;

        public IObservable<int> ScrapScore => scrapScore;

        public IObservable<int> FailedScore => failedScore;


        private int wageScore = 0;

        public IObservable<int> WageScore
        {
            get
            {
                return creamBreadScore
                    .CombineLatest(redBeansBreadScore, (cream, redbeans) => cream + redbeans)
                    .CombineLatest(FailedScore, (bread, failed) =>
                    {
                        wageScore = bread * 10;
                        wageScore -= failed * 10;

                        var hundredBonusCount = bread / 100;
                        var tenBonusCount = bread / 10 - hundredBonusCount;

                        wageScore += hundredBonusCount * 1000;
                        wageScore += tenBonusCount * 100;

                        return wageScore;
                    });
            }
        }

        public void AddCreamBreadScore()
        {
            creamBreadScore.Value++;
        }

        public void AddRedBeansBreadScore()
        {
            redBeansBreadScore.Value++;
        }

        public void AddScrapScore()
        {
            scrapScore.Value++;
        }

        public void AddFailedScore()
        {
            failedScore.Value++;
            _soundManager.PlaySE(SE.Failed);
        }

        private Subject<Unit> gameStart = new Subject<Unit>();
        private Subject<Unit> gameEnd = new Subject<Unit>();

        public IObservable<Unit> GameStart => gameStart;
        public IObservable<Unit> GameEnd => gameEnd;

        public void StartGame()
        {
            gameStart.OnNext(Unit.Default);
        }

        public void EndGame()
        {
            _soundManager.PlaySE(SE.Exit);
            gameEnd.OnNext(Unit.Default);
        }

        public void ResetGame()
        {
            creamBreadScore.Value = 0;
            redBeansBreadScore.Value = 0;
            scrapScore.Value = 0;
            failedScore.Value = 0;
            wageScore = 0;
        }

        public void Report()
        {
            var tweet = $"夜勤で{wageScore:#,0}円稼ぎました。 #深夜勤務 https://unityroom.com/games/shinyakinmu2";
            TweetUnity.Tweet(tweet);
        }
    }
}

internal static class TweetUnity
{
#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void OpenWindow(string url);
#endif

    public static void Tweet(string tweet)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
#if UNITY_WEBGL
                OpenWindow($"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(tweet)}");
#endif
        }
        else
        {
            Application.OpenURL($"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(tweet)}");
        }
    }
}