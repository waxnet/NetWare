using Photon.Pun;

namespace NetWare
{
    public static class Network
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
            return FirebaseManager.ALADDIOCPKF;
        }

        public static ServerUser GetServerUser()
        {
            return GetFirebaseManager()?.FECNCMAKFGG;
        }
    }
}
