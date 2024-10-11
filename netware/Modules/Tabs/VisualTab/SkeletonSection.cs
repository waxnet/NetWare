using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.VisualTab;

public static class SkeletonSection
{
    public static void Draw()
    {
        Menu.NewSection("Skeleton");
        DrawToggle();
        DrawColors();
    }

    private static void DrawToggle()
    {
        // enabled
        var (Value, KeyBind) = Menu.NewToggle(
            "Enabled",
            Config.Active.Skeleton.Enabled,
            Config.Active.Skeleton.KeyBind
            );
        Config.Active.Skeleton.Enabled = Value;
        Config.Active.Skeleton.KeyBind = KeyBind;
    }
    private static void DrawColors()
    {
        Menu.NewTitle("Colors");

        // team color
        var teamColor = Menu.NewTextField("Team Color", Config.Active.Skeleton.TeamColor);
        Config.Active.Skeleton.TeamColor = teamColor;

        // enemy color
        var enemyColor = Menu.NewTextField("Enemy Color", Config.Active.Skeleton.EnemyColor);
        Config.Active.Skeleton.EnemyColor = enemyColor;

        // bot color
        var botColor = Menu.NewTextField("Bot Color", Config.Active.Skeleton.BotColor);
        Config.Active.Skeleton.BotColor = botColor;
    }
}
