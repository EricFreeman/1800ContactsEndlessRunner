using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class BaseSpawnManager : MonoBehaviour, IListener<PauseRunningMessage>, IListener<ResumeRunningMessage>
    {
        public GameObject SpawnableGameObject;
        public int MinSpawnDelay;
        public int MaxSpawnDelay;
        private int _currentSpawnDelay;

        private bool _isPaused = true;

        void Start()
        {
            _currentSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);

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

            _currentSpawnDelay--;

            if (_currentSpawnDelay <= 0)
            {
                _currentSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);

                Instantiate(SpawnableGameObject);
            }
        }

        public void Handle(PauseRunningMessage message)
        {
            _isPaused = true;
        }

        public void Handle(ResumeRunningMessage message)
        {
            _isPaused = false;
        }
    }
}