using UnityEngine;

namespace Assets.Scripts.UI
{
    public class InstructionsGui : MonoBehaviour
    {
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}