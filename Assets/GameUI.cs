using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// in-game hud

public class GameUI : GuiUtility {
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
	private Font guiFont;
    private List<Rect> elementPositions = new List<Rect>();
    private static Transform destination;
    private GameObject player;
    List<String> pauseMenuItems = new List<String>(){ "resume", "options", "restart", "quit"};

    private float distanceToEdgeOfDestination;

    public int colonistsRemaining = 8;
	public double fuel = 100;
	public int planetscore = 0;
	
    // Public
    // texture to show ships remaining
    public Texture2D shipIcon;
    // the arrow which will point toward the target planet
    public GameObject arrowTowardsPlanetIcon;
    private GameObject arrow;
    public float maxLerpTime = 5.0f;
    private bool arrowInitialized = false;
    private bool arrowIsGone = false;
    private bool gamePaused = false;
    private float lerpTime = 0.0f;
    private bool optionsUpdated = true;
    private string selected = null;

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
    
    public static void BindDestination(Transform planetPosition)
    {
        destination = planetPosition;
    }

    public float calculateOffscreenDistance()
    {
        return distanceToEdgeOfDestination = Vector3.Distance(player.transform.position, destination.position) - Camera.main.orthographicSize;
    }

    public bool isOffscreen(float distance)
    {
        return distance > Camera.main.orthographicSize + destination.localScale.x;
    }

    // refresh the gui/clear static refs (may be necessary)
    public static void Refresh()
    {
    }

    void Start()
    {
        guiStyle.alignment = TextAnchor.UpperLeft;
        guiStyle.wordWrap = false;
        self = this;
        player = GameObject.FindGameObjectWithTag("Player");
        arrow = Instantiate(arrowTowardsPlanetIcon);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            gamePaused = true;
        }
        if(gamePaused)
        {
            if (optionsUpdated == true && showOptions == false)
            {
                updateOptions(pauseMenuItems);
                optionsUpdated = false;
            }
            selected = controls();
        }
    }

	// Use this for initialization
	void OnGUI ()
    {
        DrawPlayerInfo();
        DrawActionTexts();
        if (gamePaused)
        {
            DrawPauseMenu();
            if (selected != null)
                GUI.FocusControl(selected);
        }
    }

    public void DrawPauseMenu()
    {
        guiStyle.normal.textColor = lightPurple;
        guiStyle.fontSize = 24;

        float startX = Screen.width / 3, startY = Screen.height / 8;
        float endX = Screen.width / 3, endY = 3 * Screen.height / 4;


        DrawQuad(new Rect(startX - 3, startY - 3, endX + 6, endY + 6), darkPurple);
        DrawQuad(new Rect(startX, startY, endX, endY), Color.black);
        float centerTitle = getHalfTextWidth("Game Title");
        GUI.Label(new Rect(Screen.width / 2 - centerTitle, startY + 26, 100, 15), "Game Title", guiStyle);
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;

        float textCenterY = (Screen.height / 2) - ((40 * pauseMenuItems.Count) / 2);


        for (int i = 0; i < pauseMenuItems.Count; i++)
        {
            GUI.SetNextControlName(pauseMenuItems[i]);
            if (GUI.Button(new Rect(Screen.width / 2 - centerTitle, textCenterY + i * 40 , 100, 15), pauseMenuItems[i], guiStyle))
            {
                switch (pauseMenuItems[i])
                {
                    case "resume":
                        Time.timeScale = 1.0f;
                        gamePaused = false;
                        break;
                    case "options":
                        showOptions = true;
                        optionsUpdated = true;
                        break;
                    case "restart":
                        Time.timeScale = 1.0f;
                        Application.LoadLevel(Application.loadedLevelName);
                        break;
                    case "quit":
                        Time.timeScale = 1.0f;
                        Application.LoadLevel("activemainmenu");
                        break;
                }
            }
        }
        if (showOptions)
        {
            drawOptionsMenu();
        }
    }

    public void DrawPlayerInfo(/* player object */)
    {
        // draw fuel info
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontSize = 16;
        // get player's fuel and draw it
        GUI.Label(new Rect(0, 0, 100, 100), "Fuel: " + (int)fuel + "%", guiStyle);
        float centerXColonists = Screen.width / 2 - getHalfTextWidth("Colony Ships Remaining");
        GUI.Label(new Rect(centerXColonists, 0, 100, 100), "Colony Ships Remaining", guiStyle);
        for (int i = 0; i < colonistsRemaining; i++)
        {
            GUI.Label(new Rect(centerXColonists + i * 15, 20, 100, 100), shipIcon);
        }
        GUI.Label(new Rect(Screen.width - getFullTextWidth("Planet " + planetscore + "/4"), 0, 100, 100), "Planet " + planetscore + "/4", guiStyle);
        DrawPlanetDestinationInfo();
    }

    private void DrawPlanetDestinationInfo()
    {
        Vector3 playerPos = player.transform.position;
   //     Debug.Log(isOffscreen(calculateOffscreenDistance()));
        if (isOffscreen(calculateOffscreenDistance()))
        {
            if(arrowInitialized == false)
            {
                arrowIsGone = false;
                lerpTime = 0.0f;
                float a = arrow.GetComponent<Renderer>().GetComponent<SpriteRenderer>().color.a;
                arrow.GetComponent<Renderer>().GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.MoveTowards(a, 1.0f, 0.3f * Time.deltaTime));
                if (a == 1.0f)
                    arrowInitialized = true;
            }
            Vector3 prevPosition = arrow.transform.position;
            if (lerpTime < maxLerpTime)
            {
                arrow.transform.position = Vector3.Lerp(prevPosition, player.transform.position + player.transform.up * 5f, Time.deltaTime);
                lerpTime += Time.deltaTime;
            }
            else
            {
                arrow.transform.position = player.transform.position + player.transform.up * 5f;
            }
            float angle = Mathf.Atan2(destination.position.y - arrow.transform.position.y, destination.position.x - arrow.transform.position.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            float a = arrow.GetComponent<Renderer>().GetComponent<SpriteRenderer>().color.a;
            if (a != 0)
                arrow.GetComponent<Renderer>().GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.MoveTowards(a, 0.0f, 0.3f * Time.deltaTime));
            else
                arrowIsGone = true;
            arrowInitialized = false;
            guiStyle.normal.textColor = new Color(1f,1f,1f,a);
        }
        Vector3 screenPlayerPos = Camera.main.WorldToScreenPoint(playerPos);
        guiStyle.fontSize = 8;
        String distInKm = (Vector3.Distance(playerPos, destination.position) * 1000f).ToString() + " km";
        GUI.Label(new Rect(screenPlayerPos.x - distInKm.Length * 3, screenPlayerPos.y + 8, 100, 100), distInKm, guiStyle);
        guiStyle.normal.textColor = new Color(1f, 1f, 1f, 1f);
    }
}
