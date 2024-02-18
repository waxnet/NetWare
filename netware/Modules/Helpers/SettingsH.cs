namespace NetWare.Helpers
{
    public class SettingsH
    {
        // values
        public static string textFieldContent = "";
        public static string fpsCounterContent = "0";
        public static int fpsCounterTimer = 20;

        // config manager 
        public static void Save()
        {
            Config.Save(textFieldContent);
        }
        public static void Delete()
        {
            Config.Delete(textFieldContent);
        }
    }
}
