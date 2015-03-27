using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Player
{
    public class PlayerDamage : MonoBehaviour, IListener<PlayerTakeDamageMessage>, IListener<PlayerDiedMessage>, IListener<ResumeRunningMessage>
    {
        public int FlashCount = 8;
        private int _remainingFlashes;
        private bool _isFlashing;
        private bool _isDead;

        void Start()
        {
            this.Register<PlayerTakeDamageMessage>();
            this.Register<PlayerDiedMessage>();
            this.Register<ResumeRunningMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<PlayerTakeDamageMessage>();
            this.UnRegister<PlayerDiedMessage>();
            this.UnRegister<ResumeRunningMessage>();
        }

        void Update()
        {
            if (_remainingFlashes > 0)
            {
                _isFlashing = !_isFlashing;
                _remainingFlashes--;
                GetComponent<SpriteRenderer>().color = _isFlashing ? Color.red : Color.white;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        public void Handle(PlayerTakeDamageMessage message)
        {
            if (!_isDead)
            {
                _remainingFlashes = FlashCount;
            }
        }

        public void Handle(PlayerDiedMessage message)
        {
            _isDead = true;
            _remainingFlashes = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        public void Handle(ResumeRunningMessage message)
        {
            _isDead = false;
        }
    }
}