using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{

		private KeyCode pausekey = KeyCode.P;
		private ManagerScript.states prevState;
		private Rect windowRect = new Rect (Screen.width / 2 - 320, Screen.height / 2 - 240, 640, 480);
		private Rect labelRect = new Rect (170, 185, 300, 40);

	
		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
				// evalute if pause key is pressed if yes switch state to pause or to state before pause
				if (Input.GetKeyDown (pausekey)) {
						if (ManagerScript.state == ManagerScript.states.pause) {
								ManagerScript.switchState (prevState);
								Debug.Log ("resume");
						} else if (ManagerScript.state != ManagerScript.states.startScreen) {
								prevState = ManagerScript.state;
								ManagerScript.switchState (ManagerScript.states.pause);
								Debug.Log ("pause");
						}
				}
		}

		void OnGUI ()
		{
				// show pause screen
				if (ManagerScript.state == ManagerScript.states.pause) {
						Debug.Log ("pausewindow");
						windowRect = GUI.Window (0, windowRect, WindowFunction, "");
				}
		}

		void WindowFunction (int windowID)
		{
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.Label (labelRect, "PAUSE\n Press " + pausekey.ToString () + " to resume.");
		}

}
