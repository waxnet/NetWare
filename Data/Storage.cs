using System.Collections;
using UnityEngine;

namespace NetWare
{
    public class Storage : MonoBehaviour
    {
        public static PlayerController[] players = { };
        public static Pickupable[] pickupables = { };

        public static IEnumerator Update()
        {
            while (true)
            {
                players = FindObjectsOfType<PlayerController>();
                pickupables = FindObjectsOfType<Pickupable>();

                yield return new WaitForSeconds(1);
            }
        }
    }
}
