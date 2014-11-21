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
		static float moveScale ;


		//public static List<float> generatedAngles = new List<float> ();
		public static float generatedAngle;

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
		
		public static int abortedTrials = 0;
		
	 	private GameObject helperObject ;
	
	public static bool temp123 = false;


		void Awake()
		{

		}
		//
		void Start ()
		{
				GameObject pController = GameObject.Find ("OVRPlayerController");
				OVRPlayerController controller = pController.GetComponent<OVRPlayerController> ();
				controller.GetMoveScaleMultiplier (ref moveScale);
				Debug.Log ("Value--->"+moveScale);
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
				trialContainer FirstScreenTrial = new trialContainer ("FIRSTSCREEN");

				if (session == 1) {		

						
						//trialList.Add (FirstScreenTrial);							


						for (int i=0; i<10; i++) { 
								trialContainer tempTrial = new trialContainer ("Explain");
								trialList.Add (tempTrial);
						}						
						trialList.Add (blockTrial);							
						for (int i=0; i<40; i++) {
								trialContainer tempTrial = new trialContainer ("Training");
								trialList.Add (tempTrial);
						}

						trialList.Add (blockTrial);
						// Explanation trials
						for (int i=0; i<5; i++) { 
								trialContainer tempTrial = new trialContainer ("Easy");
								trialList.Add (tempTrial);
						}

						trialList.Add (blockTrial);	
						// Explanation trials
						for (int i=0; i<5; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								trialList.Add (tempTrial);
						}
						
						trialList.Add (blockTrial);

						for (int i=0; i<45; i++) { 
								trialContainer tempTrial = new trialContainer ("Easy");
								trialList.Add (tempTrial);
						}
						
						trialList.Add (blockTrial);
						
						for (int i=0; i<45; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								trialList.Add (tempTrial);
						}

						trialList.Add (blockTrial);

						List<trialContainer> easyBlock1 = new List<trialContainer> ();
						
						for (int i=0; i<36; i++) { //20
								trialContainer tempTrial = new trialContainer ("Easy");
								easyBlock1.Add (tempTrial);
						}
						
						for (int i=0; i<9; i++) {
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
						
						for (int i=0; i<36; i++) { //20
								trialContainer tempTrial = new trialContainer ("Hard");
								hardBlock1.Add (tempTrial);
						}
						
						for (int i=0; i<9; i++) {
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



						for (int i=0; i<40; i++) {
								trialContainer tempTrial = new trialContainer ("Training");
								trialList.Add (tempTrial);
						}
						trialList.Add (blockTrial);	

						trialList.Add (endTrial);

				} else if (session == 2) { // || session == 3 right now gone, we will have 2 sessions



						// TEST THIS !!!

						for (int i=0; i<40; i++) { 
								trialContainer tempTrial = new trialContainer ("Training");
								trialList.Add (tempTrial);
						}

						trialList.Add (blockTrial);	
						
						// MAKE THE SHUFFLE THE RIGHT WAY 0101 1010
				
						List<int> orderNumbers = new List<int> (){1,2,3,4};
						orderNumbers.Shuffle ();
						
						for (int u=0; u<orderNumbers.Count; u++) {
								switch (orderNumbers [u]) {

								case 1:
										//Debug.Log ("E B 1");
										List<trialContainer> easyBlock1 = new List<trialContainer> ();

										for (int i=0; i<36; i++) { //20
												trialContainer tempTrial = new trialContainer ("Easy");
												easyBlock1.Add (tempTrial);
										}
								
										for (int i=0; i<9; i++) {
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
										//Debug.Log ("H B 1");
										List<trialContainer> hardBlock1 = new List<trialContainer> ();
									
										for (int i=0; i<36; i++) { //20
												trialContainer tempTrial = new trialContainer ("Hard");
												hardBlock1.Add (tempTrial);
										}
									
										for (int i=0; i<9; i++) {
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
										//Debug.Log ("E B 2");
										List<trialContainer> easyBlock2 = new List<trialContainer> ();
								
										for (int i=0; i<36; i++) { //20
												trialContainer tempTrial = new trialContainer ("Easy");
												easyBlock2.Add (tempTrial);
										}
								
										for (int i=0; i<9; i++) {
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
										//Debug.Log ("H B 2");
										List<trialContainer> hardBlock2 = new List<trialContainer> ();
								
										for (int i=0; i<36; i++) { //20
												trialContainer tempTrial = new trialContainer ("Hard");
												hardBlock2.Add (tempTrial);
										}
								
										for (int i=0; i<9; i++) {
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

					// this was missing, causing a bug
										trialList.Add (blockTrial);

					//trialList.Add (endTrial);
										duplicatePresent = true;
										break;
								}
						}
						


						trialList.Add (blockTrial);							
						for (int i=0; i<40; i++) {
								trialContainer tempTrial = new trialContainer ("Training");
								trialList.Add (tempTrial);
						}


						trialList.Add (blockTrial);	
						duplicatePresent = true;

						trialList.Add (endTrial);


		// this is a secrete trial order, for screenshots, testing and so ever. can be overriden, even in master. not for experiment reasons

		
		} else if (session == 666) {
				
		
			for (int i=0; i<2; i++) { 
				trialContainer tempTrial = new trialContainer ("Easy");
				trialList.Add (tempTrial);
			}
			
			trialList.Add (blockTrial);	
			// Explanation trials
			for (int i=0; i<2; i++) { //20
				trialContainer tempTrial = new trialContainer ("Hard");
				trialList.Add (tempTrial);
			}
		
		
		} 

		// DABATABLE TEST IT THINK ABOUT IT DISCUSS IT
		else { Application.Quit() ;}
				
		}
	
		public static void abortTrial ()
		{	
				
		// Without stun and unstun, the aboutTrial was repeating itself in the case, the move button was presssed. It is fixes like this

				stun ();

			//	trialNumber++;
				trialINprocess = false;
				Time.timeScale = 0;
				CameraFade.StartAlphaFade (Color.black, false, 2f, 0f);
				new     WaitForSeconds (2);
				Time.timeScale = 1;
				newTrial ();
			//	switchState (states.walking);
				abortedTrials++ ;

		temp123 = false;


				unStun ();
		}
	
		public static void newTrial ()
		{  
				if (ManagerScript.getState () != ManagerScript.states.end) {
			
						//GameObject pController = GameObject.Find ("OVRPlayerController");
						//OVRPlayerController controller = pController.GetComponent<OVRPlayerController> ();
						//controller.SetMoveScaleMultiplier (3.0f);
						((PointingScript)(GameObject.Find ("helperObject").GetComponent ("PointingScript"))).CancelInvoke ("toLongPoint");
						Time.timeScale = 1;

						timetoPointingStage = 0.0f;
						pointingTime = 0.0f;
						//CameraFade.StartAlphaFade (Color.black, false, 2f, 0f);
						new    WaitForSeconds (2);
				

						//accessng parameters values according to the current trial
						spawnDistance = trialList [trialNumber].spawnDistance;
						CoolDown = trialList [trialNumber].CoolDown; //LEARN TO COPYPASTE YOU FUCKTARD!!!!!!!!!!!! there was .spwanDistance here and not CoolDown
						// How long to hide
						timer_red = trialList [trialNumber].timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = trialList [trialNumber].TimerForLooking;  // timer, than needs to reach CoolDownValue
						moveDistance = trialList [trialNumber].moveDistance;
						;   // How close can the character get
						speed = trialList [trialNumber].speed;
						//		Camera.main.backgroundColor = trialList[trialNumber].bColor;
						CondtionTypeVariableInContainer = trialList [trialNumber].CondtionTypeVariableInContainer;


						//OVRDevice.HMD.RecenterPose ();

						//OVRCameraController.increase();
						trialINprocess = true;
						Time.timeScale = 0;

				}
						if (trialList [trialNumber].CondtionTypeVariableInContainer == "BLOCKOVER") {
								Pause.PauseBetweenStates (trialList [trialNumber + 1].CondtionTypeVariableInContainer);
								switchState (states.blockover);
						} else if (trialList [trialNumber].CondtionTypeVariableInContainer == "ENDTRIAL") {
						
								string temp1 = "EXPOVER";
								Pause.PauseBetweenStates (temp1);

								switchState (states.end);				
						} else {	
								switchState (states.walking);
						}
						trialNumber++;
				
		}
	
		// the state machine
		public static void switchState (states newState)
		{				
				//unStun ();
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
						

				// here goes the code for the subject position reset and rotation reset to the starting point 						
						GameObject.Find ("OVRPlayerController").transform.position = GameObject.Find ("StartPoint").transform.position;
						GameObject.Find ("OVRPlayerController").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;
						GameObject.FindWithTag ("OVRcam").transform.rotation = GameObject.Find ("StartPoint").transform.rotation;
						GameObject.FindWithTag ("OVRcam").transform.position = GameObject.Find ("StartPoint").transform.position;

						Time.timeScale = 1;


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
						Pause.SaveValues(trialList [trialNumber + 1].CondtionTypeVariableInContainer);
						break;
				case states.end:
						ManagerScript.state = states.end;
						//Pause.SaveValues(trialList [trialNumber].CondtionTypeVariableInContainer);
						break;
				}

		}
	
		void toLongPoint ()
		{
				ManagerScript.abortTrial ();
		}
		
		// this function stuns the player 
		static void stun ()
		{
				GameObject pController = GameObject.Find ("OVRPlayerController");
				OVRPlayerController controller = pController.GetComponent<OVRPlayerController> ();
				controller.SetMoveScaleMultiplier (0.0f);

		}

		static void unStun()
		{
				GameObject pController = GameObject.Find ("OVRPlayerController");
				OVRPlayerController controller = pController.GetComponent<OVRPlayerController> ();
				controller.SetMoveScaleMultiplier (3.0f);
		}

		public static states getState ()
		{
				return state;
		}
		
	public static void PauseInTheBeginning () {


		Pause.PauseBetweenStates (trialList [trialNumber + 1].CondtionTypeVariableInContainer);
		switchState (states.pause);


	}
}