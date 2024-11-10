using NetWare.Enums;
using NetWare.Extensions;

using System.Collections.Generic;
using System.Linq;

namespace NetWare.Modules.Tabs.Enums;

public static class Aim
{
    public static readonly Dictionary<string, EAimBone> AimBones = EnumExtension.GetValues<EAimBone>()
        .ToDictionary(x => x.ConvertToString());

    public static readonly Dictionary<string, EAimMode> AimModes = EnumExtension.GetValues<EAimMode>()
        .ToDictionary(x => x.ConvertToString());

    public static readonly Dictionary<string, EAimFilter> AimFilters = EnumExtension.GetValues<EAimFilter>()
        .ToDictionary(x => x.ConvertToString());
}
