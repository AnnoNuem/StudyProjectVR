using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class ManagerScript : MonoBehaviour
{
		// when should the questionaire be in the middle be
		int middleQuestionaire = 5;

		// how much time for pointing
		int timeForPointing = 8;


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
				Time.timeScale = 0;
				CameraFade.StartAlphaFade (Color.black, false, 2f, 0f);
				new		WaitForSeconds (2);
				Time.timeScale = 1;

				switchState (states.walking);
		}

		public static void newTrial ()
			{		
				trialNumber++;
				ChangeCondition CC = new ChangeCondition ();
				CC.ChangeLaufVariable(trialNumber);
				CC.NextCondition();
				trialINprocess = false;
				Time.timeScale = 0;
				CameraFade.StartAlphaFade (Color.black, false, 2f, 0f);
				new		WaitForSeconds (2);
				Time.timeScale = 1;
				switchState (states.walking);
				((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).newTrial ();
				((PointingScript)(GameObject.Find ("helperObject").GetComponent ("PointingScript"))).CancelInvoke ("toLongPoint");
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

						// here goes the code for the subject position reset and rotation reset to the starting point 
						GameObject.Find ("Character").transform.position = GameObject.Find ("StartPoint").transform.position;
						GameObject.Find ("Character").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;

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
						((PointingScript)(GameObject.Find ("helperObject").GetComponent ("PointingScript"))).NewPointing ();
						ManagerScript.state = states.pointing;
						GameObject.Find ("Character").SendMessage ("changeMovement", true);
						Debug.Log ("pointing");
						break;


				
				}


		}

		void toLongPoint ()
		{
				ManagerScript.abortTrial ();
				Debug.Log ("To long for pointing");
				((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).toSlowPoint ();
		}


}