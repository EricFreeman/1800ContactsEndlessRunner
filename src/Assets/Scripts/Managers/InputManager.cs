using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public static class InputManager
    {
        public static bool IsPressed()
        {
            return (Input.GetKeyDown(KeyCode.Space) || Input.touches.Count() > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
        }
    }
}