using UnityEngine;
using System.Collections;

// Main menu + ingame pause menu

public class MainMenu : GuiUtility {
    // load a style so our stuff works!
    public GUIStyle gStyle;
    bool showPregame = false;
    string[] items;
    // Use this for initialization
    // Update is called once per frame
    void Update () {
		
	}

    void Awake()
    {
        guiStyle = new GUIStyle(gStyle);
        items = new string[] {"gui testbed", "gameplay testbed", "Options", "About"};
        guiStyle.font = (Font)Resources.Load("GUI/kenvector_future", typeof(Font));
        guiStyle.onHover.textColor = Color.red;
        guiStyle.alignment = TextAnchor.MiddleCenter;
        guiStyle.hover.textColor = lightPurple;
        GuiUtility.init(16, Screen.width, Screen.height);
    }

    void OnGUI()
    {
        if (!showPregame)
        {
            guiStyle.normal.textColor = lightPurple;
            guiStyle.fontSize = 24;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 28, 100, 15), "Game Title", guiStyle);
            guiStyle.fontSize = 20;
            guiStyle.normal.textColor = Color.white;

            for (int i = 0; i < items.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + i * 30, 100, 15), items[i], guiStyle))
                {
                    switch (items[i])
                    {
                        case "gui testbed":
                            showPregame = true;
                            break;
                        case "gameplay testbed":
                            Application.LoadLevel("game");
                            break;
                        case "Options":
                            showOptions = true;
                            break;
                        case "About":
                            break;
                    }
                }
            }
            if (showOptions)
            {
                drawOptionsMenu();
            }
        } else
        {
            guiStyle.wordWrap = true;
            Time.timeScale = 1.0f;
            DrawQuad(new Rect(Screen.width / 4 - 3, Screen.height / 4 - 3, Screen.width / 2 + 6, Screen.height / 2 + 6), darkPurple);
            DrawQuad(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), Color.black);
            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.normal.textColor = lightPurple;
            GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, Screen.width/2, Screen.height/2), "You must land your ship on the exoplanets! Get to work, Houston!", guiStyle);
            guiStyle.normal.textColor = Color.white;
            if (GUI.Button(new Rect(Screen.width/2-50, 3*Screen.height/4-50, 100, 50), "I'll try.", guiStyle))
            {
                Application.LoadLevel("gui_game");
            }
        }
    }
    
    void test()
	{
	}
}
