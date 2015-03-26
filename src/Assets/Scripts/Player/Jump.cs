using Assets.Scripts.Messages;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Player
{
    public class Jump : MonoBehaviour
    {
        public float JumpHeight;
        public float JumpSpeed;

        private bool _isJumping;
        private bool _isFalling;
        private Vector3 _basePosition;
        private Vector3 _jumpPinnacle;

        void Start()
        {
            _basePosition = transform.position;
            _jumpPinnacle = transform.position + new Vector3(0, JumpHeight, 0);
        }

        void Update()
        {
            if (!_isJumping && Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
                EventAggregator.SendMessage(new StartPlayerAnimationMessage { Animation = PlayerAnimation.JumpUp });
            }
            else if (_isJumping)
            {
                if (!_isFalling)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _jumpPinnacle, JumpSpeed * Time.deltaTime);

                    if (transform.position == _jumpPinnacle || Input.GetKeyDown(KeyCode.Space))
                    {
                        _isFalling = true;
                        EventAggregator.SendMessage(new StartPlayerAnimationMessage { Animation = PlayerAnimation.FallDown });
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, _basePosition, JumpSpeed * Time.deltaTime);

                    if (transform.position == _basePosition)
                    {
                        _isJumping = false;
                        _isFalling = false;
                        EventAggregator.SendMessage(new StartPlayerAnimationMessage { Animation = PlayerAnimation.Run });
                    }
                }
            }
        }
    }
}