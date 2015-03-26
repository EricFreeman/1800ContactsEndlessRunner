using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Bat
{
    public class BatAttack : MonoBehaviour
    {
        private bool _hasAttacked;

        void OnTriggerEnter2D(Collider2D col)
        {
            if (_hasAttacked) return;

            EventAggregator.SendMessage(new PlayerTakeDamageMessage());
            _hasAttacked = true;
        }
    }
}