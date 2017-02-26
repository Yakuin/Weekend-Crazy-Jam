﻿using UnityEngine;
using System.Collections;
using System;

public class GuiUtility : MonoBehaviour
{

    public static int fontSize;
    public static int screenWidth;
    public static int screenHeight;
    public static bool showOptions = false;
    public static String soundToggle = "Sound On";
    public static GUIStyle guiStyle;

    public static Color lightPurple = new Color(0.6f, 0f, 0.6f);
    public static Color darkPurple = new Color(1f, 0f, 1f);

    public static void init(int setFontSize, int setScreenWidth, int setScreenHeight)
    {
        screenWidth = setScreenWidth;
        screenHeight = setScreenHeight;
        fontSize = setFontSize;
    }

    public static float getHalfTextWidth(String text)
    {
        int fontsize = fontSize;
        return ((text.Length) * fontsize) / 2;
    }

    public static float getFullTextWidth(String text)
    {
        int fontsize = fontSize;
        return ((text.Length) * fontsize);
    }

    public static void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    public static void drawOptionsMenu()
    {
        bool prevWordWrap = guiStyle.wordWrap;
        TextAnchor prevTextAnchor = guiStyle.alignment;
        guiStyle.wordWrap = true;
        DrawQuad(new Rect(Screen.width / 4 - 3, Screen.height / 8 - 3, Screen.width / 2 + 6, 3 * Screen.height / 4 + 6), darkPurple);
        DrawQuad(new Rect(Screen.width / 4, Screen.height / 8, Screen.width / 2, 3 * Screen.height / 4), Color.black);
        guiStyle.alignment = TextAnchor.MiddleCenter;
        guiStyle.normal.textColor = lightPurple;
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2),
            "This is where we put our controls.", guiStyle);
        guiStyle.normal.textColor = Color.white;
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 7 * Screen.height / 8 - 50, 100, 50), "Got it.", guiStyle))
        {
            guiStyle.wordWrap = false;
            showOptions = false;
        }
        else if (GUI.Button(new Rect(Screen.width / 4, 7 * Screen.height / 8 - 50, 150, 50), soundToggle, guiStyle))
        {
            if (soundToggle == "Sound On")
            {
                soundToggle = "Sound Off";
                AudioListener.volume = 0;
            }
            else
            {
                soundToggle = "Sound On";
                AudioListener.volume = 1.0f;
            }
        }
        guiStyle.wordWrap = prevWordWrap;
        guiStyle.alignment = prevTextAnchor;
    }
}
