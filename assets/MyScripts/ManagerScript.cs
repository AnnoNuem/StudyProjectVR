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
				blockover,
				pause,
				end}
		;

		// chiffre for identification, can be changed in start screen
		public static string chiffre = "";
		private static states state;

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
		static bool duplicatePresent = true;


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
				trialContainer endTrial = new trialContainer ("ENDTRIAL");

				if (session == 1) {						
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
						

						// this loop does not allow mismatch conditions to be generated consecutively
						while (duplicatePresent) {
								easyBlock1.Shuffle ();
								for (int i=0; i < easyBlock1.Count-1; i++) {
										if (easyBlock1 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock1 [i + 1].CondtionTypeVariableInContainer == "Easy-False") {
												duplicatePresent = true;
												break;
										} else {
												duplicatePresent = false;
										}
								}
						}
						//////
						
						//easyBlock1.Shuffle ();
						trialList.AddRange (easyBlock1);
						trialList.Add (blockTrial);
						duplicatePresent = true;
						
						/*
						for(int i=0; i <trialList.Count;i++){
							Debug.Log("Types -> "+trialList[i].CondtionTypeVariableInContainer);
						}
						*/
						

						List<trialContainer> hardBlock1 = new List<trialContainer> ();
						
						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								hardBlock1.Add (tempTrial);
						}
						
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Hard-False");
								hardBlock1.Add (tempTrial);
						}

						while (duplicatePresent) {
								hardBlock1.Shuffle ();
								for (int i=0; i < easyBlock1.Count-1; i++) {
										if (hardBlock1 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock1 [i + 1].CondtionTypeVariableInContainer == "Hard-False") {
												duplicatePresent = true;
												break;
										} else {
												duplicatePresent = false;
										}
								}
						}

						//hardBlock1.Shuffle (); // Shuffling function
						trialList.AddRange (hardBlock1);
						trialList.Add (endTrial);
				} else if (session == 2 || session == 3) {
				
						List<int> orderNumbers = new List<int> (){1,2,3,4};
						orderNumbers.Shuffle ();
						
						for (int u=0; u<orderNumbers.Count; u++) {
								switch (orderNumbers [u]) {

								case 1:
										Debug.Log ("E B 1");
										List<trialContainer> easyBlock1 = new List<trialContainer> ();

										for (int i=0; i<24; i++) { //20
												trialContainer tempTrial = new trialContainer ("Easy");
												easyBlock1.Add (tempTrial);
										}
								
										for (int i=0; i<6; i++) {
												trialContainer tempTrial = new trialContainer ("Easy-False");
												easyBlock1.Add (tempTrial);
										}
								
										while (duplicatePresent) {
												easyBlock1.Shuffle ();
												for (int i=0; i < easyBlock1.Count-1; i++) {
														if (easyBlock1 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock1 [i + 1].CondtionTypeVariableInContainer == "Easy-False") {
																duplicatePresent = true;
																break;
														} else {
																duplicatePresent = false;
														}
												}
										}
									
							
								//easyBlock1.Shuffle ();
										trialList.AddRange (easyBlock1);
										trialList.Add (blockTrial);
										duplicatePresent = true;
										break;

								case 2:
										Debug.Log ("H B 1");
										List<trialContainer> hardBlock1 = new List<trialContainer> ();
									
										for (int i=0; i<24; i++) { //20
												trialContainer tempTrial = new trialContainer ("Hard");
												hardBlock1.Add (tempTrial);
										}
									
										for (int i=0; i<6; i++) {
												trialContainer tempTrial = new trialContainer ("Hard-False");
												hardBlock1.Add (tempTrial);
										}

										while (duplicatePresent) {
												hardBlock1.Shuffle ();
												for (int i=0; i < hardBlock1.Count-1; i++) {
														if (hardBlock1 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock1 [i + 1].CondtionTypeVariableInContainer == "Hard-False") {
																duplicatePresent = true;
																break;
														} else {
																duplicatePresent = false;
														}
												}
										}
									//hardBlock1.Shuffle (); // Shuffling function
										trialList.AddRange (hardBlock1);
										trialList.Add (blockTrial);
										duplicatePresent = true;
										break;
						
								case 3:
										Debug.Log ("E B 2");
										List<trialContainer> easyBlock2 = new List<trialContainer> ();
								
										for (int i=0; i<24; i++) { //20
												trialContainer tempTrial = new trialContainer ("Easy");
												easyBlock2.Add (tempTrial);
										}
								
										for (int i=0; i<6; i++) {
												trialContainer tempTrial = new trialContainer ("Easy-False");
												easyBlock2.Add (tempTrial);
										}
								
										while (duplicatePresent) {
												easyBlock2.Shuffle ();
												for (int i=0; i < easyBlock2.Count-1; i++) {
														if (easyBlock2 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock2 [i + 1].CondtionTypeVariableInContainer == "Easy-False") {
																duplicatePresent = true;
																break;
														} else {
																duplicatePresent = false;
														}
												}
										}
								//easyBlock2.Shuffle ();
										trialList.AddRange (easyBlock2);
										trialList.Add (blockTrial);
										duplicatePresent = true;
										break;

								case 4:
										Debug.Log ("H B 2");
										List<trialContainer> hardBlock2 = new List<trialContainer> ();
								
										for (int i=0; i<24; i++) { //20
												trialContainer tempTrial = new trialContainer ("Hard");
												hardBlock2.Add (tempTrial);
										}
								
										for (int i=0; i<6; i++) {
												trialContainer tempTrial = new trialContainer ("Hard-False");
												hardBlock2.Add (tempTrial);
										}

										while (duplicatePresent) {
												hardBlock2.Shuffle ();
												for (int i=0; i < hardBlock2.Count-1; i++) {
														if (hardBlock2 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock2 [i + 1].CondtionTypeVariableInContainer == "Hard-False") {
																duplicatePresent = true;
																break;
														} else {
																duplicatePresent = false;
														}
												}
										}
								//hardBlock2.Shuffle (); // Shuffling function
										trialList.AddRange (hardBlock2);
										//trialList.Add (endTrial);
										duplicatePresent = true;
										break;
								}
						}
						trialList.Add (endTrial);
				} else if (session == 4) {
						trialList.Add (new trialContainer ("Training"));
						trialList.Add (endTrial);
				} else {


						for (int i=0; i<2; i++) { 
								trialContainer tempTrial = new trialContainer ("Explain");
								trialList.Add (tempTrial);
						}
			
						trialList.Add (blockTrial);

						List<trialContainer> easyBlock1 = new List<trialContainer> ();

						for (int i=0; i<2; i++) { //20
								trialContainer tempTrial = new trialContainer ("Easy");
								easyBlock1.Add (tempTrial);
						}
			
						for (int i=0; i<2; i++) {
								trialContainer tempTrial = new trialContainer ("Easy-False");
								easyBlock1.Add (tempTrial);
						}

						while (duplicatePresent) {
								easyBlock1.Shuffle ();
								for (int i=0; i < easyBlock1.Count-1; i++) {
										if (easyBlock1 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock1 [i + 1].CondtionTypeVariableInContainer == "Easy-False") {
												duplicatePresent = true;
												break;
										} else {
												duplicatePresent = false;
										}
								}
						}
						//easyBlock1.Shuffle ();
						trialList.AddRange (easyBlock1);
						trialList.Add (blockTrial);
						duplicatePresent = true;

						List<trialContainer> hardBlock1 = new List<trialContainer> ();
			
						for (int i=0; i<2; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								hardBlock1.Add (tempTrial);
						}
			
						for (int i=0; i<2; i++) {
								trialContainer tempTrial = new trialContainer ("Hard-False");
								hardBlock1.Add (tempTrial);
						}

						while (duplicatePresent) {
								hardBlock1.Shuffle ();
								for (int i=0; i < easyBlock1.Count-1; i++) {
										if (hardBlock1 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock1 [i + 1].CondtionTypeVariableInContainer == "Hard-False") {
												duplicatePresent = true;
												break;
										} else {
												duplicatePresent = false;
										}
								}
						}
						//hardBlock1.Shuffle (); // Shuffling function
						trialList.AddRange (hardBlock1);
						trialList.Add (blockTrial);
						duplicatePresent = true;
			
						List<trialContainer> easyBlock2 = new List<trialContainer> ();
			
						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Easy");
								easyBlock2.Add (tempTrial);
						}
			
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Easy-False");
								easyBlock2.Add (tempTrial);
						}

						while (duplicatePresent) {
								easyBlock2.Shuffle ();
								for (int i=0; i < easyBlock1.Count-1; i++) {
										if (easyBlock2 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock2 [i + 1].CondtionTypeVariableInContainer == "Easy-False") {
												duplicatePresent = true;
												break;
										} else {
												duplicatePresent = false;
										}
								}
						}
						//easyBlock2.Shuffle ();
						trialList.AddRange (easyBlock2);
						trialList.Add (blockTrial);
						duplicatePresent = true;
			
						List<trialContainer> hardBlock2 = new List<trialContainer> ();
			
						for (int i=0; i<24; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								hardBlock2.Add (tempTrial);
						}
			
						for (int i=0; i<6; i++) {
								trialContainer tempTrial = new trialContainer ("Hard-False");
								hardBlock2.Add (tempTrial);
						}
						
						while (duplicatePresent) {
								hardBlock2.Shuffle ();
								for (int i=0; i < easyBlock1.Count-1; i++) {
										if (hardBlock2 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock2 [i + 1].CondtionTypeVariableInContainer == "Hard-False") {
												duplicatePresent = true;
												break;
										} else {
												duplicatePresent = false;
										}
								}
						}
						//hardBlock2.Shuffle (); // Shuffling function
						trialList.AddRange (hardBlock2);
						trialList.Add (endTrial);
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
				((PointingScript)(GameObject.Find ("helperObject").GetComponent ("PointingScript"))).CancelInvoke ("toLongPoint");
				Time.timeScale = 1;
		
				timetoPointingStage = 0.0f;
				pointingTime = 0.0f;
				//CameraFade.StartAlphaFade (Color.black, false, 2f, 0f);
				new    WaitForSeconds (2);



				//accessng parameters values according to the current trial
				spawnDistance = trialList [trialNumber].spawnDistance;
				CoolDown = trialList [trialNumber].CoolDown; //LEARN TO COPYPASTE YOU FUCKTARD!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! there was .spwanDistance here and not CoolDown
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


