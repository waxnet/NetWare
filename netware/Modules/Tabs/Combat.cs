using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Enums;
using NetWare.Extensions;
using NetWare.Modules.MenuTabs;
using NetWare.Modules.Tabs.CombatTab;
using NetWare.UI;
using System.Collections.Generic;
using System.Linq;

namespace NetWare.Modules.Tabs;

[MenuTab("Combat")]
public sealed class Combat : MenuTab
{
    public static readonly Dictionary<string, AimBone> AimBones = EnumExtensions.GetValues<AimBone>()
        .ToDictionary(x => x.ConvertToString());

    public static readonly Dictionary<string, AimMode> AimModes = EnumExtensions.GetValues<AimMode>()
        .ToDictionary(x => x.ConvertToString());

    public static readonly Dictionary<string, AimFilter> AimFilters = EnumExtensions.GetValues<AimFilter>()
        .ToDictionary(x => x.ConvertToString());

    public Combat() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();
        AimbotSection.Draw();

        Menu.Separate();
        SilentAimSection.Draw();

        Menu.End();
    }
}
