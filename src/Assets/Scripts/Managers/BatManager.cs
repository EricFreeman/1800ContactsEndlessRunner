using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class BatManager : MonoBehaviour, IListener<PauseRunningMessage>, IListener<ResumeRunningMessage>
    {
        public int MinSpawnDelay;
        public int MaxSpawnDelay;

        private int _currentSpawnDelay;

        public GameObject BatGameObject;

        private bool _isPaused = true;

        void Start()
        {
            _currentSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);
            this.Register<PauseRunningMessage>();
            this.Register<ResumeRunningMessage>();
        }

        private void OnDestroy()
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

                Instantiate(BatGameObject);
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