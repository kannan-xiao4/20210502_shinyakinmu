using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public enum SE
    {
        Label = 0,
        Lane = 1,
        Failed = 2,
        Exit = 3
    }

    public enum BGM
    {
        Title = 0,
        Others = 1,
        Start = 2
    }

    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> seClips;
        [SerializeField] private List<AudioClip> bgmClips;
        [SerializeField] private AudioSource seSource;
        [SerializeField] private AudioSource bgmSource;

        public void PlaySE(SE type)
        {
            seSource.PlayOneShot(seClips[(int) type]);
        }

        public void PlayBGM(BGM type)
        {
            bgmSource.clip = bgmClips[(int) type];
            bgmSource.Play();
        }
    }
}