using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.UI;
using NetWare.UI.Styles;
using System;
using UnityEngine;

namespace NetWare.Modules.SettingsModules;

[NetWareComponent]
public sealed class Watermark : MonoBehaviour
{
    private string content = "Unknown";
    private int timer = 0;

    public void OnGUI()
    {
        if (!Config.Active.Watermark.Enabled)
            return;

        // get time
        var time = DateTime.Now.ToString("hh:mm:ss tt");
        if (Config.Active.Watermark.TimeType == Enums.ETimeType.Military)
            time = DateTime.Now.ToString("HH:mm:ss");

        // get fps
        if (timer >= 20)
        {
            var fpsNumber = 1 / Time.smoothDeltaTime;

            if (fpsNumber > 2000)
                content = "Unknown";
            else
                content = fpsNumber.ToString("#,##0");

            timer = 0;
        }

        timer++;

        // data
        var titleContent = new GUIContent($"Net<color=red>Ware</color> {Menu.Version} | {time} | {content} FPS");
        var titleStyle = LabelStyles.CreateWatermark();

        var titleSize = titleStyle.CalcSize(titleContent);
        var backgroundWidth = titleSize.x + 20;
        var backgroundHeight = 30;

        var boxPosition = new Vector2(
            (backgroundWidth / 2) + 20,
            (backgroundHeight / 2) + 20
        );

        // draw box
        Render.DrawBox(
            Color.black,
            boxPosition,
            backgroundWidth,
            backgroundHeight
        );

        // draw title
        var titleRect = new Rect(
            boxPosition.x - (backgroundWidth / 2),
            boxPosition.y - (backgroundHeight / 2),
            backgroundWidth,
            backgroundHeight
        );

        GUI.Label(titleRect, titleContent, titleStyle);
    }
}
