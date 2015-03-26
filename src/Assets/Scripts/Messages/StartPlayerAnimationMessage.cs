using Assets.Scripts.Util;

namespace Assets.Scripts.Messages
{
    public class StartPlayerAnimationMessage
    {
        public PlayerAnimation Animation { get; set; }
        public bool IsOneShot { get; set; }
    }
}
