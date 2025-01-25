namespace NetWare.Modules.MiscModules;

public static class Locker
{
    public static void UnlockChampions()
    {
        var firebaseManager = Resolver.GetInstance<FirebaseManager>();
        var serverUser = Resolver.GetProperty<FirebaseManager, ServerUser>(firebaseManager, "ServerUser");

        if (serverUser is null)
            return;

        foreach (var championId in NetWare.Data.LockerData.ChampionIds)
            if (!serverUser.Champions.OwnedChampions.ContainsKey(championId))
                serverUser.Champions.OwnedChampions.Add(championId, new UserChampionData());
    }

    public static void UnlockChampionSkins()
    {
        var firebaseManager = Resolver.GetInstance<FirebaseManager>();
        var serverUser = Resolver.GetProperty<FirebaseManager, ServerUser>(firebaseManager, "ServerUser");

        if (serverUser is null)
            return;

        foreach (var championSkinId in NetWare.Data.LockerData.ChampionSkinIds)
            if (!serverUser.Skins.CharacterSkins.Contains(championSkinId))
                serverUser.Skins.CharacterSkins.Add(championSkinId);
    }

    public static void UnlockWeapons()
    {
        var firebaseManager = Resolver.GetInstance<FirebaseManager>();
        var serverUser = Resolver.GetProperty<FirebaseManager, ServerUser>(firebaseManager, "ServerUser");

        if (serverUser is null)
            return;

        foreach (var weaponId in NetWare.Data.LockerData.WeaponIds)
            if (!serverUser.Equipment.Equipment.ContainsKey(weaponId))
                serverUser.Equipment.Equipment.Add(weaponId, new UserUpgradeableItem());
    }

    public static void UnlockWeaponSkins()
    {
        var firebaseManager = Resolver.GetInstance<FirebaseManager>();
        var serverUser = Resolver.GetProperty<FirebaseManager, ServerUser>(firebaseManager, "ServerUser");

        if (serverUser is null)
            return;

        foreach (var weaponSkinId in NetWare.Data.LockerData.WeaponSkinIds)
            if (!serverUser.Skins.WeaponSkins.Contains(weaponSkinId))
                serverUser.Skins.WeaponSkins.Add(weaponSkinId);
    }

    public static void UnlockEmotes()
    {
        var firebaseManager = Resolver.GetInstance<FirebaseManager>();
        var serverUser = Resolver.GetProperty<FirebaseManager, ServerUser>(firebaseManager, "ServerUser");

        if (serverUser is null)
            return;

        for (var index = 1; index <= 50; index++)
            serverUser.Skins.OwnedEmotes.Add($"lol.1v1.playeremotes.pack.{index}");
    }

    public static void UnlockStickers()
    {
        var firebaseManager = Resolver.GetInstance<FirebaseManager>();
        var serverUser = Resolver.GetProperty<FirebaseManager, ServerUser>(firebaseManager, "ServerUser");

        if (serverUser is null)
            return;

        for (var index = 1; index <= 80; index++)
            serverUser.Skins.OwnedEmotes.Add($"lol.1v1.playerstickers.pack.{index}");
    }
}
