using UnityEngine;

namespace Assets.Scripts.Scene
{
    public class RepeatScroller : MonoBehaviour
    {
        public float Length;
        public float Speed;

        void Update()
        {
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
            if (transform.position.x <= -Length)
            {
                transform.Translate(Length, 0, 0);
            }
        }
    }
}