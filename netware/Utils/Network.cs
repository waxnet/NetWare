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
            PhotonNetwork.CurrentRoom.SetMasterClient(PhotonNetwork.LocalPlayer);
        }

        public static FirebaseManager GetFirebaseManager()
        {
            return FirebaseManager.IAOLLGGFCDC;
        }

        public static ServerUser GetServerUser()
        {
            return GetFirebaseManager()?.IOAJKGBFCFM;
        }
    }
}
