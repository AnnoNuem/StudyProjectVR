using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SpawnLookRed : MonoBehaviour
{

		bool spawning_red = true ; // a variable for controlling processes
		public int spawnDistance = 25 ;
		public double CoolDown = 2.0;       // How long to hide
		public float timer_red = 0.0f; // timer, than needs to reach CoolDown
		public float TimerForLooking = 0.0f; // timer, than needs to reach CoolDownValue
		public int moveDistance = 5;   // How close can the character get
		public float speed = 5.0f ; // the speed of the sphere
		private UnityRandom urand;
		Vector3 pos;
		private int timeTillExp = 1; // how long till explosion
		private bool willExplode = false; // will the ball explode

		// for looking at check
		float percentageOfScreenHeight = 0.20f;
		private Rect centerRect;
	
		// to acces the coordinates of the player so we can move the ball towards him
		private Transform character;

		// the condition is saved here, comes from manager script
		public string CondtionTypeVariableInContainer;
		public float TimeOnsetOfDefeatTime;
		float TimeToRespand ;
		float TimerFromSpawn;
		float TimerAfterSetOn;
		bool CanBeDefeated;
		bool vibrate;

		/*
		bool playerIndexSet = false;
		PlayerIndex playerIndex;
		GamePadState state;
		GamePadState prevState;
*/

		// Use this for initialization
		void Start ()
		{	// a variable we use to put the position in
				pos = new Vector3 ();

				spawnDistance = 25;
				CoolDown = 2.0;    
				timer_red = 0.0f; 
				TimerForLooking = 0.0f; 
				moveDistance = 5;  
				speed = 5.0f; 

				// importend for checking of looked at - code for normal camera
				double ySize = Screen.height * percentageOfScreenHeight;
				centerRect = new Rect ((float)(Screen.width / 2 - ySize / 2), (float)(Screen.height / 2 - ySize / 2), (float)(ySize), (float)(ySize));

				// is needed for later rendomly put it somewhere 
				character = GameObject.Find ("Character").transform;
		
				// first lets hide the ball
				renderer.enabled = false;

				//Random Generator intialization
				urand = new UnityRandom (213123);

				renderer.enabled = false;

		}

		// Update is called once per frame
		void Update ()
		{	
		 

		
				//vibrateController ();

				/*
		// Find a PlayerIndex, for a single player game
		// Will find the first controller that is connected ans use it
		if (!playerIndexSet || !prevState.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}
		
		prevState = state;
		state = GamePad.GetState(playerIndex);	
*/
				CondtionTypeVariableInContainer = ManagerScript.CondtionTypeVariableInContainer;

				// if the state is walking, lets render the shit out of it. 
				if (ManagerScript.state == ManagerScript.states.walking && CondtionTypeVariableInContainer != "Explain" && CondtionTypeVariableInContainer != "Dummy" && CondtionTypeVariableInContainer != "Training") {

						// this part is responsable for wating some time and respawn the object
						if (!renderer.enabled) {
								
								timer_red += Time.deltaTime;
								if (timer_red > CoolDown) { 
										MoveAndShow ();
										renderer.enabled = true;
										recordData.recordDataStressors ("S");
										Pause.ChangeNumberOfYellowSpaw ();
										TimeToRespand = 0;
										TimerFromSpawn = 0;
										TimerAfterSetOn = 0;
										CanBeDefeated = false;
										((Crosshairtesting2)(GameObject.Find ("Character").GetComponent ("Crosshairtesting2"))).SmallCrosshair ();
										//	Debug.Log ("bla");
										
								}
						}
		
						// if object visible move it towards the player =)
						if (renderer.enabled) {
								transform.position = Vector3.MoveTowards (transform.position, character.position, (float)(speed * Time.deltaTime));
						}



						// here based on the condition the value how long the subnject can respond is established			


						if (CondtionTypeVariableInContainer == "Easy" || CondtionTypeVariableInContainer == "Hard-False") {
								TimeToRespand = 1.0f;
						} else if (CondtionTypeVariableInContainer == "Hard" || CondtionTypeVariableInContainer == "Easy-False") {
								TimeToRespand = 0.5f;
						}
			
						// so if the object sparns , we start to count how long it lives


						if (renderer.enabled) {
								TimerFromSpawn += Time.deltaTime;
						}

						// after the time established by TimeOnsetOfDefeatTime it can be defeated

						if (TimerFromSpawn > TimeOnsetOfDefeatTime) {
								CanBeDefeated = true;
						}
						
						// after the time established by TimeOnsetOfDefeatTime it can be defeated
						// but only for some time, so we count time from this point and make crosshair big

						if (CanBeDefeated) {
								TimerAfterSetOn += Time.deltaTime;
								((Crosshairtesting2)(GameObject.Find ("Character").GetComponent ("Crosshairtesting2"))).BigCrosshair ();

						}

						// if you miss the time to respande, you can not respand and the crosshair is small agaian
			
						if (TimerAfterSetOn > TimeToRespand) {
								CanBeDefeated = false;
								((Crosshairtesting2)(GameObject.Find ("Character").GetComponent ("Crosshairtesting2"))).SmallCrosshair ();
							

								// & renderer.enabled to prevent explosion when the ball is defeated, as it was still expoding.
								if (!willExplode && renderer.enabled) {
										willExplode = true;
										Invoke ("startExp", timeTillExp);
								}
						}	


						// if you can respond and press the key, the ball is defeaded
			if ((Input.GetKeyDown (KeyCode.G) || Input.GetButtonDown("360_controllerbuttonB")) && CanBeDefeated) {
								renderer.enabled = false;
								recordData.recordDataStressors ("D");
								Pause.ChangeNumberOfYellowDefeted ();
								spawning_red = true;
						}

						// if the object is to near the player , lets respawn the ball
						if (Vector3.Distance (character.position, transform.position) < moveDistance) {
								recordData.recordDataStressors ("M");
								//Debug.Log ("Stressor missed");
								renderer.enabled = false;
								Pause.ChangeNumberOfYellowMissed ();
								MoveAndShow ();
		
						}

				} else {
						// if not in proper state just dissable the rendering and everything is fine
						renderer.enabled = false;

				}		
		}

		// this is the function that respawns the yellow sphere
		void MoveAndShow ()
		{	
				willExplode = false;

				// here we get a rondom value for the jidder of the onset
				GenerateTimeOnsetOfDefeatTime ();



				float temp123 = (float)urand.Range (1, 9, UnityRandom.Normalization.STDNORMAL, 0.1f);
				pos.x = (temp123 / 10);
				pos.z = (float)spawnDistance;

				// this does the magic to put it in the left or right upper corner 
				pos = Camera.main.ViewportToWorldPoint (pos);
				//randomize the height of the spwan position of the orange sphere between 4 and 12

				//pos.y = Random.Range (4, 13);
				pos.y = 5;
				//apply new position
				transform.position = pos; 
				timer_red = 0.0f;

//Mapping values to stressors
				spawnDistance = ManagerScript.spawnDistance;
				//CoolDown = ManagerScript.CoolDown;    
				timer_red = ManagerScript.timer_red; 
				TimerForLooking = ManagerScript.TimerForLooking; 
				moveDistance = ManagerScript.moveDistance;  
				speed = ManagerScript.speed; 
		}


		// this jidders the onset between 0.8 and 2.5 seconds
		void GenerateTimeOnsetOfDefeatTime ()
		{
				TimeOnsetOfDefeatTime = (float)urand.Range (8, 25, UnityRandom.Normalization.STDNORMAL, 1.0f);
				TimeOnsetOfDefeatTime = TimeOnsetOfDefeatTime / 10;
				// this change the spawn distance, so the stressor tends to explode near the player
				spawnDistance = (int)(14 * TimeOnsetOfDefeatTime);
		}

		void startExp ()
		{

				Debug.Log ("explosion");
				StartCoroutine (vibrateController ());
				((Detonator)(this.GetComponent ("Detonator"))).Explode ();
				renderer.enabled = false;

				
		}

		IEnumerator vibrateController ()
		{
				GamePad.SetVibration (0, 0.5f, 0.5f);
				yield return new WaitForSeconds (1);
				GamePad.SetVibration (0, 0.0f, 0.0f);
		}


}


