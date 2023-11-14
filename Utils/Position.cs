using UnityEngine;

namespace NetWare
{
    public class Position : MonoBehaviour
    {
        public static Vector3 ToScreen(Vector3 worldPosition)
        {
            // project world position to screen position
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            screenPosition.y = Screen.height - screenPosition.y;

            // return screen position
            return screenPosition;
        }

        public static bool IsOnScreen(Vector3 screenPosition)
        {
            // check if screen position is actually on screen
            return (screenPosition.x >= 0 && screenPosition.x <= Screen.width) && (screenPosition.y >= 0 && screenPosition.y <= Screen.height) && (screenPosition.z > 0);
        }

        public static bool IsBehindCamera(Vector3 screenPosition)
        {
            // check if screen position is in front of the camera
            return (screenPosition.z < 0);
        }
    }
}