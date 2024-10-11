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
        var (Value, KeyBind) = Menu.NewToggle(
            "Enabled",
            Config.Active.Boxes.Enabled,
            Config.Active.Boxes.KeyBind
            );

        Config.Active.Boxes.Enabled = Value;
        Config.Active.Boxes.KeyBind = KeyBind;
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
