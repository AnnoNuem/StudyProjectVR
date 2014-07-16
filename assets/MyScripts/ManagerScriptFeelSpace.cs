using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class ManagerScriptFeelSpace : MonoBehaviour
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
	int middleQuestionaire = 1000;
	
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
	public static string parameterFile =  "";
	public static bool trialINprocess = false;
	public static bool pointTaskINprocess = false;


	//Trials and random variables will be generated here
	void Awake ()
	{
		//adding trials to the list

		//First trial - not really used


		for (int i=0; i<80; i++) { 
			
			trialContainer tempTrial = new trialContainer ("Training");
			Debug.Log ("Array -->" + trialList); 
			trialList.Add (tempTrial);
		}



	}
	//
	void Start ()
	{
		ManagerScriptFeelSpace.switchState (states.startScreen);
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
		spawnDistance = trialList[trialNumber].spawnDistance;
		CoolDown =  trialList[trialNumber].spawnDistance; ;       // How long to hide
		timer_red = trialList[trialNumber].timer_red; // timer, than needs to reach CoolDown
		TimerForLooking = trialList[trialNumber].TimerForLooking;  // timer, than needs to reach CoolDownValue
		moveDistance = trialList[trialNumber].moveDistance;;   // How close can the character get
		speed = trialList[trialNumber].speed;
//		Camera.main.backgroundColor = trialList[trialNumber].bColor;
		CondtionTypeVariableInContainer = trialList [trialNumber].CondtionTypeVariableInContainer;


		trialINprocess = true;
		Time.timeScale = 0;

		switchState (states.walking);
		((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).newTrial ();
		((PointingScriptFeelSpace)(GameObject.Find ("helperObject").GetComponent ("PointingScriptFeelSpace"))).CancelInvoke ("toLongPoint");
		Time.timeScale = 1;

	}
	
	
	// the state machine
	public static void switchState (states newState)
	{
		switch (newState) {
			//start screen
		case states.startScreen:
			Time.timeScale = 0;
			GameObject.Find ("Character").SendMessage ("changeMovement", false);
			ManagerScriptFeelSpace.state = states.startScreen;
			break;
			//questionaire
		case states.questionaire:
			Time.timeScale = 0;
			GameObject.Find ("Character").SendMessage ("changeMovement", false);
			ManagerScriptFeelSpace.state = states.questionaire;
			Debug.Log ("questionaire");
			break;
			//walking
		case states.walking:
			Time.timeScale = 1;
			
			// here goes the code for the subject position reset and rotation reset to the starting point 
			GameObject.Find ("Character").transform.position = GameObject.Find ("StartPoint").transform.position;

			ManagerScriptFeelSpace.state = states.walking;
			GameObject.Find ("Character").SendMessage ("changeMovement", true);
			((LookAtMeBlueBallFeelSpace)(GameObject.Find ("BlueBallGLow").GetComponent ("LookAtMeBlueBallFeelSpace"))).newTrial ();
			break;
			//pause
		case states.pause:
			Time.timeScale = 0;
			ManagerScriptFeelSpace.state = states.pause;
			GameObject.Find ("Character").SendMessage ("changeMovement", false);
			break;
			
			//pointing
		case states.pointing:
			Time.timeScale = 1;
			((PointingScriptFeelSpace)(GameObject.Find ("helperObject").GetComponent ("PointingScriptFeelSpace"))).NewPointing ();
			ManagerScriptFeelSpace.state = states.pointing;
			GameObject.Find ("Character").SendMessage ("changeMovement", true);
			Debug.Log ("pointing");
			break;
		}
		
		
	}
	
	void toLongPoint ()
	{
		ManagerScriptFeelSpace.abortTrial ();
		Debug.Log ("To long for pointing");
		((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).toSlowPoint ();
	}
}