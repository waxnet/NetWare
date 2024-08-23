using UnityEngine;

namespace NetWare
{
    public class MenuChecks : MonoBehaviour
    {
        public void Update()
        {
            // check keybinds
            Menu.CheckKeybinds();
        }
    }
}
