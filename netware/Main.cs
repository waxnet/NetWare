using UnityEngine;

namespace NetWare
{
    public class Main : MonoBehaviour
    {
        public void Start()
        {
            // start storage updater
            StartCoroutine(Storage.Update());
            
            // setup window data
            int windowWidth = 550;
            int windowHeight = 650;

            int windowX = 200;
            int windowY = ((Screen.height / 2) - (windowHeight / 2));

            Menu.version = "v2.1";
            Menu.color = Color.red;
            Menu.tabs = new string[] { "Combat", "Visual", "Settings" };
            Menu.windowRect = new Rect(windowX, windowY, windowWidth, windowHeight);

            // config
            Config.Setup();
        }

        public void Update()
        {
            // toggle window
            if (Input.GetKeyDown(KeyCode.RightShift))
                Menu.displayWindow = !Menu.displayWindow;
        }

        public void OnGUI()
        {
            // display window
            Menu.Window(BuildMenu, "Net<color=red>Ware</color>");
        }

        private static void BuildMenu(int _)
        {
            // version
            if (Menu.version != null)
            {
                // styles and data
                GUIContent versionContent = new GUIContent(Menu.version);
                GUIStyle versionStyle = new GUIStyle("Box")
                {
                    normal = {
                        background = Texture.NewTransparent(),
                    },
                    fontStyle = FontStyle.BoldAndItalic,
                    fontSize = 10,
                };
                Vector2 versionSize = versionStyle.CalcSize(versionContent);

                // draw
                GUILayout.BeginArea(new Rect(80, 20, versionSize.x, versionSize.y));
                GUILayout.Label(versionContent, versionStyle);
                GUILayout.EndArea();
            }

            // tab selectors
            GUILayout.BeginArea(new Rect(120, 10, (Menu.windowRect.width - 125), (Menu.windowRect.height - 20)));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            for (int index = 0; index < Menu.tabs.Length; index++)
            {
                bool selected = (Menu.currentTab == index);

                GUIContent toggleContent = new GUIContent(Menu.tabs[index]);
                GUIStyle toggleStyle = new GUIStyle("Box")
                {
                    normal = {
                        background = Texture.New(.075f, .075f, .075f),
                        textColor = (selected ? Color.white : Color.gray),
                    },
                    hover = {
                        background = Texture.New(.075f, .075f, .075f),
                        textColor = (selected ? Color.white : Color.gray),
                    },
                    fontStyle = FontStyle.Bold,
                    fontSize = 12,
                };

                if (GUILayout.Toggle(selected, toggleContent, toggleStyle, GUILayout.Width(toggleStyle.CalcSize(toggleContent).x + 2)))
                    Menu.currentTab = index;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            // tabs
            GUILayout.Space(10);
            GUILayout.BeginArea(new Rect(10, 40, (Menu.windowRect.width - 20), (Menu.windowRect.height - 45)));
            Menu.tabScrollPosition = GUILayout.BeginScrollView(Menu.tabScrollPosition, false, false, GUIStyle.none, GUIStyle.none, GUIStyle.none);
            switch (Menu.currentTab)
            {
                case 0:
                    Modules.Combat.Tab();
                    break;
                case 1:
                    Modules.Visual.Tab();
                    break;
                case 2:
                    Modules.Settings.Tab();
                    break;
                default:
                    Modules.Combat.Tab();
                    break;
            }
            GUILayout.EndScrollView();

            // make menu draggable
            GUILayout.EndArea();
            GUI.DragWindow(new Rect(0, 0, Menu.windowRect.width, 20));
            GUILayout.Space(10);
        }
    }
}
