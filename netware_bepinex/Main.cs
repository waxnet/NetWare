using BepInEx.Unity.IL2CPP.Utils.Collections;
using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Modules.MenuTabs;
using NetWare.UI;
using NetWare.Utils;

using System.Linq;

using UnityEngine;

namespace NetWare;

[NetWareComponent]
public class Main : MonoBehaviour
{
    public void Start()
    {
        // start updaters
        StartCoroutine(Data.Storage.Update().WrapToIl2Cpp());
        StartCoroutine(Config.Update().WrapToIl2Cpp());

        // setup window data
        int windowWidth = 550;
        int windowHeight = 650;

        Menu.Version = "v2.4.4";
        Menu.Color = Color.red;
        Menu.WindowRect = new Rect(200, ((Screen.height / 2) - (windowHeight / 2)), windowWidth, windowHeight);
        Menu.Tabs = SourceUtils.CreateInstancesAs<MenuTab>(
            SourceUtils.GetTypesWithAttribute<MenuTabAttribute>()
                .Where(x => !x.Type.IsAbstract)
                .OrderBy(x => x.Attribute.Order)
                .Select(x => x.Type)
        ).ToArray();
        Menu.CurrentTab = Menu.Tabs.First();

        // config
        Config.Setup();
    }

    public void Update()
    {
        // toggle window
        if (Input.GetKeyDown(KeyCode.RightShift))
            Menu.Enabled = !Menu.Enabled;
    }

    public void OnGUI()
    {
        // display window
        Menu.Window((GUI.WindowFunction)BuildMenu, $"<b><color=white>Net</color><color=red>Ware</color> {Menu.Version}</b>");
    }
    private void BuildMenu(int _)
    {
        // tab selectors
        GUILayout.BeginArea(new Rect(10, 20, (Menu.WindowRect.width - 20), 30));
        GUILayout.BeginHorizontal();

        foreach (var tab in Menu.Tabs)
        {
            // data
            var isSelected = Menu.CurrentTab == tab;

            // style
            var toggleContent = new GUIContent(tab.TabName);
            var toggleStyle = new GUIStyle("Box")
            {
                normal = {
                    textColor = (isSelected ? Color.white : Color.gray)
                },
                fontStyle = FontStyle.Bold
            };

            // draw
            if (GUILayout.Toggle(isSelected, toggleContent, toggleStyle))
                Menu.CurrentTab = tab;
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        // tabs
        GUILayout.BeginArea(new Rect(10, 50, (Menu.WindowRect.width - 20), (Menu.WindowRect.height - 55)));
        Menu.TabScrollPosition = GUILayout.BeginScrollView(Menu.TabScrollPosition, false, false, GUIStyle.none, GUIStyle.none, GUIStyle.none);

        Menu.CurrentTab.Tab();

        GUILayout.EndScrollView();

        // make menu draggable
        GUILayout.EndArea();
        GUI.DragWindow(new Rect(0, 0, Menu.WindowRect.width, 20));
        GUILayout.Space(10);
    }
}
