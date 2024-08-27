using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.VisualTab;

public static class BoxesSection
{
    public static void Draw()
    {
        Menu.NewSection("Boxes");

        DrawToggle();
        DrawColor();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
            "Enabled",
            Config.Active.Boxes.Enabled,
            Config.Active.Boxes.KeyBind
            );

        Config.Active.Boxes.Enabled = enabled.Value;
        Config.Active.Boxes.KeyBind = enabled.KeyBind;
    }

    private static void DrawColor()
    {
        Menu.NewTitle("Colors");

        // team color
        var teamColor = Menu.NewTextField("Team Color", Config.Active.Boxes.TeamColor);
        Config.Active.Boxes.TeamColor = teamColor;

        // enemy color
        var enemyColor = Menu.NewTextField("Enemy Color", Config.Active.Boxes.EnemyColor);
        Config.Active.Boxes.EnemyColor = enemyColor;

        // bot color
        var botColor = Menu.NewTextField("Bot Color", Config.Active.Boxes.BotColor);
        Config.Active.Boxes.BotColor = botColor;
    }

}
