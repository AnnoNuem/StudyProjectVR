using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class ManagerScript : MonoBehaviour
{
		//Public variables for stressors
		public static int spawnDistance ;
		public static double CoolDown ;       // How long to hide
		public static float timer_red ; // timer, than needs to reach CoolDown
		public static float TimerForLooking ; // timer, than needs to reach CoolDownValue
		public static int moveDistance;   // How close can the character get
		public static float speed ;
		public static string CondtionTypeVariableInContainer;
		public static Color bColor; //Background color

		//Public variables for blue target balls
		public static int numberOfSpheres;
		public static int timeToGetToBlueSphere; // time a user has to reach the next blue sphere
		public static double hideTime;       // How long to hide
		public static double blue_spawnDistance; // How far away to spawn
		public static double blue_moveDistance;   // How close can the character get


		//public static List<float> generatedAngles = new List<float> ();
		public static float generatedAngle;
		// when should the questionaire be in the middle be
//		int middleQuestionaire = 1000;
	
		// how much time for pointing
//		int timeForPointing = 8;
	
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

		// session identifier, tree different sessions
		public static int session;
	
		//public list of trials
		public static List<trialContainer> trialList = new List<trialContainer> ();
	
		//static variable tracks what trial is in process
		public static int trialNumber = 0 ;
	public static string trialFolder;
		public static string parameterFile = "";
		public static bool trialINprocess = false;
		public static bool pointTaskINprocess = false;
		public static float timetoPointingStage = 0.0f;
		public static float pointingTime = 0.0f;



		//Trials and random variables will be generated here
		void Awake ()
		{

		}
		//
		void Start ()
		{
		ManagerScript.switchState (states.startScreen);
		trialFolder = Application.dataPath + @"\Trial" + (System.DateTime.Now).ToString ("MMM-ddd-d-HH-mm-ss-yyyy");
		}

		// Update is called once per frame
		void Update ()
		{	

		//Debug.Log ("working manager == "+GameObject.Find ("OVRCameraController").transform.forward);
				//Debug.Log ("Trial number --->" + trialNumber);
				//Debug.Log ("state -->"+state);
				//Debug.Log ("time to point -->"+timetoPointingStage);
				//Debug.Log ("Current condition-->" + CondtionTypeVariableInContainer);
				if (state == states.walking) {
						timetoPointingStage += Time.deltaTime * 1; 
				}
				if (state == states.pointing) {
						pointingTime += Time.deltaTime * 1; 
				}

				

		}

		public static void generateTrials ()
		{
				trialContainer blockTrial = new trialContainer ("BLOCKOVER");

				if (session == 1) {
				
						//Debug.Log ("Session 1");

						

//						for (int i=0; i<1; i++) {
//								trialContainer tempTrial = new trialContainer ("Dummy");
//								trialList.Add (tempTrial);
//						}
						
						for (int i=0; i<5; i++) { 
								trialContainer tempTrial = new trialContainer ("Explain");
								trialList.Add (tempTrial);
						}
						
						trialList.Add (blockTrial);
							
						for (int i=0; i<40; i++) {
								trialContainer tempTrial = new trialContainer ("Training");
								trialList.Add (tempTrial);
						}

						trialList.Add (blockTrial);
				
						for (int i=0; i<2; i++) { 
								trialContainer tempTrial = new trialContainer ("Easy");
								trialList.Add (tempTrial);
						}

						trialList.Add (blockTrial);	
				
						for (int i=0; i<2; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								trialList.Add (tempTrial);
						}
						
						trialList.Add (blockTrial);

						for (int i=0; i<30; i++) { 
								trialContainer tempTrial = new trialContainer ("Easy");
								trialList.Add (tempTrial);
						}
						
						trialList.Add (blockTrial);
						
						for (int i=0; i<30; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								trialList.Add (tempTrial);
						}

						trialList.Add (blockTrial);

						List<trialContainer> easyBlock1 = new List<trialContainer> ();
						
						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Easy");
								easyBlock1.Add (tempTrial);
						}
						
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Easy-False");
								easyBlock1.Add (tempTrial);
						}
						easyBlock1.Shuffle ();
						trialList.AddRange (easyBlock1);
						trialList.Add (blockTrial);

						List<trialContainer> hardBlock1 = new List<trialContainer> ();
						
						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								hardBlock1.Add (tempTrial);
						}
						
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Hard-False");
								hardBlock1.Add (tempTrial);
						}
						hardBlock1.Shuffle (); // Shuffling function
						trialList.AddRange (hardBlock1);
						trialList.Add (blockTrial);
				} else if (session == 2) {
				
						//Debug.Log ("Session 2");
						List<trialContainer> easyBlock1 = new List<trialContainer> ();

						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Easy");
								easyBlock1.Add (tempTrial);
						}
						
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Easy-False");
								easyBlock1.Add (tempTrial);
						}
						easyBlock1.Shuffle ();
						trialList.AddRange (easyBlock1);
						trialList.Add (blockTrial);

						List<trialContainer> hardBlock1 = new List<trialContainer> ();
						
						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								hardBlock1.Add (tempTrial);
						}
						
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Hard-False");
								hardBlock1.Add (tempTrial);
						}
						hardBlock1.Shuffle (); // Shuffling function
						trialList.AddRange (hardBlock1);
						trialList.Add (blockTrial);
						
						List<trialContainer> easyBlock2 = new List<trialContainer> ();
						
						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Easy");
								easyBlock2.Add (tempTrial);
						}
						
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Easy-False");
								easyBlock2.Add (tempTrial);
						}
						easyBlock2.Shuffle ();
						trialList.AddRange (easyBlock2);
						trialList.Add (blockTrial);

						List<trialContainer> hardBlock2 = new List<trialContainer> ();
						
						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								hardBlock2.Add (tempTrial);
						}
						
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Hard-False");
								hardBlock2.Add (tempTrial);
						}
						hardBlock2.Shuffle (); // Shuffling function
						trialList.AddRange (hardBlock2);
						trialList.Add (blockTrial);

				} else {
				
		
			//Debug.Log ("Session 2");

			for (int i=0; i<5; i++) { 
				trialContainer tempTrial = new trialContainer ("Explain");
				trialList.Add (tempTrial);
			}
			
			trialList.Add (blockTrial);

			List<trialContainer> easyBlock1 = new List<trialContainer> ();

			for (int i=0; i<5; i++) { //20
				trialContainer tempTrial = new trialContainer ("Easy");
				easyBlock1.Add (tempTrial);
			}
			
			for (int i=0; i<5; i++) {
				trialContainer tempTrial = new trialContainer ("Easy-False");
				easyBlock1.Add (tempTrial);
			}
			easyBlock1.Shuffle ();
			trialList.AddRange (easyBlock1);
			trialList.Add (blockTrial);
			
			List<trialContainer> hardBlock1 = new List<trialContainer> ();
			
			for (int i=0; i<5; i++) { //20
				trialContainer tempTrial = new trialContainer ("Hard");
				hardBlock1.Add (tempTrial);
			}
			
			for (int i=0; i<5; i++) {
				trialContainer tempTrial = new trialContainer ("Hard-False");
				hardBlock1.Add (tempTrial);
			}
			hardBlock1.Shuffle (); // Shuffling function
			trialList.AddRange (hardBlock1);
			trialList.Add (blockTrial);
			
			List<trialContainer> easyBlock2 = new List<trialContainer> ();
			
			for (int i=0; i<24; i++) { //20
				trialContainer tempTrial = new trialContainer ("Easy");
				easyBlock2.Add (tempTrial);
			}
			
			for (int i=0; i<6; i++) {
				trialContainer tempTrial = new trialContainer ("Easy-False");
				easyBlock2.Add (tempTrial);
			}
			easyBlock2.Shuffle ();
			trialList.AddRange (easyBlock2);
			trialList.Add (blockTrial);
			
			List<trialContainer> hardBlock2 = new List<trialContainer> ();
			
			for (int i=0; i<24; i++) { //20
				trialContainer tempTrial = new trialContainer ("Hard");
				hardBlock2.Add (tempTrial);
			}
			
			for (int i=0; i<6; i++) {
				trialContainer tempTrial = new trialContainer ("Hard-False");
				hardBlock2.Add (tempTrial);
			}
			hardBlock2.Shuffle (); // Shuffling function
			trialList.AddRange (hardBlock2);
			trialList.Add (blockTrial);
						
		
		
				}
		}
	
		public static void abortTrial ()
		{	
				trialNumber++;
				trialINprocess = false;
				Time.timeScale = 0;
				CameraFade.StartAlphaFade (Color.black, false, 2f, 0f);
				new     WaitForSeconds (2);
				Time.timeScale = 1;
				switchState (states.walking);
		}
	
		public static void newTrial ()
		{  

				trialNumber++;
				//CameraFade.StartAlphaFade (Color.black, false, 2f, 0f);
				new    WaitForSeconds (2);
				//accessng parameters values according to the current trial
				spawnDistance = trialList [trialNumber].spawnDistance;
				CoolDown = trialList [trialNumber].spawnDistance;
				;       // How long to hide
				timer_red = trialList [trialNumber].timer_red; // timer, than needs to reach CoolDown
				TimerForLooking = trialList [trialNumber].TimerForLooking;  // timer, than needs to reach CoolDownValue
				moveDistance = trialList [trialNumber].moveDistance;
				;   // How close can the character get
				speed = trialList [trialNumber].speed;
//		Camera.main.backgroundColor = trialList[trialNumber].bColor;
				CondtionTypeVariableInContainer = trialList [trialNumber].CondtionTypeVariableInContainer;


				//OVRDevice.HMD.RecenterPose ();

				

		//O;	
				//OVRCameraController.increase();
				trialINprocess = true;
				Time.timeScale = 0;

				switchState (states.walking);
//				((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).newTrial ();
				((PointingScript)(GameObject.Find ("helperObject").GetComponent ("PointingScript"))).CancelInvoke ("toLongPoint");
				Time.timeScale = 1;

				timetoPointingStage = 0.0f;
				pointingTime = 0.0f;

		}
	
	
		// the state machine
		public static void switchState (states newState)
		{
				switch (newState) {
				//start screen
				case states.startScreen:
						Time.timeScale = 0;
						//--GameObject.Find ("OVRPlayerController").SendMessage ("changeMovement", false);
						ManagerScript.state = states.startScreen;
						break;
				//questionaire
				case states.questionaire:
						Time.timeScale = 0;
						//--GameObject.Find ("OVRPlayerController").SendMessage ("changeMovement", false);
						ManagerScript.state = states.questionaire;
			//Debug.Log ("questionaire");
						break;
				//walking
				case states.walking:
						Time.timeScale = 1;
			
			// here goes the code for the subject position reset and rotation reset to the starting point 
						
						//GameObject.Find ("Character").transform.position = GameObject.Find ("StartPoint").transform.position;
						//GameObject.Find ("Character").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;


			GameObject.Find ("OVRPlayerController").transform.position = GameObject.Find ("StartPoint").transform.position;
			GameObject.Find ("OVRPlayerController").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;
			GameObject.FindWithTag ("OVRcam").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;
			GameObject.FindWithTag ("OVRcam").transform.position = GameObject.Find ("StartPoint").transform.position;

			//Debug.Log("mapping occurs");

			//GameObject.Find ("ForwardDirection").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;

						ManagerScript.state = states.walking;
			//--GameObject.Find ("OVRPlayerController").SendMessage ("changeMovement", true);
						//((LookAtMeBlueBall)(GameObject.Find ("BlueBallGLow").GetComponent ("LookAtMeBlueBall"))).newTrial ();
						((PlayerLookingAt)(GameObject.Find ("BlueBallGLow").GetComponent ("PlayerLookingAt"))).newTrial ();
//						((SpawnLookRed)(GameObject.Find("RedBallGlow").GetComponent("SpawnLookRed"))).newTrial();
						break;
				//pause
				case states.pause:
						Time.timeScale = 0;
						ManagerScript.state = states.pause;
						//--GameObject.Find ("Character").SendMessage ("changeMovement", false);
						break;
			
				//pointing
				case states.pointing:
						Time.timeScale = 1;
						((PointingScript)(GameObject.Find ("helperObject").GetComponent ("PointingScript"))).NewPointing ();
						ManagerScript.state = states.pointing;
						//--GameObject.Find ("Character").SendMessage ("changeMovement", true);
			//Debug.Log ("pointing");
						break;
				}
		}
	
		void toLongPoint ()
		{
				ManagerScript.abortTrial ();
				//Debug.Log ("To long for pointing");
	//			((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).toSlowPoint ();
		}
}