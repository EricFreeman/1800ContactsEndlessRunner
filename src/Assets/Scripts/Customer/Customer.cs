using System.Collections.Generic;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Customer
{
    public class Customer : MonoBehaviour, IListener<PauseRunningMessage>, IListener<ResumeRunningMessage>
    {
        public List<Sprite> Standing;
        public List<Sprite> Happy;
        public List<AudioClip> HappyMessage;

        public float Speed = 2f;
        private bool _isHappy;
        private int _index;

        private bool _isPaused;

        void Start()
        {
            this.Register<PauseRunningMessage>();
            this.Register<ResumeRunningMessage>();

            _index = Random.Range(0, Standing.Count);
            GetComponent<SpriteRenderer>().sprite = Standing[_index];
        }

        void OnDestroy()
        {
            this.UnRegister<PauseRunningMessage>();
            this.UnRegister<ResumeRunningMessage>();
        }

        void Update()
        {
            if (_isPaused) return;

            transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name == "Player" && !_isHappy)
            {
                _isHappy = true;
                GetComponent<AudioSource>().clip = HappyMessage[_index];
                GetComponent<AudioSource>().Play();
                GetComponent<SpriteRenderer>().sprite = Happy[_index];
                EventAggregator.SendMessage(new EarnPointsMessage { Points = 1000 });
            }
        }

        public void Handle(PauseRunningMessage message)
        {
            _isPaused = true;
        }

        public void Handle(ResumeRunningMessage message)
        {
            _isPaused = false;
            Destroy(gameObject);
        }
    }
}