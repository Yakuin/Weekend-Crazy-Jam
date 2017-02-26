using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGUIElement : GuiUtility {

    public string action;
    private bool playerInside = false;
    private bool showPregame = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(Input.GetKey(KeyCode.Return) && playerInside && !showPregame)
        {
            switch(action)
            {
                case "new game":
                    Time.timeScale = 0.0f;
                    showPregame = true;
                    break;
                case "options":
                    break;
                case "about":
                    break;
            }
        }
        else if(showPregame)
        {
            DrawPregameInfo();
        }
	}

    private void DrawPregameInfo()
    {
        {
            guiStyle.wordWrap = true;
            Time.timeScale = 1.0f;
            DrawQuad(new Rect(Screen.width / 4 - 3, Screen.height / 4 - 3, Screen.width / 2 + 6, Screen.height / 2 + 6), darkPurple);
            DrawQuad(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), Color.black);
            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.normal.textColor = lightPurple;
            GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "You must land your ship on the exoplanets! Get to work, Houston!", guiStyle);
            guiStyle.normal.textColor = Color.white;
            if (GUI.Button(new Rect(Screen.width / 2 - 50, 3 * Screen.height / 4 - 50, 100, 50), "I'll try.", guiStyle))
            {
                Application.LoadLevel("gui_game");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().color *= 0.75f;
            Debug.Log("player entered me");
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().color /= 0.75f;
            Debug.Log("player left me");
            playerInside = false;
        }
    }
}
