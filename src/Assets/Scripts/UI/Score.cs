using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class Score : MonoBehaviour, IListener<ResumeRunningMessage>, IListener<PlayerDiedMessage>, IListener<EarnPointsMessage>
    {
        private int _score;
        private int _previousBest;

        private bool _isRunning;

        public Text ScoreText;
        public AudioClip NewHighScoreClip;

        private bool _hasPlayedNewHighScoreClip;

        void Start()
        {
            this.Register<ResumeRunningMessage>();
            this.Register<PlayerDiedMessage>();
            this.Register<EarnPointsMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<ResumeRunningMessage>();
            this.UnRegister<PlayerDiedMessage>();
            this.UnRegister<EarnPointsMessage>();
        }

        void Update()
        {
            if (!_isRunning) return;

            _score++;
            ScoreText.text = _score.ToString("N0");

            if (_score > _previousBest && !_hasPlayedNewHighScoreClip && _score > 0)
            {
                AudioSource.PlayClipAtPoint(NewHighScoreClip, Vector3.zero);
                _hasPlayedNewHighScoreClip = true;
            }
        }

        public void Handle(ResumeRunningMessage message)
        {
            _isRunning = true;
            _score = 0;
            ScoreText.color = new Color(1, 1, 1, 1);
            _previousBest = PlayerPrefs.GetInt("BestScore");
        }

        public void Handle(PlayerDiedMessage message)
        {
            _isRunning = false;
            
            var best = PlayerPrefs.GetInt("BestScore");
            if (_score > best)
            {
                PlayerPrefs.SetInt("BestScore", _score);
                EventAggregator.SendMessage(new NewHighScoreMessage { Score = _score});
            }
        }

        public void Handle(EarnPointsMessage message)
        {
            _score += message.Points;
        }
    }
}