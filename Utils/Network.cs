using Photon.Pun;
using UnityEngine;

namespace NetWare
{
    public class Network : MonoBehaviour
    {
        public static BuildingNetworkController GetBuildingNetworkController()
        {
            return FindObjectOfType<BuildingNetworkController>();
        }

        public static void BecomeMasterClient()
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
        }
    }
}
