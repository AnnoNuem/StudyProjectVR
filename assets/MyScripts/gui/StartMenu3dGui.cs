using UnityEngine;
using System.Collections;
using System.IO;

public class StartMenu3dGui : VRGUI
{
		public GUISkin skin;
		int count = 0;
		public string SessionId;
		public string SessionNumber;
		public string ComPort ;
	
		public override void OnVRGUI ()
		{

				if (ManagerScript.state == ManagerScript.states.startScreen) {

						GUI.skin = skin;
						GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
						GUILayout.BeginVertical ("box");
			
						GUILayout.Label ("<color=lime> Enter here the session id </color>");
						SessionId= GUILayout.TextField (SessionId, 25);
						ManagerScript.chiffre = SessionId;
						GUILayout.Label ("<color=lime> Enter here the session number </color>");
						SessionNumber = GUILayout.TextField (SessionNumber, 25);
						int.TryParse (SessionNumber, out ManagerScript.session);
						GUILayout.Label ("<color=lime> If feelspace, than comport ? </color>");
						ComPort = GUILayout.TextField (ComPort, 25);
						

						if (GUILayout.Button ("ok", GUILayout.ExpandHeight (true))) {

								ManagerScript.trialINprocess = true;
								ManagerScript.trialFolder = Application.dataPath + @"/Trial-Session-" + ManagerScript.session + "-" + ManagerScript.chiffre + (System.DateTime.Now).ToString ("MMM-ddd-d-HH-mm-ss-yyyy");
				
								if (!Directory.Exists (ManagerScript.trialFolder)) {
										Directory.CreateDirectory (ManagerScript.trialFolder);
								}
				
								recordData.recordDataParametersInit ();
								ManagerScript.generateTrials ();
								ManagerScript.switchState (ManagerScript.states.walking);
								ManagerScript.newTrial();


								
				Debug.Log ("chiffre -->"+ManagerScript.chiffre );
				Debug.Log ("session -->"+ManagerScript.session );
				enabled = !enabled;
						}
				
						GUILayout.EndVertical ();
						GUILayout.EndArea ();
				}
		
		}
		
		
}

