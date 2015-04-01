namespace Assets.Scripts.Messages
{
    public class DelaySpawnMessage
    {
        public int DelayTime;
        public bool IsEnemyDelay;
        public int Threshold { get; set; }
    }
}