using NetWare.Attributes;
using NetWare.Enums;
using NetWare.Extensions;
using NetWare.Modules.MenuTabs;
using NetWare.Modules.Tabs.CombatTab;
using NetWare.UI;

using System.Collections.Generic;
using System.Linq;

namespace NetWare.Modules.Tabs;

[Attributes.MenuTabAttribute("Combat")]
public sealed class Combat : MenuTabs.MenuTab
{
    public Combat() : base()
    {
    }

    public static readonly Dictionary<string, EAimBone> AimBones = EnumExtension.GetValues<EAimBone>()
        .ToDictionary(x => x.ConvertToString());

    public static readonly Dictionary<string, EAimMode> AimModes = EnumExtension.GetValues<EAimMode>()
        .ToDictionary(x => x.ConvertToString());

    public static readonly Dictionary<string, EAimFilter> AimFilters = EnumExtension.GetValues<EAimFilter>()
        .ToDictionary(x => x.ConvertToString());

    public override void Tab()
    {
        Menu.Begin();
        AimbotSection.Draw();

        Menu.Separate();
        SilentAimSection.Draw();

        Menu.End();
    }
}
