using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class TreeManager : MonoBehaviour, IListener<PauseRunningMessage>, IListener<ResumeRunningMessage>
    {
        public GameObject TreeGameObject;
        public int MinTreeSpawnDelay;
        public int MaxTreeSpawnDelay;
        private int _currentTreeSpawnDelay;

        private bool _isPaused;

        void Start()
        {
            _currentTreeSpawnDelay = Random.Range(MinTreeSpawnDelay, MaxTreeSpawnDelay);

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

            _currentTreeSpawnDelay--;

            if (_currentTreeSpawnDelay <= 0)
            {
                _currentTreeSpawnDelay = Random.Range(MinTreeSpawnDelay, MaxTreeSpawnDelay);

                Instantiate(TreeGameObject);
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