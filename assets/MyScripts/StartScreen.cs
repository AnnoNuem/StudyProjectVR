using UnityEngine;
using System.Collections;
using System.IO;

public class StartScreen : MonoBehaviour
{	
	private Rect windowRect = new Rect (Screen.width / 2 - 320, Screen.height / 2 - 240, 640, 480);
	private Rect buttonRect = new Rect (490, 400, 100, 30);
	private Rect textFieldRect = new Rect (270, 125, 100, 20);
	private Rect labelRectChif = new Rect (270, 100, 120, 20);
	private Rect labelRectCombo = new Rect (270, 185, 120, 20);
	private Rect comboBoxRect = new Rect(Screen.width / 2 - 320 + 270, Screen.height / 2 - 240 + 210, 100, 20);

	GUIContent[] comboBoxList;
	private ComboBox comboBoxControl;// = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();

	private void Start()
	{
			comboBoxList = new GUIContent[3];
			comboBoxList[0] = new GUIContent("Session 1");
			comboBoxList[1] = new GUIContent("Session 2");
			comboBoxList[2] = new GUIContent("Session 3");


		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
			listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
			listStyle.padding.right =
				listStyle.padding.top =
					listStyle.padding.bottom = 4;

			comboBoxControl = new ComboBox(comboBoxRect, comboBoxList[0], comboBoxList, "button", "box", listStyle);
	}


	void OnGUI ()
	{
			if (ManagerScript.state == ManagerScript.states.startScreen) {
				windowRect = GUI.Window (0, windowRect, WindowFunction, "Inlusio VR");
			// result of manger box selection is send to managerscript.session ( added 1 to get values between 1 and max)
			ManagerScript.session =  comboBoxControl.Show() + 1;
			Debug.Log (ManagerScript.session);
			}


	}

	void WindowFunction (int windowID)
	{
		ManagerScript.chiffre = GUI.TextField (textFieldRect, ManagerScript.chiffre);
		GUI.Label (labelRectChif, "Enter a Chiffre:");
		GUI.Label (labelRectCombo, "Select a Session:");

		if (GUI.Button (buttonRect, "Start")) {

			//intializing data path for storing data
			ManagerScript.trialINprocess = true;
			ManagerScript.trialFolder = Application.dataPath + @"/Trial"+ManagerScript.chiffre + (System.DateTime.Now).ToString ("MMM-ddd-d-HH-mm-ss-yyyy");

			if (!Directory.Exists (ManagerScript.trialFolder)) {
				Directory.CreateDirectory (ManagerScript.trialFolder);
			}

			recordData.recordDataParametersInit();
			ManagerScript.switchState(ManagerScript.states.questionaire);
		}
	}
	
}