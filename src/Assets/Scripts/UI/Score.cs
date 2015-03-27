using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class Score : MonoBehaviour, IListener<ResumeRunningMessage>, IListener<PlayerDiedMessage>, IListener<EarnPointsMessage>
    {
        private int _score;

        private bool _isRunning;

        public Text ScoreText;

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
        }

        public void Handle(ResumeRunningMessage message)
        {
            _isRunning = true;
            _score = 0;
            ScoreText.color = new Color(1, 1, 1, 1);
        }

        public void Handle(PlayerDiedMessage message)
        {
            _isRunning = false;
        }

        public void Handle(EarnPointsMessage message)
        {
            _score += message.Points;
        }
    }
}