using NetWare.Attributes;

using UnityEngine;

namespace NetWare.Configuration.Configs.Visual;

public sealed class TracersConfig : IBindable
{
    [ConfigProperty] public bool Enabled { get; set; } = false;
    [ConfigProperty] public KeyCode? KeyBind { get; set; } = KeyCode.None;

    [ConfigProperty] public string TeamColor { get; set; } = "#00FF00";
    [ConfigProperty] public string EnemyColor { get; set; } = "#FF0000";
    [ConfigProperty] public string BotColor { get; set; } = "#FFFFFF";
}
