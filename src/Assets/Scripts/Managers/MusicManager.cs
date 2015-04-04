using System.Collections.Generic;
using Assets.Scripts.Messages;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class MusicManager : MonoBehaviour, IListener<PlayerDiedMessage>, IListener<ResumeRunningMessage>
    {
        public List<AudioClip> Music;
        public AudioClip FuneralMarch;

        private AudioSource _audioSource;
        private AudioClip _previousSong;

        private bool _isMusicPaused;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            this.Register<PlayerDiedMessage>();
            this.Register<ResumeRunningMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<PlayerDiedMessage>();
            this.UnRegister<ResumeRunningMessage>();
        }

        void Update()
        {
            if (_isMusicPaused) return;

            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = GetRandomSong();
                _audioSource.Play();
            }
        }

        private AudioClip GetRandomSong()
        {
            while (true)
            {
                var newSong = Music.Random();
                if (newSong != _previousSong)
                {
                    _previousSong = newSong;
                    return newSong;
                }
            }
        }

        public void Handle(PlayerDiedMessage message)
        {
            _isMusicPaused = true;
            _audioSource.clip = FuneralMarch;
            _audioSource.Play();
        }

        public void Handle(ResumeRunningMessage message)
        {
            if (_isMusicPaused)
            {
                _isMusicPaused = false;
                _audioSource.clip = GetRandomSong();
                _audioSource.Play();
            }
        }
    }
}