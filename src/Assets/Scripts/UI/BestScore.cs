using Assets.Scripts.Messages;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class BestScore : MonoBehaviour, IListener<NewHighScoreMessage>, IListener<ResumeRunningMessage>
    {
        public Text ScoreText;

        void Start()
        {
            this.Register<NewHighScoreMessage>();
            this.Register<ResumeRunningMessage>();

            var bestScore = PlayerPrefs.GetInt("BestScore");
            ScoreText.text = "RECORD: {0}".ToFormat(bestScore.ToString("N0"));
        }

        void OnDestroy()
        {
            this.UnRegister<NewHighScoreMessage>();
            this.UnRegister<ResumeRunningMessage>();
        }

        public void Handle(ResumeRunningMessage message)
        {
            ScoreText.color = new Color(1, 1, 1, 1);
        }

        public void Handle(NewHighScoreMessage message)
        {
            ScoreText.text = "RECORD: {0}".ToFormat(message.Score.ToString("N0"));
        }
    }
}