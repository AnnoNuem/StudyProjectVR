using UnityEngine;
using System.Collections;

public class Pause : VRGUI
{
	
	private static KeyCode pausekey = KeyCode.P;
	private static ManagerScript.states prevState;
//	private Rect windowRect = new Rect (Screen.width / 2 - 320, Screen.height / 2 - 240, 640, 480);
//	private Rect labelRect = new Rect (170, 185, 300, 250);
	
	private static int NumberOfYellowSpaw = 0 ;
	private static int NumberOfYellowDefeted = 0;
	private static int NumberOfYellowMissed = 0;
	
	private static string CondtionTypeVariableInContainer;
	private static string tempVarForCondition;
	private static bool paused = false;
	
	private static string displayText = "" ;

	Transform cameraTransform = null;

	public GUISkin skin;

	void Awake (){
				cameraTransform = GameObject.FindWithTag ("OVRcam").transform;
		}
	
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		
		// evalute if pause key is pressed if yes switch state to pause or to state before pause
		if (Input.GetKeyDown (pausekey) || Input.GetButtonDown("360controllerButtonStart")) {
			if (paused && ManagerScript.getState() == ManagerScript.states.blockover) {
				ManagerScript.newTrial();
				paused = false;
			} else if (paused){
				ManagerScript.switchState (prevState);
				displayText = "";
				paused = false;
			} else if (!paused && ManagerScript.getState () != ManagerScript.states.startScreen) {
				paused = true;
				prevState = ManagerScript.getState ();
				ManagerScript.switchState (ManagerScript.states.pause);
			}
		}
		
	}
	
	public override void OnVRGUI()
	{
		
		GUI.skin = skin;
	//	guiPosition.x = cameraTransform.position.x + 10;
	//	guiPosition.z = cameraTransform.position.z + 10;
		
		// show pause screen
		if (paused) {
			//Debug.Log ("pausewindow");
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.BeginVertical ("box");
			GUILayout.Label ("<color=lime> PAUSE </color>");
			GUILayout.Label ("Press " + pausekey.ToString () + " to resume.\n");
			GUILayout.Label (displayText);
			GUILayout.Label (NumberOfYellowSpaw + " " + "Number of Yellow Spawn" );
			GUILayout.Label (NumberOfYellowDefeted + " " + "Number of Yellow defeated " );
			GUILayout.Label (NumberOfYellowMissed + " " + "Number of Yellow missed ");
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		} else {
			GUI.enabled = false ;		
		}
	}
	
	
	
	// the code to increase the numbers of yellow spheres defeated, spawn and missed
	
	public static void ChangeNumberOfYellowSpaw(){
		
		NumberOfYellowSpaw++ ;
	}
	
	public static void ChangeNumberOfYellowDefeted(){
		
		NumberOfYellowDefeted++ ;
	}
	
	public static void ChangeNumberOfYellowMissed(){
		
		NumberOfYellowMissed++ ;
	}
	
	public static void PauseBetweenStates (string NextBlockType){
		paused = true;
		if (NextBlockType.Contains("Easy")){
			displayText = "Block Complted.\nNext block of Trials is Easy.\n";
		} else {
			displayText = "Block Complted.\nNext block of Trials is Hard.\n";
		}

		prevState = ManagerScript.getState ();
		//Debug.Log ("pause");
	}
}
