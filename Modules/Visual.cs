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
                    Camera.main.fieldOfView = Config.GetInt("visual.camera.customfovamount");
                }
            } else if (resetFOV) {
                resetFOV = false;
                Camera.main.fieldOfView = 60;
            }
        }

        public static void Draw()
        {
            // esp
            if (Config.GetBool("visual.esp.tracers") || Config.GetBool("visual.esp.skeleton") || Config.GetBool("visual.esp.boxes") || Config.GetBool("visual.esp.nametags"))
            {
                foreach (PlayerController playerController in Storage.players)
                {
                    if (Players.IsPlayerValid(playerController))
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

                        // info
                        if (Config.GetBool("visual.esp.info"))
                        {
                            DrawPlayerInfo(playerController, Color.black);
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
                "visual.esp.info",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.info"),
                    "Info"
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
            Config.SetInt(
                "visual.camera.customfovamount",
                Menu.NewSlider(
                    "Custom FOV Amount",
                    Config.GetInt("visual.camera.customfovamount"),
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
            Vector3 screenPosition = Position.ToScreen(Players.GetHipPosition(playerController));

            // check if player is on screen
            if (!Position.IsBehindCamera(screenPosition))
            {
                // draw tracer
                Render.DrawLine(
                    Players.GetPlayerTeamColor(playerController),
                    Render.screenCenterBottom,
                    screenPosition
                );
            }
        }

        private static void DrawPlayerSkeleton(PlayerController playerController)
        {
            // get player animator and color
            Animator animator = playerController.GetComponent<Animator>();
            Color color = Players.GetPlayerTeamColor(playerController);

            // check if player animator exists
            if (animator != null )
            {
                // spine
                for (int index = 0; (index + 1) != Skeleton.spine.Length; index++)
                {
                    HumanBodyBones originBone = Skeleton.spine[index];
                    HumanBodyBones destinationBone = Skeleton.spine[index + 1];

                    Vector3 originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
                    {
                        Render.DrawLine(color, originPosition, destinationPosition);
                    }
                }

                // arms
                for (int index = 0; (index + 1) != Skeleton.arms.Length; index++)
                {
                    HumanBodyBones originBone = Skeleton.arms[index];
                    HumanBodyBones destinationBone = Skeleton.arms[index + 1];

                    Vector3 originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
                    {
                        Render.DrawLine(color, originPosition, destinationPosition);
                    }
                }

                // legs
                for (int index = 0; (index + 1) != Skeleton.legs.Length; index++)
                {
                    HumanBodyBones originBone = Skeleton.legs[index];
                    HumanBodyBones destinationBone = Skeleton.legs[index + 1];

                    Vector3 originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

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

            // check if player is behind camera
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

        private static void DrawPlayerInfo(PlayerController playerController, Color backgroundColor)
        {
            // get positions
            Vector3 feetWorldPosition = Players.GetFeetPosition(playerController);
            feetWorldPosition.y -= .2f;
            Vector3 feetScreenPosition = Position.ToScreen(feetWorldPosition);

            // check if player is behind camera
            if (!Position.IsBehindCamera(feetScreenPosition))
            {
                // get info
                string rankXP = "RankXP : " + Players.GetPlayerRankXP(playerController)?.ToString();
                string distance = "Distance : " + Players.GetPlayerDistance(playerController).ToString("0.0") + "m";

                GUIStyle textStyle = new GUIStyle() { fontSize = 10 };
                textStyle.normal.textColor = Color.white;
                Vector2[] sizes = {
                    textStyle.CalcSize(new GUIContent(rankXP)),
                    textStyle.CalcSize(new GUIContent(distance))
                };

                Vector2 biggestSize = new Vector2(0, 0);
                foreach (Vector2 size in sizes)
                {
                    if (size.x > biggestSize.x)
                    {
                        biggestSize.x = size.x;
                    }

                    if (size.y > biggestSize.y)
                    {
                        biggestSize.y = size.y;
                    }
                }

                // draw info box
                float boxX = feetScreenPosition.x;
                float boxY = (feetScreenPosition.y + (biggestSize.y + 5));
                float boxSizeX = (biggestSize.x + 10);
                float boxSizeY = ((biggestSize.y * sizes.Length) + 5);

                Render.DrawBox(
                    backgroundColor,
                    new Vector2(
                        boxX,
                        boxY
                    ),
                    boxSizeX,
                    boxSizeY
                );

                // draw name
                int offset = 1;

                GUI.Label(
                    new Rect(
                        (feetScreenPosition.x - (biggestSize.x / 2)),
                        (float)(feetScreenPosition.y + ((biggestSize.y / 2) * offset)),
                        biggestSize.x,
                        (biggestSize.y * 1.5f)
                    ),
                    rankXP,
                    textStyle
                );
                offset += 2;

                GUI.Label(
                    new Rect(
                        (feetScreenPosition.x - (biggestSize.x / 2)),
                        (float)(feetScreenPosition.y + ((biggestSize.y / 2) * offset)),
                        biggestSize.x,
                        (biggestSize.y * 1.5f)
                    ),
                    distance,
                    textStyle
                );
            }
        }

        private static void DrawPlayerNametag(PlayerController playerController, Color backgroundColor)
        {
            // get positions
            Vector3 headWorldPosition = Players.GetHeadPosition(playerController);
            headWorldPosition.y += .22f;
            Vector3 headScreenPosition = Position.ToScreen(headWorldPosition);

            // check if player is on screen
            if (!Position.IsBehindCamera(headScreenPosition))
            {
                // get name and name size
                string playerName = Players.GetPlayerName(playerController);
                if (playerController.FJLDBODHGKB)
                {
                    playerName += " (BOT)";
                }
                Vector2 playerNameSize = new GUIStyle().CalcSize(new GUIContent(playerName));

                // draw name box
                float boxX = headScreenPosition.x;
                float boxY = ((headScreenPosition.y - playerNameSize.y) + 2);
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
                        (headScreenPosition.x - (playerNameSize.x / 2)),
                        (headScreenPosition.y - (playerNameSize.y * 2) + 7),
                        playerNameSize.x,
                        (playerNameSize.y * 1.5f)
                    ),
                    playerName
                );
            }
        }
    }
}
