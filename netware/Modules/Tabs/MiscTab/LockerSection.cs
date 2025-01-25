using NetWare.UI;

namespace NetWare.Modules.Tabs.MiscTab;

public static class LockerSection
{
    public static void Draw()
    {
        Menu.NewSection("Locker");
        DrawButtons();
    }

    private static void DrawButtons()
    {
        // unlock champions
        Menu.NewButton(
            "Unlock Champions",
            MiscModules.Locker.UnlockChampions
        );

        // unlock champion skins
        Menu.NewButton(
            "Unlock Champion Skins",
            MiscModules.Locker.UnlockChampionSkins
        );

        // unlock weapons
        Menu.NewButton(
            "Unlock Weapons",
            MiscModules.Locker.UnlockWeapons
        );

        // unlock weapon skins
        Menu.NewButton(
            "Unlock Weapon Skins",
            MiscModules.Locker.UnlockWeaponSkins
        );

        // unlock emotes
        Menu.NewButton(
            "Unlock Emotes",
            MiscModules.Locker.UnlockEmotes
        );

        // unlock stickers
        Menu.NewButton(
            "Unlock Stickers",
            MiscModules.Locker.UnlockStickers
        );
    }
}
