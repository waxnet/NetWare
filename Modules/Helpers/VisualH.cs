using System.Collections.Generic;
using UnityEngine;

namespace NetWare.Helpers
{
    public class VisualH
    {
        // values
        public static bool resetFOV = false;

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

            if (headWorldPosition.y > feetWorldPosition.y)
            {
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
                if (playerController.DKGMJCDBDMN)
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
                    Colors.GetPlayerTeamColor(playerController),
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
