﻿using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Bat
{
    public class BatAttack : MonoBehaviour, IListener<PlayerDiedMessage>
    {
        public AudioClip Hit;

        private bool _hasAttacked;

        void Start()
        {
            this.Register<PlayerDiedMessage>();

        }

        void OnDestroy()
        {
            this.UnRegister<PlayerDiedMessage>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (_hasAttacked) return;

            AudioSource.PlayClipAtPoint(Hit, Vector3.zero);
            EventAggregator.SendMessage(new PlayerTakeDamageMessage());
            EventAggregator.SendMessage(new BatTakeBoxMessage { BatGameObject = gameObject});
            
            _hasAttacked = true;
        }

        public void Handle(PlayerDiedMessage message)
        {
            _hasAttacked = true;
        }
    }
}