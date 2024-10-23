using NetWare.UI;
using Photon.Pun;

namespace NetWare.Modules.Tabs.MiscTab;

public static class GamePlaySection
{
    public static void Draw()
    {
        Menu.NewSection("Game Play");
        DrawButtons();
    }

    private static void DrawButtons()
    {
        // leave game
        Menu.NewButton(
            "Leave Game",
            PhotonNetwork.Disconnect
        );
    }
}
