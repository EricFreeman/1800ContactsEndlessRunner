using Assets.Scripts.Managers;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        private bool _isPlaying;
        private bool _isPhone;
        private bool _isFlashing;

        public int AnimationDelay = 12;
        private int _currentAnimationDelay;

        public Sprite Logo;
        public Sprite LogoSpaceToStart;
        public Sprite LogoTapToStart;

        void Start()
        {
            #if UNITY_ANDROID
                _isPhone = true;
            #elif UNITY_IPHONE
                _isPhone = true;
            #else
                _isPhone = false;
            #endif

            _currentAnimationDelay = AnimationDelay;
        }

        void Update()
        {
            _currentAnimationDelay--;
            if (_currentAnimationDelay <= 0 && !_isPlaying)
            {
                _currentAnimationDelay = AnimationDelay;
                _isFlashing = !_isFlashing;
                var spriteToUse = Logo;

                if (_isFlashing)
                {
                    spriteToUse = _isPhone ? LogoTapToStart : LogoSpaceToStart;
                }

                GetComponent<SpriteRenderer>().sprite = spriteToUse;
            }

            if (InputManager.IsPressedAboveBottomOfScreen() && !_isPlaying)
            {
                _isPlaying = true;
                GetComponent<SpriteRenderer>().sprite = Logo;
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

        public GameObject InstructionsPanel;
        private GameObject _currentInstructionsPanel;
        public GameObject Ui;

        public void OpenInstructions()
        {
            _currentInstructionsPanel = Instantiate(InstructionsPanel);
            _currentInstructionsPanel.transform.SetParent(Ui.transform, false);
        }
    }
}