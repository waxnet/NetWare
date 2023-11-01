using UnityEngine;

namespace NetWare
{
    public class Visual : MonoBehaviour
    {
        public static void Execute()
        {
            // camera
            if (Config.Visual.Camera.customFov)
            {
                resetFOV = true;

                CameraManager localPlayerCameraManager = LocalPlayer.GetLocalPlayerCameraManager();

                localPlayerCameraManager.ResetZoomStateInstant();
                localPlayerCameraManager.MainCamera.fieldOfView = Config.Visual.Camera.customFovAmount;
            } else if (resetFOV)
            {
                resetFOV = false;
                LocalPlayer.GetLocalPlayerCameraManager().MainCamera.fieldOfView = 60;
            }
        }

        public static void Draw()
        {
            // esp
            if (Config.Visual.ESP.tracers || Config.Visual.ESP.nametags || Config.Visual.ESP.skeleton)
            {
                foreach (PlayerController playerController in Storage.players)
                {
                    if (!playerController.IsMine() && Players.IsPlayerAlive(playerController) && Skeleton.HasSkeleton(playerController))
                    {
                        // tracers
                        if (Config.Visual.ESP.tracers)
                        {
                            DrawPlayerTracer(playerController);
                        }

                        // nametags
                        if (Config.Visual.ESP.nametags)
                        {
                            DrawPlayerNametag(playerController, Color.black);
                        }

                        // skeleton
                        if (Config.Visual.ESP.skeleton)
                        {
                            DrawPlayerSkeleton(playerController);
                        }
                    }
                }
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("ESP");
            Config.Visual.ESP.tracers = Menu.NewToggle(Config.Visual.ESP.tracers, "Tracers");
            Config.Visual.ESP.nametags = Menu.NewToggle(Config.Visual.ESP.nametags, "Nametags");
            Config.Visual.ESP.skeleton = Menu.NewToggle(Config.Visual.ESP.skeleton, "Skeleton");

            Menu.Separate();

            Menu.NewSection("Camera");
            Config.Visual.Camera.customFov = Menu.NewToggle(Config.Visual.Camera.customFov, "Custom FOV");
            Config.Visual.Camera.customFovAmount = Menu.NewSlider("Custom FOV Amount", Config.Visual.Camera.customFovAmount, 20, 150);

            Menu.End();
        }

        // internal methods and variables
        private static bool resetFOV = false;

        private static void DrawPlayerTracer(PlayerController playerController)
        {
            Vector3 playerScreenPosition = Position.ToScreen(Players.GetHipPosition(playerController));

            if (Position.IsOnScreen(playerScreenPosition))
            {
                Render.DrawLine(
                    Players.GetPlayerTeamColor(playerController),
                    Render.screenCenterBottom,
                    playerScreenPosition
                );
            }
        }

        private static void DrawPlayerNametag(PlayerController playerController, Color backgroundColor)
        {
            // get positions
            Vector3 playerHeadWorldPosition = Players.GetHeadPosition(playerController);
            playerHeadWorldPosition.y += .2f;
            Vector3 playerHeadScreenPosition = Position.ToScreen(playerHeadWorldPosition);

            // check if player is on screen
            if (Position.IsOnScreen(playerHeadScreenPosition))
            {
                // get name and name size
                string playerName = playerController.ECFNDIOEOMA.CIJLGLDLKBF;
                if (playerController.EBPEIGIEEIF)
                {
                    playerName += " (BOT)";
                }
                Vector2 playerNameSize = new GUIStyle().CalcSize(new GUIContent(playerName));

                // draw name box
                float boxX = playerHeadScreenPosition.x;
                float boxY = (playerHeadScreenPosition.y - (playerNameSize.y + 5));
                float boxSizeX = (playerNameSize.x + 10);
                float boxSizeY = (playerNameSize.y + 5);

                Render.DrawBox(
                    backgroundColor,
                    new Vector2(
                        boxX,
                        boxY
                    ),
                    boxSizeX,
                    boxSizeY
                );

                // draw team line
                boxSizeX /= 2;
                boxSizeY /= 2;

                Render.DrawLine(
                    Players.GetPlayerTeamColor(playerController),
                    new Vector3(
                        boxX + boxSizeX,
                        boxY - boxSizeY
                    ),
                    new Vector3(
                        boxX - boxSizeX,
                        boxY - boxSizeY
                    )
                );

                // draw name
                GUI.Label(
                    new Rect(
                        (playerHeadScreenPosition.x - (playerNameSize.x / 2)),
                        (playerHeadScreenPosition.y - (playerNameSize.y * 2)),
                        playerNameSize.x,
                        (playerNameSize.y * 1.5f)
                    ),
                    playerName
                );
            }
        }

        private static void DrawPlayerSkeleton(PlayerController playerController)
        {
            // get player animator
            Animator playerAnimator = playerController.GetComponent<Animator>();
            Color color = Players.GetPlayerTeamColor(playerController);

            // check if player animator exists
            if (playerAnimator != null )
            {
                // spine
                for (int index = 0; (index + 1) != Skeleton.spine.Length; index++)
                {
                    HumanBodyBones originBone = Skeleton.spine[index];
                    HumanBodyBones destinationBone = Skeleton.spine[index + 1];

                    Vector3 originPosition = Position.ToScreen(playerAnimator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(playerAnimator.GetBoneTransform(destinationBone).transform.position);

                    if (Position.IsOnScreen(originPosition) && Position.IsOnScreen(destinationPosition))
                    {
                        Render.DrawLine(color, originPosition, destinationPosition);
                    }
                }

                // arms
                for (int index = 0; (index + 1) != Skeleton.rightArm.Length; index++)
                {
                    HumanBodyBones originBone = Skeleton.rightArm[index];
                    HumanBodyBones destinationBone = Skeleton.rightArm[index + 1];

                    Vector3 originPosition = Position.ToScreen(playerAnimator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(playerAnimator.GetBoneTransform(destinationBone).transform.position);

                    if (Position.IsOnScreen(originPosition) && Position.IsOnScreen(destinationPosition))
                    {
                        Render.DrawLine(color, originPosition, destinationPosition);
                    }
                }
                for (int index = 0; (index + 1) != Skeleton.leftArm.Length; index++)
                {
                    HumanBodyBones originBone = Skeleton.leftArm[index];
                    HumanBodyBones destinationBone = Skeleton.leftArm[index + 1];

                    Vector3 originPosition = Position.ToScreen(playerAnimator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(playerAnimator.GetBoneTransform(destinationBone).transform.position);

                    if (Position.IsOnScreen(originPosition) && Position.IsOnScreen(destinationPosition))
                    {
                        Render.DrawLine(color, originPosition, destinationPosition);
                    }
                }

                // legs
                for (int index = 0; (index + 1) != Skeleton.rightLeg.Length; index++)
                {
                    HumanBodyBones originBone = Skeleton.rightLeg[index];
                    HumanBodyBones destinationBone = Skeleton.rightLeg[index + 1];

                    Vector3 originPosition = Position.ToScreen(playerAnimator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(playerAnimator.GetBoneTransform(destinationBone).transform.position);

                    if (Position.IsOnScreen(originPosition) && Position.IsOnScreen(destinationPosition))
                    {
                        Render.DrawLine(color, originPosition, destinationPosition);
                    }
                }
                for (int index = 0; (index + 1) != Skeleton.leftLeg.Length; index++)
                {
                    HumanBodyBones originBone = Skeleton.leftLeg[index];
                    HumanBodyBones destinationBone = Skeleton.leftLeg[index + 1];

                    Vector3 originPosition = Position.ToScreen(playerAnimator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(playerAnimator.GetBoneTransform(destinationBone).transform.position);

                    if (Position.IsOnScreen(originPosition) && Position.IsOnScreen(destinationPosition))
                    {
                        Render.DrawLine(color, originPosition, destinationPosition);
                    }
                }
            }
        }
    }
}
