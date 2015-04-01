using System.Collections.Generic;
using Assets.Scripts.Messages;
using Assets.Scripts.Shared;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Snake
{
    public class Snake : MonoBehaviour, IListener<PlayerDiedMessage>
    {
        public List<Sprite> SnakeMoving;
        public List<Sprite> SnakeEating;
        public AudioClip HitSound;

        private AnimationController _animationController;

        private int _currentAnimationSpeed;
        private int _currentFrame;
        private bool _hasAttackedPlayer;

        void Start()
        {
            this.Register<PlayerDiedMessage>();
            _animationController = GetComponent<AnimationController>();
            _animationController.PlayAnimation(SnakeMoving);
        }

        void OnDestroy()
        {
            this.UnRegister<PlayerDiedMessage>();
        }

        private void Update()
        {
            transform.Translate(-2.5f * Time.deltaTime, 0, 0);

            if (transform.position.x <= -2)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name == "Player" && !_hasAttackedPlayer)
            {
                AudioSource.PlayClipAtPoint(HitSound, Vector3.zero);
                EventAggregator.SendMessage(new PlayerTakeDamageMessage());
                _hasAttackedPlayer = true;
                _animationController.PlayAnimation(SnakeEating);
            }
        }

        public void Handle(PlayerDiedMessage message)
        {
            _hasAttackedPlayer = true;
        }
    }
}