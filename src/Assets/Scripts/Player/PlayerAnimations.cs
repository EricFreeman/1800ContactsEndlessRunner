using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        public List<Sprite> Running;

        public int FrameDelay;
        private int _currentFrameDelay;

        private int _frame;
        private List<Sprite> _currentAnimation;
        private bool _isOneShot;

        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentAnimation = Running;
            _currentFrameDelay = FrameDelay;
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
                        _currentAnimation = Running;
                    }
                }

                _spriteRenderer.sprite = _currentAnimation[_frame];
            }
        }
    }
}