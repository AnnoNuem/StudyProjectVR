﻿using UnityEngine;
using System.Collections;
using System.IO;

public class StartScreen : MonoBehaviour
{	
		private Rect windowRect = new Rect (Screen.width / 2 - 320, Screen.height / 2 - 240, 640, 480);
		private Rect buttonRect = new Rect (270, 260, 100, 30);
		private Rect textFieldRect = new Rect (270, 210, 100, 20);
		private Rect labelRect = new Rect (270, 185, 100, 20);

		void OnGUI ()
		{
				if (ManagerScript.state == ManagerScript.states.startScreen) {
						windowRect = GUI.Window (0, windowRect, WindowFunction, "Inlusio VR");
				}

		}
	
		void WindowFunction (int windowID)
		{
			if (GUI.Button (buttonRect, "Start")) {
				
				// intialize datapath value
				ManagerScript.trialINprocess = true;
				ManagerScript.trialFolder = Application.dataPath + @""+ManagerScript.chiffre+(System.DateTime.Now).ToString ("MMM-ddd-d-HH-mm-ss-yyyy");
				Directory.CreateDirectory (ManagerScript.trialFolder);

				ManagerScript.switchState(ManagerScript.states.questionaire);
			}
			ManagerScript.chiffre = GUI.TextField (textFieldRect, ManagerScript.chiffre);
			GUI.Label (labelRect, "Enter a Chiffre:");
		}
	
}