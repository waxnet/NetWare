using NetWare.Entities;

namespace NetWare.Extensions;

public static class PlayerControllerExtension
{
    public static bool IsTeammate(this PlayerController playerController) => Players.IsPlayerTeammate(playerController);
    public static bool IsBot(this PlayerController playerController) => Players.IsPlayerBot(playerController);
    public static bool IsValid(this PlayerController playerController) => Players.IsPlayerValid(playerController);
}
