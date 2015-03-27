using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        private bool _isPlaying;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isPlaying)
            {
                _isPlaying = true;
                EventAggregator.SendMessage(new ResumeRunningMessage());
            }

            if (_isPlaying)
            {
                var color = GetComponent<SpriteRenderer>().color;
                GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, color.a -= .005f);

                if (color.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}