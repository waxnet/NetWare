﻿using NetWare.Configuration;
using NetWare.Enums;
using NetWare.Extensions;
using NetWare.UI;
using System.Collections.Generic;
using System.Linq;

namespace NetWare.Modules.Tabs.SettingsTab;

public static class WatermarkSection
{
    private static readonly Dictionary<string, TimeType> _timeTypes = EnumExtensions.GetValues<TimeType>()
        .ToDictionary(x => x.ConvertToString());

    public static void Draw()
    {
        Menu.NewSection("Watermark");

        DrawToggle();
        DrawTime();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
            "Enabled",
            Config.Active.Watermark.Enabled,
            Config.Active.Watermark.KeyBind
            );

        Config.Active.Watermark.Enabled = enabled.Value;
        Config.Active.Watermark.KeyBind = enabled.KeyBind;
    }

    private static void DrawTime()
    {
        // time type
        var timeType = Menu.NewDropdown("Time Type", "WatermarkTimeType", Config.Active.Watermark.TimeType.ConvertToString(), _timeTypes.Keys);
        Config.Active.Watermark.TimeType = _timeTypes[timeType];
    }
}
