using UnityEngine;

namespace Assets.Scripts.UI
{
    public class OpenAppScript : MonoBehaviour
    {
        public void GetTheApp()
        {
            #if UNITY_ANDROID
                Application.OpenURL("market://details?id=com.contacts1800.ecomapp");
            #elif UNITY_IPHONE
                Application.OpenURL("itms-apps://itunes.apple.com/app/id515430900");
            #else
                Application.OpenURL("https://www.1800contacts.com/mobile-apps.html");
            #endif
        }
    }
}