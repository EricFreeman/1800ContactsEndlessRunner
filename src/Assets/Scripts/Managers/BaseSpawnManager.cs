using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class BaseSpawnManager : MonoBehaviour, IListener<PauseRunningMessage>, IListener<ResumeRunningMessage>, IListener<DelaySpawnMessage>
    {
        public GameObject SpawnableGameObject;
        public int MinSpawnDelay;
        public int MaxSpawnDelay;
        public bool IsEnemy;

        private int _currentSpawnDelay;

        private bool _isPaused = true;

        void Start()
        {
            _currentSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);

            this.Register<PauseRunningMessage>();
            this.Register<ResumeRunningMessage>();
            this.Register<DelaySpawnMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<PauseRunningMessage>();
            this.UnRegister<ResumeRunningMessage>();
            this.UnRegister<DelaySpawnMessage>();
        }

        void Update()
        {
            if (_isPaused) return;

            _currentSpawnDelay--;

            if (_currentSpawnDelay <= 0)
            {
                _currentSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);

                Instantiate(SpawnableGameObject);

                if (IsEnemy)
                {
                    EventAggregator.SendMessage(new DelaySpawnMessage { DelayTime = 25, IsEnemyDelay = true, Threshold = 50 });
                }
            }
        }

        public void Handle(PauseRunningMessage message)
        {
            _isPaused = true;
        }

        public void Handle(ResumeRunningMessage message)
        {
            _isPaused = false;
            _currentSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);
        }

        public void Handle(DelaySpawnMessage message)
        {
            if ((!message.IsEnemyDelay || IsEnemy) && _currentSpawnDelay <= message.Threshold)
            {
                _currentSpawnDelay += message.DelayTime;
            }
        }
    }
}