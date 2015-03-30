using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        public List<Sprite> IdleAnimation;
        public List<Sprite> RunAnimation;
        public List<Sprite> DieAnimation;
        public List<Sprite> JumpUpAnimation;
        public List<Sprite> FallDownAnimation;
        public List<Sprite> DeadFallingAnimation;
    }
}