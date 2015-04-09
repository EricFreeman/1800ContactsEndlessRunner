using Assets.Scripts.Managers;
using Assets.Scripts.Messages;
using Assets.Scripts.Shared;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IListener<PlayerDiedMessage>, IListener<StartGameMessage>
    {
        public float JumpHeight = .75f;
        public float JumpSpeed = 2;

        private bool _isJumping;
        private bool _isFalling;
        private Vector3 _basePosition;
        private Vector3 _jumpPinnacle;
        private bool _fallFaster;

        public AudioClip JumpAudio;

        private bool _isGameStarted;
        private bool _isPlayerDead;
        private float _deathTime;

        private AnimationController _animationController;
        private PlayerAnimations _animations;

        void Start()
        {
            _basePosition = transform.position;
            _jumpPinnacle = transform.position + new Vector3(0, JumpHeight, 0);
            _animationController = GetComponent<AnimationController>();
            _animations = GetComponent<PlayerAnimations>();

            _animationController.PlayAnimation(_animations.IdleAnimation);

            this.Register<PlayerDiedMessage>();
            this.Register<StartGameMessage>();
        }

        void OnRegister()
        {
            this.UnRegister<PlayerDiedMessage>();
            this.UnRegister<StartGameMessage>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (_isPlayerDead)
            {
                if (InputManager.IsPressedOnLeftSideOfScreen() && Time.fixedTime - _deathTime > 1)
                {
                    _isPlayerDead = false;
                    EventAggregator.SendMessage(new ResumeRunningMessage());
                    _animationController.PlayAnimation(_animations.RunAnimation);
                }
            }
            else if (!_isJumping && InputManager.IsPressed() && !_isPlayerDead && _isGameStarted)
            {
                _isJumping = true;
                _animationController.PlayAnimation(_animations.JumpUpAnimation);
                AudioSource.PlayClipAtPoint(JumpAudio, Vector3.zero);
            }
            else if (_isJumping)
            {
                if (!_isFalling)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _jumpPinnacle, JumpSpeed * Time.deltaTime);

                    if (transform.position == _jumpPinnacle || InputManager.IsPressed())
                    {
                        _isFalling = true;
                        _animationController.PlayAnimation(_animations.FallDownAnimation);
                    }
                }

                if (InputManager.IsPressed())
                {
                    _fallFaster = true;
                }
            }

            if (_isFalling)
            {
                transform.position = Vector3.MoveTowards(transform.position, _basePosition, JumpSpeed * Time.deltaTime * (_fallFaster ? 2 : 1));

                if (transform.position == _basePosition)
                {
                    _isJumping = false;
                    _isFalling = false;
                    _fallFaster = false;

                    if (_isPlayerDead)
                    {
                        _animationController.PlayAnimation(_animations.DieAnimation);
                    }
                    else
                    {
                        _animationController.PlayAnimation(_animations.RunAnimation);
                    }
                }
            }
        }

        public void Handle(PlayerDiedMessage message)
        {
            _isPlayerDead = true;
            _deathTime = Time.fixedTime;
            if (_isJumping)
            {
                _isFalling = true;
                _animationController.PlayAnimation(_animations.DeadFallingAnimation);
            }
            else
            {
                _animationController.PlayAnimation(_animations.DieAnimation);
            }
        }

        public void Handle(StartGameMessage message)
        {
            _isGameStarted = true;
            EventAggregator.SendMessage(new ResumeRunningMessage());
            _isJumping = true;
            _animationController.PlayAnimation(_animations.JumpUpAnimation);
            AudioSource.PlayClipAtPoint(JumpAudio, Vector3.zero);
        }
    }
}