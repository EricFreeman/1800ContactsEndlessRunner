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

        public static bool IsPressedAboveBottomOfScreen()
        {
            return (Input.GetKeyDown(KeyCode.Space) || Input.touches.Count() > 0 && Input.GetTouch(0).position.y < Screen.height - 30);
        }

        public static bool IsPressedOnLeftSideOfScreen()
        {
            return (Input.GetKeyDown(KeyCode.Space) || Input.touches.Count() > 0 && Input.GetTouch(0).position.x < Screen.width/2);
        }
    }
}