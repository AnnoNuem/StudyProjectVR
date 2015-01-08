using UnityEngine;

public class StartMenu3dGui : VRGUI
{
    public GUISkin skin;

    //		int count = 0;
    public string SessionId;

    public string SessionNumber;
    public string debuggField;
    public static  int debugg = 0;

    public override void OnVRGUI ()
    {
        if (ManagerScript.getState() == ManagerScript.states.startScreen)
        {
            GUI.skin = skin;
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical("box");

            GUILayout.Label("<color=lime> Enter here the Subject id </color>");
            SessionId = GUILayout.TextField(SessionId, 25);
            ManagerScript.chiffre = SessionId;
            GUILayout.Label("<color=lime> Enter here the session number </color>");
            SessionNumber = GUILayout.TextField(SessionNumber, 25);
            int.TryParse(SessionNumber, out ManagerScript.session);
            //GUILayout.Label ("<color=lime> If feelspace, than comport ? </color>");
            //ComPort = GUILayout.TextField (ComPort, 25);
            GUILayout.Label("<color=lime> If Debugg, enter 1 ? </color>");
            debuggField = GUILayout.TextField(debuggField, 25);
            int.TryParse(debuggField, out debugg);


			
            ManagerScript.debugg = debugg;

            if (GUILayout.Button("ok", GUILayout.ExpandHeight(true)))
            {
                ManagerScript.switchState(ManagerScript.states.start);

                enabled = !enabled;
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
