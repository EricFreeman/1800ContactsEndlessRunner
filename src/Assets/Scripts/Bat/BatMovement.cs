using System;
using Assets.Scripts.Util;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Bat
{
    public class BatMovement : MonoBehaviour
    {
        private BatMovementTypes _movementType;

        void Start()
        {
            _movementType = (BatMovementTypes) Random.Range(0, Enum.GetValues(typeof(BatMovementTypes)).Length);

            if (_movementType != BatMovementTypes.Hover)
            {
                transform.position = new Vector3(3, 2, 0);
            }
        }

        void Update()
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
    }
}