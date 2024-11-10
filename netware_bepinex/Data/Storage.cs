using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace NetWare.Data;

public static class Storage
{
    // data
    public static PlayerController[] Players { get; private set; } = [];

    // methods
    public static IEnumerator Update()
    {
        while (true)
        {
            if (PhotonNetwork.InRoom)
                Players = Object.FindObjectsOfType<PlayerController>();
            yield return new WaitForSeconds(1);
        }
    }
}
