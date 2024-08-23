using System;
using UnityEngine;

namespace NetWare.Modules
{
    public class Watermark : MonoBehaviour
    {
        private string content = "Unknown";
        private int timer = 0;

        public void OnGUI()
        {
            if (!Config.GetBool("settings.watermark.enabled"))
                return;

            // get time
            string time = DateTime.Now.ToString("hh:mm:ss tt");
            if (Config.GetString("settings.watermark.timetype") == "Military")
                time = DateTime.Now.ToString("HH:mm:ss");

            // get fps
            if (timer >= 20)
            {
                float fpsNumber = (1 / Time.smoothDeltaTime);
                if (fpsNumber > 2000)
                    content = "Unknown";
                else
                    content = fpsNumber.ToString("#,##0");

                timer = 0;
            }
            timer++;

            // data
            GUIContent titleContent = new GUIContent($"Net<color=red>Ware</color> {Menu.version} | {time} | {content} FPS");
            GUIStyle titleStyle = new GUIStyle("Label")
            {
                wordWrap = false,
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
            };

            Vector2 titleSize = titleStyle.CalcSize(titleContent);
            float backgroundWidth = (titleSize.x + 20);
            float backgroundHeight = 30;

            Vector2 boxPosition = new Vector2(
                ((backgroundWidth / 2) + 20),
                ((backgroundHeight / 2) + 20)
            );

            // draw box
            Render.DrawBox(
                Color.black,
                boxPosition,
                backgroundWidth,
                backgroundHeight
            );

            // draw title
            Rect titleRect = new Rect(
                (boxPosition.x - (backgroundWidth / 2)),
                (boxPosition.y - (backgroundHeight / 2)),
                backgroundWidth,
                backgroundHeight
            );
            GUI.Label(titleRect, titleContent, titleStyle);
        }
    }
}
