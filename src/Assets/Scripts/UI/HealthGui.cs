using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class HealthGui : MonoBehaviour, IListener<PlayerTakeDamageMessage>
    {
        public int Health = 3;
        public GameObject HealthIconGameObject;
        public Vector3 StartHealthIconPosition = new Vector3(-1.3f, .7f, 0);
        public float DistanceBetweenIcon = .2f;

        public List<GameObject> HealthIcons; 

        void Start()
        {
            this.Register<PlayerTakeDamageMessage>();

            HealthIcons = new List<GameObject>();
            for (var i = 0; i < Health; i++)
            {
                var icon = Instantiate(HealthIconGameObject);
                icon.transform.position = StartHealthIconPosition + new Vector3(DistanceBetweenIcon * i, 0, 0);
                icon.transform.parent = transform;
                HealthIcons.Add(icon);
            }
        }

        void OnDestroy()
        {
            this.UnRegister<PlayerTakeDamageMessage>();
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
    }
}