using System.Linq;
using System;
using UnityEngine;

namespace NetWare
{
    public class MenuChecks : MonoBehaviour
    {
        public void Update()
        {
            if (!Input.anyKey)
                return;

            foreach (string key in Config.config.Keys)
            {
                if (key.Split('.').Last() != "keybind")
                    continue;

                string keybindString = Config.GetString(key);
                var keybindKeycode = (KeyCode)Enum.Parse(typeof(KeyCode), keybindString);
                string toggleKey = key.Replace("keybind", "enabled");

                if (keybindString != "..." && keybindString != "None")
                    if (Input.GetKeyDown(keybindKeycode))
                        Config.SetBool(toggleKey, !Config.GetBool(toggleKey));
            }
        }
    }
}
