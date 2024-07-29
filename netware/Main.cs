using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace NetWare
{
    public class Main : MonoBehaviour
    {
        public void Start()
        {
            // start storage updater
            StartCoroutine(Storage.Update());

            // window data
            int windowWidth = 450;
            int windowHeight = 550;

            int windowX = 200;
            int windowY = ((Screen.height / 2) - (windowHeight / 2));

            // window position and size
            Menu.windowRect = new Rect(windowX, windowY, windowWidth, windowHeight);

            // setup config
            Config.Setup();
        }

        public void Update()
        {
            // run features
            if (PhotonNetwork.InRoom)
            {
                Combat.Execute();
                Movement.Execute();
                Visual.Execute();
                Exploits.Execute();
            }

            // check keybinds
            Menu.CheckKeybinds();

            // toggle window
            if (Input.GetKeyDown(KeyCode.Insert))
                Menu.displayWindow = !Menu.displayWindow;
        }

        public void OnGUI()
        {
            // run features
            Combat.Draw();
            if (PhotonNetwork.InRoom)
                Visual.Draw();
            Settings.Draw();

            // display window
            if (Menu.displayWindow)
                Menu.windowRect = GUI.Window(0, Menu.windowRect, BuildMenu, "<b>Net<color=red>Ware</color></b>", "Box");
        }

        // internal methods and variables
        private static List<List<float>> menuEffectsPoints = new List<List<float>>();
        private static float menuEffectsTimer = 0;

        private static void BuildMenu(int _)
        {
            // particles
            if (Config.GetBool("settings.interface.menueffects"))
            {
                float delay = Random.Range(
                    Config.GetFloat("settings.interface.menueffects.spawndelaymin", .1f),
                    Config.GetFloat("settings.interface.menueffects.spawndelaymax", .1f)
                );

                if ((Time.time - menuEffectsTimer) >= delay)
                {
                    menuEffectsPoints.Add(new List<float> {
                        (Menu.windowRect.width - Random.Range(10, (Menu.windowRect.width - 10))),
                        0
                    });
                    menuEffectsTimer = (Time.time + Random.Range(0, 1));
                }
                for (int index = 0; index < menuEffectsPoints.Count; index++)
                {
                    List<float> point = menuEffectsPoints[index];
                    int realX = (int)(Menu.windowRect.x + point[0]);
                    int realY = (int)(Menu.windowRect.y - point[1]);

                    Color particleColor = Colors.HexToRGB(Config.GetString("settings.interface.menueffects.color"));
                    switch (Config.GetString("settings.interface.menueffects.colormode"))
                    {
                        case "Rainbow":
                            particleColor = Colors.GetRainbow();
                            break;
                        case "Confetti":
                            particleColor = Colors.GetRainbow(point[0]);
                            break;
                    }
                    Render.DrawBox(
                        particleColor,
                        new Vector2(realX, realY),
                        3, 3
                    );
                    point[1] -= Config.GetFloat("settings.interface.menueffects.speed", .2f);

                    if (realY >= (Menu.windowRect.y + Menu.windowRect.height))
                        menuEffectsPoints.RemoveAt(index);
                }
            }

            // tab selectors
            GUILayout.BeginArea(new Rect(10, 10, (Menu.windowRect.width - 20), (Menu.windowRect.height - 20)));
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            for (int index = 0; index < Menu.tabs.Length; index++)
            {
                bool selected = (Menu.currentTab == index);
                string name = Menu.tabs[index];

                GUIStyle toggleStyle = new GUIStyle("Box");
                if (selected) {
                    toggleStyle.normal.textColor = Color.white;
                } else {
                    toggleStyle.normal.textColor = Color.gray;
                }

                int offset = 8;
                if (index == 2)
                    offset = 4;

                if (GUILayout.Toggle(selected, "<b>" + name + "</b>", toggleStyle, GUILayout.Width((Menu.windowRect.width / Menu.tabs.Length) - offset)))
                    Menu.currentTab = index;
            }
            GUILayout.EndHorizontal();

            // tabs
            Menu.tabScrollPosition = GUILayout.BeginScrollView(Menu.tabScrollPosition, false, false, GUIStyle.none, GUIStyle.none, GUIStyle.none);
            switch (Menu.currentTab)
            {
                case 0:
                    Combat.Tab();
                    break;
                case 1:
                    Visual.Tab();
                    break;
                case 2:
                    Movement.Tab();
                    break;
                case 3:
                    Exploits.Tab();
                    break;
                case 4:
                    Settings.Tab();
                    break;
                default:
                    Combat.Tab();
                    break;
            }
            GUILayout.EndScrollView();

            // make menu draggable
            GUILayout.EndArea();
            GUI.DragWindow(new Rect(0, 0, Menu.windowRect.width, 20));
        }
    }
}
