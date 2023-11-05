namespace NetWare
{
    public class Config
    {
        // combat
        public static class Combat
        {
            public static class SilentAim
            {
                public static bool enabled = false;
                public static bool checkFov = true;
                public static float fovSize = 200;
            }

            public static class Weapons
            {
                public static bool noRecoil = false;
                public static bool infiniteAmmo = false;
                public static bool rapidFire = false;
            }
        }

        // visual
        public static class Visual
        {
            public static class ESP
            {
                public static bool tracers = false;
                public static bool nametags = false;
                public static bool skeleton = false;
            }

            public static class Camera
            {
                public static bool customFov = false;
                public static float customFovAmount = 100;
            }
        }

        // movement
        public static class Movement
        {
            public static class Speed
            {
                public static bool speed = false;
                public static float speedAmount = 5;
            }

            public static class Fly
            {
                public static bool fly = false;
                public static bool helicopter = false;
            }
        }

        // exploits
        public static class Exploits
        {
            public static class Player
            {
                public static bool godmode = false;
                public static bool instantLand = false;
            }

            public static class Other
            {
                public static bool autoPlay = false;
            }

            public static class World
            {
                public static bool buildingSpam = false;
                public static bool rigSpam = false;
                public static bool instantBreak = false;
            }
        }
    }
}
