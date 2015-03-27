using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class MusicManager : MonoBehaviour, IListener<PlayerDiedMessage>, IListener<ResumeRunningMessage>
    {
        public AudioClip Main;
        public AudioClip FuneralMarch;

        private AudioSource _audioSource;

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
                // TODO: Support more than one song
                _audioSource.Play();
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
            _isMusicPaused = false;
            _audioSource.clip = Main;
            _audioSource.Play();
        }
    }
}