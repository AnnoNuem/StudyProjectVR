//using UnityEngine;
//using System.Collections;
//
//public class Questionaire : MonoBehaviour {
//
////	private Rect windowRect = new Rect (Screen.width / 2 - 320, Screen.height / 2 - 240, 640, 480);
//	private Rect buttonRect = new Rect (270, 260, 100, 30);
//	private Rect labelRect = new Rect (10, 185, 300, 20);
//	
//	void OnGUI ()
//	{
//		if (ManagerScript.getState () == ManagerScript.states.questionaire) {
//			//at the moment we not need no questionaire
//			ManagerScript.newTrial();
//		//	windowRect = GUI.Window (0, windowRect, WindowFunction, "Questionaire");
//		}
//		
//	}
//	
//	void WindowFunction (int windowID)
//	{
//		if (GUI.Button (buttonRect, "Continue")) {
//			ManagerScript.newTrial();
//		}
//		GUI.Label (labelRect, "Please answer the following questions:");
//	}
//	
//}
