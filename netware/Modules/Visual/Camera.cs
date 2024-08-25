using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public class Camera : MonoBehaviour
    {
        public void Update()
        {
            if (!PhotonNetwork.InRoom)
                return;

            // reset zoom if needed
            if (Data.resetZoom)
            {
                Data.resetZoom = false;
                LocalPlayer.GetCameraManager()?.ResetZoomStateInstant();
            }

            // fov changer
            if (Config.GetBool("visual.fovchanger.enabled")) {
                Data.resetFOV = true;
                Data.resetZoom = true;
                UnityEngine.Camera.main.fieldOfView = Config.GetFloat("visual.fovchanger.amount", 100);
            } else if (Data.resetFOV) {
                Data.resetFOV = false;
                UnityEngine.Camera.main.fieldOfView = 60;
            }

            // camera settings
            if (Config.GetBool("visual.camerasettings.enabled"))
            {
                var thirdPersonCamera = LocalPlayer.GetThirdPersonCamera();

                if (thirdPersonCamera != null)
                {
                    thirdPersonCamera.rightOffset = Config.GetFloat("visual.camerasettings.x", .2f);
                    thirdPersonCamera.SetHeight(Config.GetFloat("visual.camerasettings.y", 1.5f));
                    thirdPersonCamera.defaultDistance = Config.GetFloat("visual.camerasettings.z", 2.5f);
                }
                Data.resetZoom = true;
            }
        }
    }
}
