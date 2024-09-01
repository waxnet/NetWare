using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.VisualTab;

public static class NameTagsSection
{
    public static void Draw()
    {
        Menu.NewSection("Nametags");
        DrawToggle();
        DrawColors();
    }

    private static void DrawToggle()
    {
        // enabled
        var (Value, KeyBind) = Menu.NewToggle(
            "Enabled",
            Config.Active.NameTags.Enabled,
            Config.Active.NameTags.KeyBind
            );
        Config.Active.NameTags.Enabled = Value;
        Config.Active.NameTags.KeyBind = KeyBind;
    }
    private static void DrawColors()
    {
        Menu.NewTitle("Colors");

        // team color
        var teamColor = Menu.NewTextField("Team Color", Config.Active.NameTags.TeamColor);
        Config.Active.NameTags.TeamColor = teamColor;

        // enemy color
        var enemyColor = Menu.NewTextField("Enemy Color", Config.Active.NameTags.EnemyColor);
        Config.Active.NameTags.EnemyColor = enemyColor;

        // bot color
        var botColor = Menu.NewTextField("Bot Color", Config.Active.NameTags.BotColor);
        Config.Active.NameTags.BotColor = botColor;

        // background color
        var bgColor = Menu.NewTextField("Background Color", Config.Active.NameTags.BackgroundColor);
        Config.Active.NameTags.BackgroundColor = bgColor;
    }
}
