using NetWare.Configuration;
using NetWare.UI;

using Photon.Pun;

namespace NetWare.Modules.Tabs.SettingsTab;

public static class GameplaySection
{
    public static void Draw()
    {
        Menu.NewSection("Gameplay");

        // fps cap
        var fpsCap = Menu.NewTextField("FPS Cap", Config.Active.FpsCapper.FpsRaw);

        // disconnect
        Menu.NewButton("Leave Match", PhotonNetwork.Disconnect);

        if (fpsCap == Config.Active.FpsCapper.FpsRaw)
            return;
        Config.Active.FpsCapper.FpsRaw = fpsCap;
        Config.Active.FpsCapper.Fps = int.Parse(fpsCap);
    }
}
