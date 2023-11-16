using UnityEngine;

namespace NetWare
{
    public class Visual : MonoBehaviour
    {
        public static void Execute()
        {
            // camera
            if (Config.GetBool("visual.camera.customfov"))
            {
                resetFOV = true;

                CameraManager cameraManager = LocalPlayer.GetCameraManager();

                if (cameraManager != null)
                {
                    cameraManager.ResetZoomStateInstant();
                    Camera.main.fieldOfView = Config.GetFloat("visual.camera.customfovamount");
                }
            } else if (resetFOV)
            {
                resetFOV = false;

                Camera.main.fieldOfView = 60;
            }
        }

        public static void Draw()
        {
            // player esp
            if (Config.GetBool("visual.esp.tracers") || Config.GetBool("visual.esp.skeleton") || Config.GetBool("visual.esp.boxes") || Config.GetBool("visual.esp.nametags"))
            {
                foreach (PlayerController playerController in Storage.players)
                {
                    if (!playerController.IsMine() && Players.IsPlayerAlive(playerController) && Skeleton.HasSkeleton(playerController))
                    {
                        // tracers
                        if (Config.GetBool("visual.esp.tracers"))
                        {
                            DrawPlayerTracer(playerController);
                        }

                        // skeleton
                        if (Config.GetBool("visual.esp.skeleton"))
                        {
                            DrawPlayerSkeleton(playerController);
                        }

                        // boxes
                        if (Config.GetBool("visual.esp.boxes"))
                        {
                            DrawPlayerBox(playerController);
                        }

                        // nametags
                        if (Config.GetBool("visual.esp.nametags"))
                        {
                            DrawPlayerNametag(playerController, Color.black);
                        }
                    }
                }
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("ESP");
            Config.SetBool(
                "visual.esp.tracers",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.tracers"),
                    "Tracers"
                )
            );
            Config.SetBool(
                "visual.esp.skeleton",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.skeleton"),
                    "Skeleton"
                )
            );
            Config.SetBool(
                "visual.esp.boxes",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.boxes"),
                    "Boxes"
                )
            );
            Config.SetBool(
                "visual.esp.nametags",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.nametags"),
                    "Nametags"
                )
            );

            Menu.Separate();

            Menu.NewSection("Camera");
            Config.SetBool(
                "visual.camera.customfov",
                Menu.NewToggle(
                    Config.GetBool("visual.camera.customfov"),
                    "Custom FOV"
                )
            );
            Config.SetFloat(
                "visual.camera.customfovamount",
                Menu.NewSlider(
                    "Custom FOV Amount",
                    Config.GetFloat("visual.camera.customfovamount"),
                    20,
                    150
                )
            );

            Menu.End();
        }

        // internal methods and variables
        private static bool resetFOV = false;

        private static void DrawPlayerTracer(PlayerController playerController)
        {
            // get position
            Vector3 playerScreenPosition = Position.ToScreen(Players.GetHipPosition(playerController));

            // check if player is on screen
            if (!Position.IsBehindCamera(playerScreenPosition))
            {
                // draw tracer
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
            if (!Position.IsBehindCamera(playerHeadScreenPosition))
            {
                // get name and name size
                string playerName = playerController.MFOHGDFOEHJ.DAGNIMBJDNP;
                if (playerController.POJDIMMBOCO)
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
            // get player animator and color
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

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
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

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
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

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
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

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
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

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
                    {
                        Render.DrawLine(color, originPosition, destinationPosition);
                    }
                }
            }
        }

        private static void DrawPlayerBox(PlayerController playerController)
        {
            // get positions and color
            Color color = Players.GetPlayerTeamColor(playerController);

            Vector3 headWorldPosition = Players.GetHeadPosition(playerController);
            headWorldPosition.y += .22f;
            Vector3 headScreenPosition = Position.ToScreen(headWorldPosition);

            Vector3 feetWorldPosition = Players.GetFeetPosition(playerController);
            feetWorldPosition.y -= .2f;
            Vector3 feetScreenPosition = Position.ToScreen(feetWorldPosition);

            // check if player is on screen
            if (!Position.IsBehindCamera(headScreenPosition) && !Position.IsBehindCamera(feetScreenPosition))
            {
                // get box position
                float boxX = headScreenPosition.x + (feetScreenPosition.x - headScreenPosition.x) * .5f;
                float boxY = headScreenPosition.y + (feetScreenPosition.y - headScreenPosition.y) * .5f;

                // get box size
                float boxHeightA = Mathf.Abs(headScreenPosition.x - feetScreenPosition.x) / 2;
                float boxHeightB = Mathf.Abs(headScreenPosition.y - feetScreenPosition.y) / 2;

                float boxWidth = boxHeightB / 1.5f;
                float boxHeight = boxHeightB;

                if (boxHeightA > boxWidth)
                {
                    boxWidth = boxHeightA;
                    boxHeight = boxHeightA / 1.5f;
                }

                // get corners
                Vector3 topLeft = new Vector3(boxX - boxWidth, boxY - boxHeight, 0);
                Vector3 topRight = new Vector3(boxX + boxWidth, boxY - boxHeight, 0);

                Vector3 bottomLeft = new Vector3(boxX - boxWidth, boxY + boxHeight, 0);
                Vector3 bottomRight = new Vector3(boxX + boxWidth, boxY + boxHeight, 0);

                // draw box
                Render.DrawLine(
                    color,
                    topLeft,
                    topRight
                );
                Render.DrawLine(
                    color,
                    topRight,
                    bottomRight
                );
                Render.DrawLine(
                    color,
                    bottomRight,
                    bottomLeft
                );
                Render.DrawLine(
                    color,
                    bottomLeft,
                    topLeft
                );
            }
        }
    }
}
