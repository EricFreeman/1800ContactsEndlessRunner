using System.Collections.Generic;
using Assets.Scripts.Messages;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour, IListener<StartPlayerAnimationMessage>
    {
        public List<Sprite> RunAnimation;
        public List<Sprite> JumpAnimation; 

        public int FrameDelay;
        private int _currentFrameDelay;

        private int _frame;
        private List<Sprite> _currentAnimation;
        private bool _isOneShot;

        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentAnimation = RunAnimation;
            _currentFrameDelay = FrameDelay;

            this.Register<StartPlayerAnimationMessage>();
        }

        void Update()
        {
            _currentFrameDelay--;

            if (_currentFrameDelay <= 0)
            {
                _currentFrameDelay = FrameDelay;

                _frame++;
                if (_frame >= _currentAnimation.Count)
                {
                    _frame = 0;

                    if (_isOneShot)
                    {
                        _currentAnimation = RunAnimation;
                    }
                }

                _spriteRenderer.sprite = _currentAnimation[_frame];
            }
        }

        public void Handle(StartPlayerAnimationMessage message)
        {
            _isOneShot = message.IsOneShot;

            switch (message.Animation)
            {
                case PlayerAnimation.Run:
                    _currentAnimation = RunAnimation;
                    break;
                case PlayerAnimation.Jump:
                    _currentAnimation = JumpAnimation;
                    break;
            }
        }
    }
}