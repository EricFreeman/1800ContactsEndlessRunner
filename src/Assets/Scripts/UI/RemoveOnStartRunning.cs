using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class RemoveOnStartRunning : MonoBehaviour, IListener<ResumeRunningMessage>
    {
        void Start()
        {
            this.Register<ResumeRunningMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<ResumeRunningMessage>();
        }

        public void Handle(ResumeRunningMessage message)
        {
            Destroy(gameObject);
        }
    }
}