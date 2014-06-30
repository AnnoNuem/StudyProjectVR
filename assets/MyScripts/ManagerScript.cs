using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class ManagerScript : MonoBehaviour
{
		// when should the questionaire be in the middle be
		int middleQuestionaire = 5;


		// states for state machine to describe in which experiment state we are
		public enum states
		{
				startScreen,
				walking,
				pointing,
				questionaire,
				pause}
		;

		// chiffre for identification, can be changed in start screen
		public static string chiffre = "";
		public static states state;

		//public list of trials
		public static List<trialContainer> trialList = new List<trialContainer> ();
		//static variable tracks what trial is in process
		public static int trialNumber = 0 ;
		public static string trialFolder = Application.dataPath + @"\Trial" + (System.DateTime.Now).ToString ("MMM-ddd-d-HH-mm-ss-yyyy");
		public static bool trialINprocess = false;
		public static bool pointTaskINprocess = false;
	

		//Trials and random variables will be generated here
		void Awake ()
		{
				trialINprocess = true;

				if (!Directory.Exists (ManagerScript.trialFolder)) {
						Directory.CreateDirectory (ManagerScript.trialFolder);
				}

				//adding trials to the list
				//Generate random values for conditions
				for (int i=0; i<5; i++) {
						trialContainer tempTrial = new trialContainer ();
						tempTrial.lightColor = "red";
						trialList.Add (tempTrial);
				}
		}
		//
		void Start ()
		{
				ManagerScript.switchState (states.startScreen);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (trialNumber == middleQuestionaire) {
						switchState (states.questionaire);
				}
		}

		public static void abortTrial ()
		{
		trialNumber++;
				trialINprocess = false;
				CameraFade.StartAlphaFade (Color.black, false, 2f, 2f);
		switchState (states.walking);
		}

	public static void newTrial ()
	{
		trialNumber++;
		trialINprocess = false;
		CameraFade.StartAlphaFade (Color.black, false, 2f, 2f);
		switchState (states.walking);
		((GuiScript)(GameObject.Find("GuiHelper").GetComponent("GuiScript"))).newTrial();
	}


		// the state machine
		public static void switchState (states newState)
		{
				switch (newState) {
				//start screen
				case states.startScreen:
						Time.timeScale = 0;
						GameObject.Find ("Character").SendMessage ("changeMovement", false);
						ManagerScript.state = states.startScreen;
						break;
				//questionaire
				case states.questionaire:
						Time.timeScale = 0;
						GameObject.Find ("Character").SendMessage ("changeMovement", false);
						ManagerScript.state = states.questionaire;
						Debug.Log ("questionaire");
						break;
				//walking
				case states.walking:
						Time.timeScale = 1;
						ManagerScript.state = states.walking;
						GameObject.Find ("Character").SendMessage ("changeMovement", true);
						((LookAtMeBlueBall)(GameObject.Find ("BlueBallGLow").GetComponent ("LookAtMeBlueBall"))).newTrial ();
						break;
				//pause
				case states.pause:
						Time.timeScale = 0;
						ManagerScript.state = states.pause;
						GameObject.Find ("Character").SendMessage ("changeMovement", false);
						break;

				//pointing
				case states.pointing:
						Time.timeScale = 1;
						ManagerScript.state = states.pointing;
						GameObject.Find ("Character").SendMessage ("changeMovement", true);
			((PointingScript)(GameObject.Find("helperObject").GetComponent("PointingScript"))).NewPointing();
						Debug.Log ("pointing");
						break;

				
				}
		}


}