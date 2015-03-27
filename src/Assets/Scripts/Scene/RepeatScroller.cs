using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Scene
{
    public class RepeatScroller : MonoBehaviour, IListener<PauseRunningMessage>, IListener<ResumeRunningMessage>
    {
        public float Length;
        public float Speed;

        private bool _isPaused = true;

        void Start()
        {
            this.Register<PauseRunningMessage>();
            this.Register<ResumeRunningMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<PauseRunningMessage>();
            this.UnRegister<ResumeRunningMessage>();
        }

        void Update()
        {
            if (_isPaused) return;

            transform.Translate(-Speed * Time.deltaTime, 0, 0);
            if (transform.position.x <= -Length)
            {
                transform.Translate(Length, 0, 0);
            }
        }

        public void Handle(PauseRunningMessage message)
        {
            _isPaused = true;
        }

        public void Handle(ResumeRunningMessage message)
        {
            _isPaused = false;
        }
    }
}