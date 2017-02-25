using UnityEngine;
using System.Collections;

// Main menu + ingame pause menu

public class MainMenu : MonoBehaviour {
	public GUIStyle myStyle;
	bool showOptions = false;
	string soundToggle = "Sound On";
    bool showPregame = false;
    string[] items;
    // Use this for initialization
    // Update is called once per frame
    void Update () {
		
	}

    void Awake()
    {
        items = new string[] { "New Game", "Options", "About" };
    }

    void OnGUI()
	{
        if (!showPregame)
        {
            myStyle.normal.textColor = new Color(249.0f / 255.0f, 160.0f / 255.0f, 79.0f / 255.0f);
            myStyle.fontSize = 24;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 28, 100, 15), "7drl", myStyle);
            myStyle.fontSize = 16;
            myStyle.normal.textColor = Color.white;

            for (int i = 0; i < items.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + i * 20, 100, 15), items[i], myStyle))
                {
                    if (items[i] == "New Game")
                    {
                        Time.timeScale = 1.0f;
                        showPregame = true;
                 //       DrawQuad(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), Color.black);
                  //      GUI.Label(new Rect(Screen.width / 4 + 50, Screen.height / 4 + 50, 120, 120), "Monsters are attacking the castle! Can you survive the week before help arrives?");
                        //    Application.LoadLevel("castle");
                    }
                    else if (items[i] == "Options")
                    {
                        showOptions = true;

                    }
                    else if (items[i] == "About")
                    {
                    }
                }
            }
            if (showOptions)
            {
                myStyle.wordWrap = true;
                Time.timeScale = 1.0f;
                DrawQuad(new Rect(Screen.width / 4-3, Screen.height / 8-3, Screen.width / 2+6, 3 * Screen.height / 4+6), new Color(249.0f / 255.0f, 160.0f / 255.0f, 79.0f / 255.0f));
                DrawQuad(new Rect(Screen.width / 4, Screen.height / 8, Screen.width / 2, 3 * Screen.height / 4), Color.black);
                myStyle.alignment = TextAnchor.MiddleCenter;
                myStyle.normal.textColor = new Color(249.0f / 255.0f, 160.0f / 255.0f, 79.0f / 255.0f);
                GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), 
                    "WASD to move.\n123 to cast spells.\nE to pickup weapons. R to drop them.\nzxc to spawn allies (for gold.)\nLMB to attack with your weapon.\nSpacebar to upright yourself.", myStyle);
                myStyle.normal.textColor = Color.white;
                if (GUI.Button(new Rect(Screen.width / 2 - 50, 7 * Screen.height / 8 - 50, 100, 50), "Got it.", myStyle))
                {
                    showOptions = false;
                }
                else if (GUI.Button(new Rect(Screen.width / 4, 7 * Screen.height / 8 - 50, 100, 50), soundToggle, myStyle))
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
            }
        } else
        {
            myStyle.wordWrap = true;
            Time.timeScale = 1.0f;
            DrawQuad(new Rect(Screen.width / 4 - 3, Screen.height / 4 - 3, Screen.width / 2 + 6, Screen.height / 2 + 6), new Color(249.0f / 255.0f, 160.0f / 255.0f, 79.0f / 255.0f));
            DrawQuad(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), Color.black);
            myStyle.alignment = TextAnchor.MiddleCenter;
            myStyle.normal.textColor = new Color(249.0f / 255.0f, 160.0f / 255.0f, 79.0f / 255.0f);
            GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, Screen.width/2, Screen.height/2), "Monsters are attacking the castle! Help arrives in seven days! Can you survive?", myStyle);
            myStyle.normal.textColor = Color.white;
            if (GUI.Button(new Rect(Screen.width/2-50, 3*Screen.height/4-50, 100, 50), "I'll try.", myStyle))
            {
                Application.LoadLevel("castle");
            }
        }
    }
    public static void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
    void test()
	{
	}
}
