using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class GameOverPanel : MonoBehaviour, IListener<PlayerDiedMessage>, IListener<ResumeRunningMessage>
    {
        public Image GameOverImage;

        void Start()
        {
            this.Register<PlayerDiedMessage>();
            this.Register<ResumeRunningMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<PlayerDiedMessage>();
            this.UnRegister<ResumeRunningMessage>();
        }

        public void Handle(PlayerDiedMessage message)
        {
            GameOverImage.gameObject.SetActive(true);
        }

        public void Handle(ResumeRunningMessage message)
        {
            GameOverImage.gameObject.SetActive(false);
        }
    }
}