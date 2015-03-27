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
        public Vector3 StartHealthIconPosition = new Vector3(-1.3f, .7f, 0);
        public float DistanceBetweenIcon = .2f;

        public List<GameObject> HealthIcons;

        public bool _isRunning;

        void Start()
        {
            this.Register<PlayerTakeDamageMessage>();
            this.Register<ResumeRunningMessage>();

            HealthIcons = new List<GameObject>();
            for (var i = 0; i < Health; i++)
            {
                var icon = Instantiate(HealthIconGameObject);
                icon.transform.position = StartHealthIconPosition + new Vector3(DistanceBetweenIcon * i, 0, 0);
                icon.transform.parent = transform;
                HealthIcons.Add(icon);
            }

            transform.position = new Vector3(0, .2f, 0);
        }

        void OnDestroy()
        {
            this.UnRegister<PlayerTakeDamageMessage>();
            this.UnRegister<ResumeRunningMessage>();
        }

        void Update()
        {
            if (_isRunning)
            {
                transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, .25f * Time.deltaTime);
            }
        }

        public void Handle(PlayerTakeDamageMessage message)
        {
            Health--;

            if (Health >= 0)
            {
                var last = HealthIcons.Last();
                HealthIcons.Remove(last);
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
            _isRunning = true;
        }
    }
}