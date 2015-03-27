using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Player
{
    public class PlayerDamage : MonoBehaviour, IListener<PlayerTakeDamageMessage>, IListener<PlayerDiedMessage>
    {
        public int FlashCount = 8;
        private int _remainingFlashes;
        private bool _isFlashing;

        void Start()
        {
            this.Register<PlayerTakeDamageMessage>();
            this.Register<PlayerDiedMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<PlayerTakeDamageMessage>();
            this.UnRegister<PlayerDiedMessage>();
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
            _remainingFlashes = FlashCount;
        }

        public void Handle(PlayerDiedMessage message)
        {
            _remainingFlashes = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}