using System.Collections.Generic;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Snake
{
    public class Snake : MonoBehaviour, IListener<PlayerDiedMessage>
    {
        public List<Sprite> SnakeMoving;
        public List<Sprite> SnakeEating;
        public int AnimationSpeed = 4;

        private List<Sprite> _currentAnimation; 
        private int _currentAnimationSpeed;
        private int _currentFrame;
        private bool _hasAttackedPlayer;

        void Start()
        {
            this.Register<PlayerDiedMessage>();
            _currentAnimation = SnakeMoving;
        }

        void OnDestroy()
        {
            this.UnRegister<PlayerDiedMessage>();
        }

        private void Update()
        {
            _currentAnimationSpeed--;
            if (_currentAnimationSpeed <= 0)
            {
                _currentAnimationSpeed = AnimationSpeed;
            }

            _currentFrame++;
            if (_currentFrame >= _currentAnimation.Count)
            {
                _currentFrame = 0;
            }

            GetComponentInChildren<SpriteRenderer>().sprite = _currentAnimation[_currentFrame];

            transform.Translate(-2*Time.deltaTime, 0, 0);

            if (transform.position.x <= -2)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name == "Player" && !_hasAttackedPlayer)
            {
                EventAggregator.SendMessage(new PlayerTakeDamageMessage());
                _hasAttackedPlayer = true;
                _currentAnimation = SnakeEating;
            }
        }

        public void Handle(PlayerDiedMessage message)
        {
            _hasAttackedPlayer = true;
        }
    }
}