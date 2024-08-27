using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace NetWare.Data;

public static class Storage
{
    public static PlayerController[] Players { get; private set; } = [];

    public static IEnumerator Update()
    {
        while (true)
        {
            UpdatePlayers();
            yield return new WaitForSeconds(1);
        }
    }

    private static void UpdatePlayers()
    {
        if (PhotonNetwork.InRoom)
            Players = Object.FindObjectsOfType<PlayerController>();
    }
}
