using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bat
{
    public class BatAnimations : MonoBehaviour
    {
        public List<Sprite> IdleAnimation;
        public List<Sprite> BoxAnimation; 
        
        public int FrameDelay;
        private int _currentFrameDelay;

        private int _frame;
        private List<Sprite> _currentAnimation;

        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentAnimation = IdleAnimation;
            _currentFrameDelay = FrameDelay;
        }

        private void Update()
        {
            _currentFrameDelay--;

            if (_currentFrameDelay <= 0)
            {
                _currentFrameDelay = FrameDelay;

                _frame++;
                if (_frame >= _currentAnimation.Count)
                {
                    _frame = 0;
                }

                _spriteRenderer.sprite = _currentAnimation[_frame];
            }
        }

        public void TakeBox()
        {
            _currentAnimation = BoxAnimation;
        }
    }
}