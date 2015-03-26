using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject TreeGameObject;
        public int MinTreeSpawnDelay;
        public int MaxTreeSpawnDelay;
        private int _currentTreeSpawnDelay;

        void Start()
        {
            _currentTreeSpawnDelay = Random.Range(MinTreeSpawnDelay, MaxTreeSpawnDelay);
        }

        void Update()
        {
            _currentTreeSpawnDelay--;

            if (_currentTreeSpawnDelay <= 0)
            {
                _currentTreeSpawnDelay = Random.Range(MinTreeSpawnDelay, MaxTreeSpawnDelay);

                Instantiate(TreeGameObject);
            }
        }
    }
}