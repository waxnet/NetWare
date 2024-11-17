using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Modules.MenuTabs;
using NetWare.UI;
using NetWare.UI.Styles;
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
        StartCoroutine(Data.Storage.Update());
        StartCoroutine(Config.Update());

        // setup window data
        int windowWidth = 550;
        int windowHeight = 650;

        Menu.Version = "v2.4.5";
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
        Menu.Window(BuildMenu, "Net<color=red>Ware</color>");
    }
    private void BuildMenu(int _)
    {
        // version
        if (Menu.Version is not null)
        {
            // styles and data
            var versionContent = new GUIContent(Menu.Version);
            var versionStyle = LabelStyles.CreateVersion();
            var versionSize = versionStyle.CalcSize(versionContent);

            // draw
            GUILayout.BeginArea(new Rect(80, 20, versionSize.x, versionSize.y));
            GUILayout.Label(versionContent, versionStyle);
            GUILayout.EndArea();
        }

        // tab selectors
        GUILayout.BeginArea(new Rect(120, 10, (Menu.WindowRect.width - 125), (Menu.WindowRect.height - 20)));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        foreach (var tab in Menu.Tabs)
        {
            var isSelected = Menu.CurrentTab == tab;

            var toggleContent = new GUIContent(tab.TabName);
            var toggleStyle = ToggleStyles.CreateTab(isSelected);

            if (GUILayout.Toggle(isSelected, toggleContent, toggleStyle, GUILayout.Width(toggleStyle.CalcSize(toggleContent).x + 2)))
                Menu.CurrentTab = tab;
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        // tabs
        GUILayout.Space(10);
        GUILayout.BeginArea(new Rect(10, 40, (Menu.WindowRect.width - 20), (Menu.WindowRect.height - 45)));
        Menu.TabScrollPosition = GUILayout.BeginScrollView(Menu.TabScrollPosition, false, false, GUIStyle.none, GUIStyle.none, GUIStyle.none);

        Menu.CurrentTab.Tab();

        GUILayout.EndScrollView();

        // make menu draggable
        GUILayout.EndArea();
        GUI.DragWindow(new Rect(0, 0, Menu.WindowRect.width, 20));
        GUILayout.Space(10);
    }
}
