using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class BatManager : MonoBehaviour
    {
        public int MinSpawnDelay;
        public int MaxSpawnDelay;

        private int _currentSpawnDelay;

        public GameObject BatGameObject;

        void Start()
        {
            _currentSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);
        }

        void Update()
        {
            _currentSpawnDelay--;

            if (_currentSpawnDelay <= 0)
            {
                _currentSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);

                Instantiate(BatGameObject);
            }
        }
    }
}