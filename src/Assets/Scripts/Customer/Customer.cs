using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Customer
{
    public class Customer : MonoBehaviour, IListener<PauseRunningMessage>, IListener<ResumeRunningMessage>
    {
        public Sprite Standing;
        public Sprite Happy;

        private bool _isHappy;

        public float Speed = 2.5f;

        private bool _isPaused;

        void Start()
        {
            this.Register<PauseRunningMessage>();
            this.Register<ResumeRunningMessage>();
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
                GetComponent<SpriteRenderer>().sprite = Happy;
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