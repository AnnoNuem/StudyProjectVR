using UnityEngine;
using System.Collections;

public class Pause : VRGUI
{
	
	private KeyCode pausekey = KeyCode.P;
	private ManagerScript.states prevState;
	private Rect windowRect = new Rect (Screen.width / 2 - 320, Screen.height / 2 - 240, 640, 480);
	private Rect labelRect = new Rect (170, 185, 300, 250);
	
	static int NumberOfYellowSpaw = 0 ;
	static int NumberOfYellowDefeted = 0;
	static int NumberOfYellowMissed = 0;
	
	public string CondtionTypeVariableInContainer;
	public string tempVarForCondition;
	public bool BoolWhenToDisplayPause = false;
	
	public string dummyTextForLater = "i am empty" ;
	
	// Use this for initialization
	
	public GUISkin skin;
	
	
	void Start ()
	{
		
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		
		// evalute if pause key is pressed if yes switch state to pause or to state before pause
		if (Input.GetKeyDown (pausekey) || Input.GetButtonDown("360controllerButtonStart")) {
			if (ManagerScript.state == ManagerScript.states.pause) {
				ManagerScript.switchState (prevState);
				//Debug.Log ("resume");
			} else if (ManagerScript.state != ManagerScript.states.startScreen) {
				prevState = ManagerScript.state;
				ManagerScript.switchState (ManagerScript.states.pause);
				//Debug.Log ("pause");
			}
		}
		
		// before the first condition and after all blocks we need to display a text as well as the statistics in a paused game style
		
		CondtionTypeVariableInContainer = ManagerScript.CondtionTypeVariableInContainer;
		
		if (!BoolWhenToDisplayPause && CondtionTypeVariableInContainer != "BLOCKOVER") {
			tempVarForCondition = CondtionTypeVariableInContainer ;
			BoolWhenToDisplayPause = true;
			
		}
		
		
		if (BoolWhenToDisplayPause && CondtionTypeVariableInContainer == "BLOCKOVER") {
			// how often was BLOCKOVER displazed ?
			BoolWhenToDisplayPause = false;
			
			// we pause the game
			prevState = ManagerScript.state;
			ManagerScript.switchState (ManagerScript.states.pause);
			//Debug.Log ("pause");
			
			// we need to decide base on the previous state, what to do:
			
			
			
			
		}
		
	}
	
	public override void OnVRGUI()
	{
		
		GUI.skin = skin;
		
		// show pause screen
		if (ManagerScript.state == ManagerScript.states.pause) {
			//Debug.Log ("pausewindow");
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.BeginVertical ("box");
			GUILayout.Label ("<color=lime> PAUSE </color>");
			GUILayout.Label ("Press " + pausekey.ToString () + " to resume.");
			GUILayout.Label (dummyTextForLater);
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
}
