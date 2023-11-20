using Photon.Pun;
using UnityEngine;

namespace NetWare
{
    public class Network : MonoBehaviour
    {
        public static BuildingNetworkController GetBuildingNetworkController()
        {
            return BuildingNetworkController.Instance;
        }

        public static void BecomeMasterClient()
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
        }
    }
}
