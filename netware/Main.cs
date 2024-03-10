using Photon.Pun;
using System;
using System.Runtime.InteropServices.ComTypes;
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

            // toggle window
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                Menu.displayWindow = !Menu.displayWindow;
            }
        }

        public void OnGUI()
        {
            // run features
            Combat.Draw();
            if (PhotonNetwork.InRoom)
            {
                Visual.Draw();
            }
            Settings.Draw();

            // display window
            if (Menu.displayWindow)
            {
                Menu.windowRect = GUI.Window(0, Menu.windowRect, BuildMenu, "<b>Net<color=red>Ware</color></b>", "Box");
            }
        }

        // internal methods
        private static void BuildMenu(int _)
        {
            // tab selectors
            GUILayout.BeginArea(new Rect(10, 10, (Menu.windowRect.width - 20), (Menu.windowRect.height - 20)));
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            for (int index = 0; index < Menu.tabs.Length; index++)
            {
                bool selected = (Menu.currentTab == index);
                string name = Menu.tabs[index];

                GUIStyle toggleStyle = new GUIStyle("Box");
                if (selected)
                {
                    toggleStyle.normal.textColor = Color.white;
                } else {
                    toggleStyle.normal.textColor = Color.gray;
                }

                int offset = 8;
                if (index == 2)
                {
                    offset = 4;
                }

                if (GUILayout.Toggle(selected, "<b>" + name + "</b>", toggleStyle, GUILayout.Width((Menu.windowRect.width / Menu.tabs.Length) - offset)))
                {
                    Menu.currentTab = index;
                }
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
