using UnityEngine;
using System.Collections;
using System.IO;

public class StartScreenFeelSpace : MonoBehaviour
{	
	private Rect windowRect = new Rect (Screen.width / 2 - 320, Screen.height / 2 - 240, 640, 480);
	private Rect buttonRect = new Rect (270, 260, 100, 30);
	private Rect textFieldRect = new Rect (270, 210, 100, 20);
	private Rect labelRect = new Rect (270, 185, 100, 20);

	void OnGUI ()
	{
		if (ManagerScriptFeelSpace.state == ManagerScriptFeelSpace.states.startScreen) {
			windowRect = GUI.Window (0, windowRect, WindowFunction, "Inlusio VR");
		}

	}

	void WindowFunction (int windowID)
	{
		ManagerScriptFeelSpace.chiffre = GUI.TextField (textFieldRect, ManagerScriptFeelSpace.chiffre);
		GUI.Label (labelRect, "Enter a Chiffre:");

		if (GUI.Button (buttonRect, "Start")) {

			//intializing data path for storing data
			ManagerScriptFeelSpace.trialINprocess = true;
			ManagerScriptFeelSpace.trialFolder = Application.dataPath + @"/Trial"+ManagerScriptFeelSpace.chiffre + (System.DateTime.Now).ToString ("MMM-ddd-d-HH-mm-ss-yyyy");

			if (!Directory.Exists (ManagerScriptFeelSpace.trialFolder)) {
				Directory.CreateDirectory (ManagerScriptFeelSpace.trialFolder);
			}

			recordDataFeelSpace.recordDataParametersInit();
			ManagerScriptFeelSpace.switchState(ManagerScriptFeelSpace.states.questionaire);
		}
	}
	
}