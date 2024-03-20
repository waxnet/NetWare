using Invector.CharacterController;
using System.Collections.Generic;
using UnityEngine;

namespace NetWare.Helpers
{
    public static class VisualH
    {
        // values
        public static bool resetFOV = false;
        public static int[] speedGraphData = new int[30];
        public static int speedGraphTimer = 0;

        // esp
        public static void DrawPlayerTracer(PlayerController playerController)
        {
            // get position
            Vector3 screenPosition = Position.ToScreen(Players.GetHipPosition(playerController));

            // check if player is on screen
            if (!Position.IsBehindCamera(screenPosition))
            {
                // draw tracer
                Render.DrawLine(
                    Colors.GetPlayerTeamColor(playerController),
                    Render.screenCenterBottom,
                    screenPosition
                );
            }
        }
        public static void DrawPlayerSkeleton(PlayerController playerController)
        {
            // get player animator and color
            Animator animator = playerController.GetComponent<Animator>();
            Color color = Colors.GetPlayerTeamColor(playerController);

            // check if player animator exists
            if (animator != null)
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
                        if ((index + 2) == Skeleton.spine.Length)
                        {
                            originPosition.z = 0;
                            destinationPosition.z = 0;

                            float circleRadius = (originPosition - destinationPosition).magnitude;

                            Render.DrawCircle(color, destinationPosition, circleRadius);

                            continue;
                        }

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
        public static void DrawPlayer3DBox(PlayerController playerController)
        {
            // get color
            Color color = Colors.GetPlayerTeamColor(playerController);

            // get head and feet world position
            Vector3 headWorldPosition = Players.GetHeadPosition(playerController);
            Vector3 feetWorldPosition = Players.GetFeetPosition(playerController);

            if (headWorldPosition.y > feetWorldPosition.y)
            {
                headWorldPosition.y += .22f;
                feetWorldPosition.y -= .2f;
            } else {
                headWorldPosition.y -= .22f;
                feetWorldPosition.y += .2f;
            }

            // change head and feet x and z positions
            Vector3 xzPosition = Vector3.Lerp(
                new Vector3(headWorldPosition.x, 0, headWorldPosition.z),
                new Vector3(feetWorldPosition.x, 0, feetWorldPosition.z),
                .5f
            );

            headWorldPosition.x = xzPosition.x;
            headWorldPosition.z = xzPosition.z;
            feetWorldPosition.x = xzPosition.x;
            feetWorldPosition.z = xzPosition.z;

            // get head and feet screen position
            Vector3 headScreenPosition = Position.ToScreen(headWorldPosition);
            Vector3 feetScreenPosition = Position.ToScreen(feetWorldPosition);

            // check if player is behind camera
            if (!Position.IsBehindCamera(headScreenPosition) && !Position.IsBehindCamera(feetScreenPosition))
            {
                // get box world points
                Vector3[] worldPoints = {
                    (headWorldPosition + new Vector3(.4f, 0, .4f)),
                    (headWorldPosition + new Vector3(-.4f, 0, .4f)),
                    (headWorldPosition + new Vector3(-.4f, 0, -.4f)),
                    (headWorldPosition + new Vector3(.4f, 0, -.4f)),
                    (headWorldPosition + new Vector3(.4f, 0, .4f)),

                    (feetWorldPosition + new Vector3(.4f, 0, .4f)),
                    (feetWorldPosition + new Vector3(-.4f, 0, .4f)),
                    (feetWorldPosition + new Vector3(-.4f, 0, -.4f)),
                    (feetWorldPosition + new Vector3(.4f, 0, -.4f)),
                    (feetWorldPosition + new Vector3(.4f, 0, .4f))
                };

                // get box screen points and check if they are drawable
                List<Vector3> screenPoints = new List<Vector3>();
                foreach (Vector3 worldPoint in worldPoints)
                {
                    screenPoints.Add(Position.ToScreen(worldPoint));
                }

                bool areScreenPointsDrawable = true;
                foreach (Vector3 screenPoint in screenPoints)
                {
                    if (Position.IsBehindCamera(screenPoint))
                    {
                        areScreenPointsDrawable = false;
                        break;
                    }
                }

                // draw box
                if (areScreenPointsDrawable)
                {
                    for (int index = 0; index < 4; index++)
                    {
                        if ((index + 1) > 4)
                        {
                            break;
                        }

                        Vector3 pointA = screenPoints[index];
                        Vector3 pointB = screenPoints[index + 1];

                        Render.DrawLine(
                            color,
                            pointA,
                            pointB
                        );
                    }
                    for (int index = 5; index < 9; index++)
                    {
                        if ((index + 1) > 9)
                        {
                            break;
                        }

                        Vector3 pointA = screenPoints[index];
                        Vector3 pointB = screenPoints[index + 1];

                        Render.DrawLine(
                            color,
                            pointA,
                            pointB
                        );
                    }
                    for (int index = 0; index < 9; index++)
                    {
                        if ((index + 5) > 9)
                        {
                            break;
                        }

                        Vector3 pointA = screenPoints[index];
                        Vector3 pointB = screenPoints[index + 5];

                        Render.DrawLine(
                            color,
                            pointA,
                            pointB
                        );
                    }
                }
            }
        }
        public static void DrawPlayer2DBox(PlayerController playerController)
        {
            // get color
            Color color = Colors.GetPlayerTeamColor(playerController);

            // get head and feet world position
            Vector3 headWorldPosition = Players.GetHeadPosition(playerController);
            Vector3 feetWorldPosition = Players.GetFeetPosition(playerController);

            if (headWorldPosition.y > feetWorldPosition.y) {
                headWorldPosition.y += .22f;
                feetWorldPosition.y -= .2f;
            } else {
                headWorldPosition.y -= .22f;
                feetWorldPosition.y += .2f;
            }

            // get head and feet screen position
            Vector3 headScreenPosition = Position.ToScreen(headWorldPosition);
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

                if (boxHeightA > boxWidth) {
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
        public static void DrawPlayerFilledBox(PlayerController playerController)
        {
            // get color and set alpha
            Color color = Colors.GetPlayerTeamColor(playerController);
            color.a = .2f;

            // get head and feet world position
            Vector3 headWorldPosition = Players.GetHeadPosition(playerController);
            Vector3 feetWorldPosition = Players.GetFeetPosition(playerController);

            if (headWorldPosition.y > feetWorldPosition.y) {
                headWorldPosition.y += .22f;
                feetWorldPosition.y -= .2f;
            } else {
                headWorldPosition.y -= .22f;
                feetWorldPosition.y += .2f;
            }

            // get head and feet screen position
            Vector3 headScreenPosition = Position.ToScreen(headWorldPosition);
            Vector3 feetScreenPosition = Position.ToScreen(feetWorldPosition);

            // check if player is behind camera
            if (!Position.IsBehindCamera(headScreenPosition) && !Position.IsBehindCamera(feetScreenPosition))
            {
                // get box position
                float boxX = headScreenPosition.x + (feetScreenPosition.x - headScreenPosition.x) * .5f;
                float boxY = headScreenPosition.y + (feetScreenPosition.y - headScreenPosition.y) * .5f;

                // get box size
                float boxHeightA = Mathf.Abs(headScreenPosition.x - feetScreenPosition.x);
                float boxHeightB = Mathf.Abs(headScreenPosition.y - feetScreenPosition.y);

                float boxWidth = boxHeightB / 1.5f;
                float boxHeight = boxHeightB;

                if (boxHeightA > boxWidth) {
                    boxWidth = boxHeightA;
                    boxHeight = boxHeightA / 1.5f;
                }

                // draw box
                Render.DrawBox(
                    color,
                    new Vector3(boxX, boxY, 0),
                    boxWidth,
                    boxHeight
                );
            }
        }
        public static void DrawPlayerInfo(PlayerController playerController, Color backgroundColor)
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
        public static void DrawPlayerNametag(PlayerController playerController, Color backgroundColor)
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
                if (Players.IsPlayerBot(playerController))
                    playerName += " (BOT)";
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

                Color teamLineColor = Colors.GetPlayerTeamColor(playerController);
                teamLineColor.a = backgroundColor.a;

                Render.DrawLine(
                    teamLineColor,
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

        // other
        public static void DrawSpeedGraph()
        {
            // update data
            if (speedGraphTimer >= 10)
            {
                speedGraphTimer = 0;

                for (int index = 0; index < (speedGraphData.Length - 1); index++)
                {
                    speedGraphData[index] = speedGraphData[index + 1];
                }

                vThirdPersonController localPlayer = LocalPlayer.GetThirdPersonController();
                if (localPlayer == null || localPlayer.Rigidbody == null) {
                    speedGraphData[speedGraphData.Length - 1] = 0;
                } else {
                    speedGraphData[speedGraphData.Length - 1] = (int)(localPlayer.Rigidbody.velocity.magnitude);
                }
            }
            speedGraphTimer++;

            // get highest value
            float highestValue = .1f;
            foreach (int point in speedGraphData)
                if (point > highestValue)
                    highestValue = point;

            // draw box
            Vector2 boxPosition = new Vector2(Render.screenCenterBottom.x, (Render.screenCenterBottom.y - 100));

            Color boxColor = Color.black;
            boxColor.a = .5f;

            Render.DrawBox(
                boxColor,
                boxPosition,
                584,
                86
            );

            // draw point
            Vector3 pointsOrigin = (Render.screenCenterBottom - new Vector3(290, 60));
            
            string colorMode = Config.GetString("visual.speedgraph.colormode");
            Color lineColor = Color.white;
            if (colorMode == "Normal")
                lineColor = Colors.HexToRGB(Config.GetString("visual.speedgraph.color"));

            float rainbowOffset = 0;

            for (int index = 0; index < (speedGraphData.Length - 1); index++)
            {
                int normalizedValue1 = (int)(speedGraphData[index] * 80 / highestValue);
                int normalizedValue2 = (int)(speedGraphData[index + 1] * 80 / highestValue);

                if (colorMode == "Rainbow") {
                    lineColor = Colors.GetRainbow();
                } else if (colorMode == "Rainbow Wave") {
                    lineColor = Colors.GetRainbow(rainbowOffset);
                }

                Render.DrawLine(
                    lineColor,
                    new Vector2(pointsOrigin.x, (pointsOrigin.y - normalizedValue1)),
                    new Vector2((pointsOrigin.x + 20), (pointsOrigin.y - normalizedValue2))
                );

                rainbowOffset -= .025f;
                pointsOrigin.x += 20;
            }
        }
        public static void DrawCrosshair()
        {
            Color scopeColor = HudManager.Instance.SniperScope.color;
            if (
                Config.GetBool("visual.crosshair.betterscope") &&
                LocalPlayer.GetWeaponStats().ZoomSettings.HasScope &&
                LocalPlayer.IsAiming()
            ) {
                scopeColor.a = 0;
                HudManager.Instance.SniperScope.color = scopeColor;

                Render.DrawBox(
                    Color.black,
                    Render.screenCenter,
                    Screen.width, 1
                );
                Render.DrawBox(
                    Color.black,
                    Render.screenCenter,
                    1, Screen.height
                );

                return;
            }
            scopeColor.a = 1;
            HudManager.Instance.SniperScope.color = scopeColor;

            Color color = Colors.HexToRGB(Config.GetString("visual.crosshair.color"));
            if (Config.GetBool("visual.crosshair.rainbow"))
                color = Colors.GetRainbow();

            Vector3 position = Render.screenCenter;
            if (Config.GetBool("visual.crosshair.dynamic"))
            {
                Vector3? aimPosition = LocalPlayer.GetAimPosition();
                if (aimPosition.HasValue)
                    position = Position.ToScreen((Vector3)aimPosition);
            }
            if (Position.IsBehindCamera(position))
                return;


            Render.DrawBox(
                Color.black,
                position,
                26, 4
            );
            Render.DrawBox(
                Color.black,
                position,
                4, 26
            );

            Render.DrawBox(
                color,
                position,
                24, 2
            );
            Render.DrawBox(
                color,
                position,
                2, 24
            );
        }
    }
}
