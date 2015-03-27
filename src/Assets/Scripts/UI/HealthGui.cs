using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class HealthGui : MonoBehaviour, IListener<PlayerTakeDamageMessage>, IListener<ResumeRunningMessage>
    {
        public int Health = 3;
        public GameObject HealthIconGameObject;

        private List<GameObject> _healthIcons;

        public bool _isRunning;

        void Start()
        {
            this.Register<PlayerTakeDamageMessage>();
            this.Register<ResumeRunningMessage>();
        } 

        void OnDestroy()
        {
            this.UnRegister<PlayerTakeDamageMessage>();
            this.UnRegister<ResumeRunningMessage>();
        }

        private void Setup()
        {
            Health = 3;
            _healthIcons = new List<GameObject>();
            for (var i = 0; i < Health; i++)
            {
                var icon = Instantiate(HealthIconGameObject);
                icon.transform.SetParent(transform, false);
                _healthIcons.Add(icon);
            }
        }

        public void Handle(PlayerTakeDamageMessage message)
        {
            Health--;

            if (Health >= 0)
            {
                var last = _healthIcons.Last();
                _healthIcons.Remove(last);
                Destroy(last);
            }

            if (Health == 0)
            {
                EventAggregator.SendMessage(new PlayerDiedMessage());
                EventAggregator.SendMessage(new PauseRunningMessage());
            }
        }

        public void Handle(ResumeRunningMessage message)
        {
            Setup();
            _isRunning = true;
        }
    }
}