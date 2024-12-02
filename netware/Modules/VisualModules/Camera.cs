using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;

using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.VisualModules;

[NetWareComponent]
public sealed class Camera : MonoBehaviour
{
    public void Update()
    {
        if (!PhotonNetwork.InRoom)
            return;

        // reset zoom if needed
        if (Data.ResetZoom)
        {
            Data.ResetZoom = false;
            LocalPlayer.GetCameraManager()?.ResetZoomStateInstant();
        }

        // fov changer
        if (Config.Active.FovChanger.Enabled) {
            Data.ResetFov = true;
            Data.ResetZoom = true;
            UnityEngine.Camera.main.fieldOfView = Config.Active.FovChanger.Amount;
        } else if (Data.ResetFov) {
            Data.ResetFov = false;
            UnityEngine.Camera.main.fieldOfView = 60;
        }

        // camera settings
        if (!Config.Active.CameraSettings.Enabled)
            return;

        var thirdPersonCamera = LocalPlayer.GetThirdPersonCamera();
        if (thirdPersonCamera is not null)
        {
            thirdPersonCamera.rightOffset = Config.Active.CameraSettings.OffsetX;
            thirdPersonCamera.SetHeight(Config.Active.CameraSettings.OffsetY);
            thirdPersonCamera.defaultDistance = Config.Active.CameraSettings.OffsetZ;
        }

        Data.ResetZoom = true;
    }
}
