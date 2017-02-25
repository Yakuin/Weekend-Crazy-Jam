using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// in-game hud

public class GameUI : MonoBehaviour {
    public static GameUI self;

    #region data structures
    private class ActionText{
		public ActionText(string txt, GameObject obj, Color col, bool perm, int fontsize){
			text = txt;
			textColor = col;
			subject = obj;
			permanent = perm;
			fontSize = fontsize;
		}
		public string text;
		public GameObject subject;
		public Color textColor = Color.black;
		public bool permanent = false;
		public int fontSize = 15;
	}

    private Rect CalculateRect(ActionText t)
	{
		int mult = 1;
		foreach (ActionText txt in actionTexts) {
			if(t.Equals(txt))
				break;
			else if(txt.subject.Equals(t.subject))
				mult++;
		}
		if (t.subject == null) {
			actionTexts.Remove (t);
			return new Rect();
		}
		Vector3 pos = t.subject.transform.position;
		Vector3 tmp = Camera.main.WorldToScreenPoint(pos);
		tmp.y = Screen.height - tmp.y;
		return new Rect(new Vector2(tmp.x-(t.fontSize+1), tmp.y-((t.fontSize+1)+mult*(t.fontSize+1))), new Vector2(100, 100));
	}
    #endregion
    #region member variables
    // Private
    // the popup texts that appear (this system is taken from a previous project of mine)
    private List<ActionText> actionTexts = new List<ActionText>();
    // game over screen or something perhaps
	private Texture2D failure;
	private GUIStyle guiStyle = new GUIStyle();
	private Font guiFont;

    private int colonistsRemaining = 3;

    // Public
    // texture to show ships remaining
    public Texture2D shipIcon;
    // the arrow which will point toward the target planet
    public Texture2D arrowTowardsPlanetIcon;

    #endregion
    #region actiontext

    public void AddActionText(string text, GameObject pos, Color col, bool permanent = false, int fontSize = 15)
	{
		actionTexts.Add (new ActionText (text, pos, col, permanent, fontSize));
	}

	public void RemoveActionText(string text)
	{
		foreach (ActionText t in actionTexts) {
			if (t.text.Equals (text)) {
				actionTexts.Remove (t);
				return;
			}
		}
	}

	private void DrawActionTexts()
	{
        if (actionTexts.Count < 1)
            return;
		for (int i = 0; i < actionTexts.Count; i++) {
			ActionText text = actionTexts[i];
			guiStyle.fontSize = text.fontSize;
			guiStyle.normal.textColor = text.textColor;
			GUI.Label(CalculateRect(text), text.text, guiStyle);
			Color col = text.textColor;
			if(!text.permanent)
				text.textColor.a = Mathf.MoveTowards(text.textColor.a, 0.0f, 0.1f*Time.deltaTime);
			if(text.textColor.a == 0)
				actionTexts.Remove(text);
		}
	}
    #endregion

    void Start()
    {
        guiFont = (Font)Resources.Load("GUI/kenvector_future", typeof(Font));
        guiStyle.font = guiFont;
        guiStyle.fontSize = 10;
        self = this;
    }

	// Use this for initialization
	void OnGUI ()
    {
        DrawPlayerInfo();
        DrawActionTexts();
        // if player dies
        /*
            // pause the game & draw the failure screen
			Time.timeScale = 0;
			if (GUI.Button (new Rect (Screen.width / 2 - 160, Screen.height / 2 - 140, 320, 280), failure)) {
				Application.LoadLevel ("MainMenu");
			}
		}
        */
    }

    public void DrawPlayerInfo(/* player object */)
    {
        // draw fuel info
        guiStyle.normal.textColor = Color.white;
        // get player's fuel and draw it
        GUI.Label(new Rect(100, 100, 100, 100), "Fuel: 100%", guiStyle);
        for (int i = 0; i < colonistsRemaining; i++)
        {
            GUI.Label(new Rect(100, 200, 100, 100), "");
        }
    }
}
