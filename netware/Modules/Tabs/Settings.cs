using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Enums;
using NetWare.Extensions;
using NetWare.Modules.MenuTabs;
using NetWare.UI;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NetWare.Modules.Tabs;

[MenuTab("Settings", 2)]
public sealed class Settings : MenuTab
{
    private static readonly Dictionary<string, TimeType> _timeTypes = EnumExtensions.GetValues<TimeType>()
        .ToDictionary(x => x.ConvertToString());

    private string _selectedConfig = null;

    public Settings() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();
        DrawConfigManager();
        DrawWatermark();
        DrawGameplay();

        Menu.Separate();
        DrawConfigSelector();

        Menu.End();
    }

    #region Config Manager
    private void DrawConfigManager()
    {
        Menu.NewSection("Config Manager");

        // config name
        _selectedConfig = Menu.NewTextField("Config Name", _selectedConfig);

        // buttons
        Menu.NewButton("Load", () =>
        {
            if (!string.IsNullOrEmpty(_selectedConfig))
                Config.Load(_selectedConfig);
        });

        Menu.NewButton("Save", () => Config.Save(_selectedConfig));
        Menu.NewButton("Delete", () => Config.Delete(_selectedConfig));

        GUILayout.Space(6);
        Menu.NewButton("Open Configs Folder", () =>
        {
            try
            {
                Config.OpenConfigFolder();
            } catch (Exception ex)
            {
                Native.MessageBox(ex.Message);
            }
        });
    }
    #endregion

    #region Watermark
    private static void DrawWatermark()
    {
        Menu.NewSection("Watermark");

        DrawWatermarkToggle();
        DrawWatermarkTime();
    }
    private static void DrawWatermarkToggle()
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
    private static void DrawWatermarkTime()
    {
        // time type
        var timeType = Menu.NewDropdown("Time Type", "WatermarkTimeType", Config.Active.Watermark.TimeType.ConvertToString(), _timeTypes.Keys);
        Config.Active.Watermark.TimeType = _timeTypes[timeType];
    }
    #endregion

    #region Gameplay
    private static void DrawGameplay()
    {
        Menu.NewSection("Gameplay");

        // fps cap
        var fpsCap = Menu.NewTextField("FPS Cap", Config.Active.FpsCapper.FpsRaw);

        // disconnect
        Menu.NewButton("Leave Current Match", PhotonNetwork.Disconnect);

        if (fpsCap == Config.Active.FpsCapper.FpsRaw)
            return;

        Config.Active.FpsCapper.FpsRaw = fpsCap;
        Config.Active.FpsCapper.Fps = int.Parse(fpsCap);
    }
    #endregion

    #region Config Selector
    private void DrawConfigSelector()
    {
        Menu.NewSection("Config Selection");

        foreach (string config in Config.ConfigNames)
            Menu.NewButton(config, () => _selectedConfig = config);
    }
    #endregion
}
