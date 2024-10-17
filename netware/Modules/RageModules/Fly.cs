using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;

using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.RageModules;

[NetWareComponent]
public sealed class Fly : MonoBehaviour
{
    public void Update()
    {
        var localPlayer = LocalPlayer.Get();

        if (
            !Config.Active.Fly.Enabled ||
            !PhotonNetwork.InRoom ||
            localPlayer is null
            ) {
            Data.playerPosition = default;
            return;
        } else if (Data.playerPosition == default) {
            Data.playerPosition = localPlayer.gameObject.transform.position;
        }

        // get direction input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // get camera forward and right directions
        var thirdPersonCamera = LocalPlayer.GetThirdPersonCamera();
        if (thirdPersonCamera is null)
            return;
        Vector3 forward = thirdPersonCamera.transform.forward;
        Vector3 right = thirdPersonCamera.transform.right;

        // get directions
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // calculate the new position
        Vector3 moveDirection = ((((forward * verticalInput) + (right * horizontalInput)) * Config.Active.Fly.HorizontalSpeed) * Time.deltaTime);

        // player current position
        Vector3 newPosition = Data.playerPosition;

        // horizontal movement
        newPosition += moveDirection;

        // vertical movement
        if (Input.GetKey(KeyCode.Space))
            newPosition += ((Vector3.up * Config.Active.Fly.VerticalSpeed) * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
            newPosition += ((Vector3.down * Config.Active.Fly.VerticalSpeed) * Time.deltaTime);

        // set new position
        Data.playerPosition = newPosition;
        localPlayer.gameObject.transform.position = newPosition;

        // set new rotation if spinbot is enabled
        if (Config.Active.Fly.Spin)
            localPlayer.gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
}
