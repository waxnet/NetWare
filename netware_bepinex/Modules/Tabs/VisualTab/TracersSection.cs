using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.VisualTab;

public static class TracersSection
{
    public static void Draw()
    {
        Menu.NewSection("Tracers");
        DrawToggle();
        DrawColor();
    }

    private static void DrawToggle()
    {
        // enabled
        var (Value, KeyBind) = Menu.NewToggle(
            "Enabled",
            Config.Active.Tracers.Enabled,
            Config.Active.Tracers.KeyBind
            );
        Config.Active.Tracers.Enabled = Value;
        Config.Active.Tracers.KeyBind = KeyBind;
    }
    private static void DrawColor()
    {
        Menu.NewTitle("Colors");

        // team color
        var teamColor = Menu.NewTextField("Team Color", Config.Active.Tracers.TeamColor);
        Config.Active.Tracers.TeamColor = teamColor;

        // enemy color
        var enemyColor = Menu.NewTextField("Enemy Color", Config.Active.Tracers.EnemyColor);
        Config.Active.Tracers.EnemyColor = enemyColor;

        // bot color
        var botColor = Menu.NewTextField("Bot Color", Config.Active.Tracers.BotColor);
        Config.Active.Tracers.BotColor = botColor;
    }
}