//				((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).newTrial ();
				if (trialList [trialNumber].CondtionTypeVariableInContainer == "BLOCKOVER") {
						Pause.PauseBetweenStates (trialList [trialNumber + 1].CondtionTypeVariableInContainer);
						switchState (states.blockover);
				} else if (trialList [trialNumber].CondtionTypeVariableInContainer == "ENDTRIAL") {
						switchState (states.end);				
				} else {
						switchState (states.walking);
				}
				trialNumber++;
		}
	
	
		// the state machine
		public static void switchState (states newState)
		{				
				unStun ();
				switch (newState) {
				//start screen
				case states.startScreen:
						Time.timeScale = 0;
						ManagerScript.state = states.startScreen;
						break;
				//questionaire
				case states.questionaire:
						Time.timeScale = 0;
						ManagerScript.state = states.questionaire;
						break;
				//walking
				case states.walking:
						
						Time.timeScale = 1;
			
				// here goes the code for the subject position reset and rotation reset to the starting point 						
						GameObject.Find ("OVRPlayerController").transform.position = GameObject.Find ("StartPoint").transform.position;
						GameObject.Find ("OVRPlayerController").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;
						GameObject.FindWithTag ("OVRcam").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;
						GameObject.FindWithTag ("OVRcam").transform.position = GameObject.Find ("StartPoint").transform.position;


						ManagerScript.state = states.walking;
						((PlayerLookingAt)(GameObject.Find ("BlueBallGLow").GetComponent ("PlayerLookingAt"))).newTrial ();
						((SpawnLookRed)(GameObject.Find ("RedBallGlow").GetComponent ("SpawnLookRed"))).newTrial ();
						break;
						
				//pause
				case states.pause:
						Time.timeScale = 0;
						ManagerScript.state = states.pause;
						break;
			
				//pointing
				case states.pointing:
						Time.timeScale = 1;
						((PointingScript)(GameObject.Find ("helperObject").GetComponent ("PointingScript"))).NewPointing ();
						stun ();
						ManagerScript.state = states.pointing;
						break;
				
				//blockover
				case states.blockover:
						ManagerScript.state = states.blockover;
						break;
				case states.end:
						ManagerScript.state = states.end;
						break;
				}

		}
	
		void toLongPoint ()
		{
				ManagerScript.abortTrial ();
		}

		static void stun ()
		{
				GameObject pController = GameObject.Find ("OVRPlayerController");
				OVRPlayerController controller = pController.GetComponent<OVRPlayerController> ();
				controller.SetMoveScaleMultiplier (0.0f);
		}

		static void unStun ()
		{
				GameObject pController = GameObject.Find ("OVRPlayerController");
				OVRPlayerController controller = pController.GetComponent<OVRPlayerController> ();
				controller.SetMoveScaleMultiplier (3.0f);
		}

		public static states getState ()
		{
				return state;
		}


}