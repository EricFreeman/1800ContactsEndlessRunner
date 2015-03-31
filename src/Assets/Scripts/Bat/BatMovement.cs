using System;
using Assets.Scripts.Messages;
using Assets.Scripts.Shared;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEventAggregator;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Bat
{
    public class BatMovement : MonoBehaviour, IListener<BatTakeBoxMessage>
    {
        private AnimationController _animationController;

        private BatMovementTypes _movementType;
        private BatAnimations _batAnimations;
        private bool _hasTakenBox;

        void Start()
        {
            _animationController = GetComponent<AnimationController>();
            _batAnimations = GetComponent<BatAnimations>();

            _animationController.PlayAnimation(_batAnimations.IdleAnimation);
            _movementType = (BatMovementTypes)Random.Range(0, Enum.GetValues(typeof(BatMovementTypes)).Length);

            if (_movementType != BatMovementTypes.Hover)
            {
                transform.position = new Vector3(3, 2, 0);
            }

            this.Register<BatTakeBoxMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<BatTakeBoxMessage>();
        }

        void Update()
        {
            if (_hasTakenBox)
            {
                transform.Translate(-2 * Time.deltaTime, 2 * Time.deltaTime, 0);
            }
            else
            {
                transform.Translate(-2 * Time.deltaTime, 0, 0);

                if (_movementType == BatMovementTypes.SwoopHigh)
                {
                    if (transform.position.y > .5f)
                    {
                        transform.Translate(-.2f * Time.deltaTime, -1.5f * Time.deltaTime, 0);
                    }
                }
                else if (_movementType == BatMovementTypes.SwoopLow)
                {
                    if (transform.position.y > -.3f)
                    {
                        transform.Translate(-.2f * Time.deltaTime, -2.5f * Time.deltaTime, 0);
                    }
                }
            }

            if (transform.position.x < -2)
            {
                Destroy(gameObject);
            }
        }

        public void Handle(BatTakeBoxMessage message)
        {
            if (message.BatGameObject == gameObject)
            {
                _hasTakenBox = true;
                _animationController.PlayAnimation(_batAnimations.BoxAnimation);
            }
        }
    }
}