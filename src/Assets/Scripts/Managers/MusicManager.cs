using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class MusicManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!_audioSource.isPlaying)
            {
                // TODO: Support more than one song
                _audioSource.Play();
            }
        }
    }
}