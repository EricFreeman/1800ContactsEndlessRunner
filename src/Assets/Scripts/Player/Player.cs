using Assets.Scripts.Messages;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IListener<PlayerDiedMessage>
    {
        public float JumpHeight = .75f;
        public float JumpSpeed = 2;

        private bool _isJumping;
        private bool _isFalling;
        private Vector3 _basePosition;
        private Vector3 _jumpPinnacle;

        private bool _isPlayerDead;

        void Start()
        {
            _basePosition = transform.position;
            _jumpPinnacle = transform.position + new Vector3(0, JumpHeight, 0);

            this.Register<PlayerDiedMessage>();
        }

        void OnRegister()
        {
            this.UnRegister<PlayerDiedMessage>();
        }

        void Update()
        {
            if (_isPlayerDead)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _isPlayerDead = false;
                    EventAggregator.SendMessage(new ResumeRunningMessage());
                    EventAggregator.SendMessage(new StartPlayerAnimationMessage
                    {
                        Animation = PlayerAnimation.Run
                    });
                }
            }
            else if (!_isJumping && Input.GetKeyDown(KeyCode.Space) && !_isPlayerDead)
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

                        if (_isPlayerDead)
                        {
                            EventAggregator.SendMessage(new StartPlayerAnimationMessage
                            {
                                Animation = PlayerAnimation.Die
                            });
                        }
                        else
                        {
                            EventAggregator.SendMessage(new StartPlayerAnimationMessage
                            {
                                Animation = PlayerAnimation.Run
                            });
                        }
                    }
                }
            }
        }

        public void Handle(PlayerDiedMessage message)
        {
            _isPlayerDead = true;
            if (_isJumping)
            {
                _isFalling = true;
            }
        }
    }
}