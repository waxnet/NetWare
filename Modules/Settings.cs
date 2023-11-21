using UnityEngine;

namespace NetWare
{
    public class Settings : MonoBehaviour
    {
        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Config Manager");
            textFieldContent = Menu.NewTextField("Config Name", textFieldContent);
            Menu.NewButton("Save", Save);
            Menu.NewButton("Delete", Delete);

            Menu.Separate();

            Menu.NewSection("Config Loader");
            foreach (string config in Config.configList)
            {
                void Load()
                {
                    Config.Load(config);
                }

                Menu.NewButton(config, Load);
            }

            Menu.End();
        }

        // internal methods and variables
        private static string textFieldContent = "";

        private static void Save()
        {
            Config.Save(textFieldContent);
        }

        private static void Delete()
        {
            Config.Delete(textFieldContent);
        }
    }
}
