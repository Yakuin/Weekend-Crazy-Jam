using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGUI : GuiUtility {

    // load a style so our stuff works!
    public GUIStyle gStyle;

    void Awake()
    {
        guiStyle = new GUIStyle(gStyle);
        guiStyle.font = (Font)Resources.Load("GUI/kenvector_future", typeof(Font));
        guiStyle.onHover.textColor = Color.red;
        guiStyle.alignment = TextAnchor.MiddleCenter;
        guiStyle.hover.textColor = lightPurple;
        GuiUtility.init(16, Screen.width, Screen.height);
    }
}
