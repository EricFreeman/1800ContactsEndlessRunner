using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    public class AnimationController : MonoBehaviour
    {
        private List<Sprite> _currentAnimation;
        private List<Sprite> _previousAnimation; 

        public float FramesPerSeconds;

        private int _currentFrame;
        private float _timeOnFrame;

        void Update()
        {
            _timeOnFrame += Time.deltaTime;
            if (_timeOnFrame >= FramesPerSeconds/60)
            {
                _timeOnFrame = 0;
                _currentFrame++;

                if (_currentFrame >= _currentAnimation.Count)
                {
                    _currentFrame = 0;
                    if (_previousAnimation != null)
                    {
                        _currentAnimation = _previousAnimation;
                        _previousAnimation = null;
                    }
                }
            }

            GetComponent<SpriteRenderer>().sprite = _currentAnimation[_currentFrame];
        }

        public void PlayAnimation(List<Sprite> anim, bool isOneShot)
        {
            if (isOneShot) _previousAnimation = _currentAnimation;
            _currentAnimation = anim;
        }
    }
}