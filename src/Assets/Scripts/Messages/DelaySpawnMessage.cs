namespace Assets.Scripts.Messages
{
    public class DelaySpawnMessage
    {
        public float DelayTime { get; set; }
        public bool IsEnemyDelay { get; set; }
        public float Threshold { get; set; }
    }
}