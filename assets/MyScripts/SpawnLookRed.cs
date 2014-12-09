using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SpawnLookRed : MonoBehaviour
{
		GameObject displaytext ;
		float random;
		Vector3 v, pos;
		Vector3 rayDirection;
		bool keyPressedToEarly = false;
		float rotationSpeed = 100f;
		float rotationSpeedEasy = 50f;
		float rotationSpeedHard = 500f;
		float transformationSpeed = 15f;
		//float transformationSpeedEasy = 15f;
		//float transformationSpeedHard = 25f;
		float distanceToGoal = 10;
		float spawnDistance = 40f ;
		float spawnheight = 20f;
		float coolDown = 2.0f;       // How long to hide
		float showSphereAtTime = 0.0f; // timer, than needs to reach CoolDown


	int missedHardBalls = 0 ;
	int missedEasyBalls = 0 ;
	int catchedHardBalls = 0 ;
	int catchedEasyBalls = 0 ;
	float EasyDelay = 0f;	
	float HardDealy = 0f;

	//stuff for vibrating
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

		private UnityRandom urand;
		private int timeTillExp = 1; // how long till explosion
		float defeatableTillTime;
		static float moveScale ;
	
		// the condition is saved here, comes from manager script
		float onsetOfDefeatAtTime;
		float durationOfResponsePeriod ;
		GameObject pController;
		Transform cameraTransform = null;
		GameObject pxController;
		OVRPlayerController xcontroller;

		enum yellowSphereStates
		{
				hidden,
				moving,
				defeatable,
				notDefeatedInTime,
		}
		yellowSphereStates s;

		void Awake ()
		{
				cameraTransform = GameObject.FindWithTag ("OVRcam").transform;
				displaytext = GameObject.Find ("Displaytext2");
		}

		void Start ()
		{	
				pxController = GameObject.Find ("OVRPlayerController");
				xcontroller = pxController.GetComponent<OVRPlayerController> ();
				xcontroller.GetMoveScaleMultiplier (ref moveScale);

				urand = new UnityRandom ((int)System.DateTime.Now.Ticks);	
				switchState (yellowSphereStates.hidden);
		}

		void Update ()
		{	
				if (ManagerScript.getState () == ManagerScript.states.walking && ManagerScript.CondtionTypeVariableInContainer != "Explain" && ManagerScript.CondtionTypeVariableInContainer != "Dummy" && ManagerScript.CondtionTypeVariableInContainer != "Training") {
						switch (s) {
						case yellowSphereStates.defeatable:
								if (Time.time > defeatableTillTime) {
										switchState (yellowSphereStates.notDefeatedInTime);
										break;
								}
								move ();
								if ((Input.GetKeyDown (KeyCode.G) || Input.GetButtonDown ("360controllerButtonB")) && !keyPressedToEarly) {
										switchState (yellowSphereStates.hidden);
										recordData.recordDataStressors ("D","");
										Pause.ChangeNumberOfYellowDefeted ();
										
									if (ManagerScript.CondtionTypeVariableInContainer == "Easy" ) {
										catchedEasyBalls ++ ;
									}
									
									else if (ManagerScript.CondtionTypeVariableInContainer == "Hard") {
										
										catchedHardBalls++ ;
									}		

								
										GenerateTimeWindowForResponce(); // lets randomise the time window to respind every time the ball is  defeaded


								}
								break;
						case yellowSphereStates.hidden:
								if (Input.GetKeyDown (KeyCode.G) || Input.GetButtonDown ("360controllerButtonB")) {
										keyPressedToEarly = true;
										Debug.Log ("shootkey pressed to early");
								}			
								if (Time.time > showSphereAtTime) {
										switchState (yellowSphereStates.moving);
								}
								break;
						case yellowSphereStates.moving:
								if (Input.GetKeyDown (KeyCode.G) || Input.GetButtonDown ("360controllerButtonB")) {
										keyPressedToEarly = true;
										Debug.Log ("shootkey pressed to early");
								}	
								if (Time.time > onsetOfDefeatAtTime) {
										switchState (yellowSphereStates.defeatable);
								}
								move ();
								break;
						case yellowSphereStates.notDefeatedInTime:
								move ();
								break;	
						}	
				} else {
						renderer.enabled = false;
						displaytext.GetComponent<TextMesh> ().text = "";
						CancelInvoke ("startExp");
				}		
		}

		void reset ()
		{
				renderer.enabled = false;
				CancelInvoke ("startExp"); 
				showSphereAtTime = Time.time + coolDown;
				keyPressedToEarly = false;
		}

	
		// this is the function that respawns the yellow sphere
		void MoveAndShow ()
		{	

				// here we get a rondom value for the jidder of the onset
				GenerateTimeOnsetOfDefeatTime ();

				//position yellow sphere
				random = (float)urand.Range (-10, 10, UnityRandom.Normalization.STDNORMAL, 0.1f);
				rayDirection = cameraTransform.TransformDirection (Vector3.forward);
				pos.x = (cameraTransform.position.x + rayDirection.x * spawnDistance) + random;
				pos.z = (cameraTransform.position.z + rayDirection.z * spawnDistance) - random;
				pos.y = spawnheight;
				transform.position = pos;
			
				renderer.enabled = true;
				recordData.recordDataStressors ("S","");
				Pause.ChangeNumberOfYellowSpaw ();
		}


		// this jidders the onset between 0.8 and 2.5 seconds
		void GenerateTimeOnsetOfDefeatTime ()
		{
				onsetOfDefeatAtTime = (float)urand.Range (8, 25, UnityRandom.Normalization.STDNORMAL, 1.0f);
				onsetOfDefeatAtTime = onsetOfDefeatAtTime / 10 + Time.time;
		}
		void GenerateTimeWindowForResponce () {


			
		if (catchedEasyBalls > 15 ){
				
			EasyDelay = EasyDelay -35;
		}

		if (catchedHardBalls > 5) {
			HardDealy = HardDealy - 35 ;
		}

		if (missedEasyBalls > 5) {
			EasyDelay = EasyDelay + 40 ;
		}

		if (missedHardBalls > 6) {
			HardDealy = HardDealy + 35 ;
		}
				



			if (ManagerScript.CondtionTypeVariableInContainer == "Easy" || ManagerScript.CondtionTypeVariableInContainer == "Hard-False") {
				
			durationOfResponsePeriod = 0.600f + EasyDelay + (Random.Range (-100f, 100f)  ) / 1000;
				Debug.Log (durationOfResponsePeriod);
				rotationSpeed = rotationSpeedEasy;
			} else if (ManagerScript.CondtionTypeVariableInContainer == "Hard" || ManagerScript.CondtionTypeVariableInContainer == "Easy-False") {
			durationOfResponsePeriod = 0.350f + HardDealy + (Random.Range (-50f, 50f) ) / 1000;
				Debug.Log (durationOfResponsePeriod);
				
				rotationSpeed = rotationSpeedHard;
			}
		}

		void startExp ()
		{
				recordData.recordDataStressors ("M","");
				StartCoroutine (stunForSeconds (2));
				StartCoroutine (vibrateController ());
				((Detonator)(this.GetComponent ("Detonator"))).Explode ();
				switchState (yellowSphereStates.hidden);
		}

		IEnumerator vibrateController ()
		{		
		Debug.Log ("vibrate controller");
				if (!playerIndexSet || !prevState.IsConnected) {
						for (int i = 0; i < 4; ++i) {
								PlayerIndex testPlayerIndex = (PlayerIndex)i;
								GamePadState testState = GamePad.GetState (testPlayerIndex);
								if (testState.IsConnected) {
										Debug.Log (string.Format ("GamePad found {0}", testPlayerIndex));
										playerIndex = testPlayerIndex;
										playerIndexSet = true;
								}
						}
				}
				prevState = state;
				state = GamePad.GetState (playerIndex);
		
				// Set vibration according to triggers
				//GamePad.SetVibration (playerIndex, state.Triggers.Left, state.Triggers.Right);
				GamePad.SetVibration (0, 1.0f, 1.0f);
				yield return new WaitForSeconds (0.2f);
				GamePad.SetVibration (0, 0.0f, 0.0f);
				yield return new WaitForSeconds (0.01f);
				GamePad.SetVibration (0, 1.0f, 1.0f);
				yield return new WaitForSeconds (0.2f);
				GamePad.SetVibration (0, 0.0f, 0.0f);
				yield return new WaitForSeconds (0.01f);
				GamePad.SetVibration (0, 1.0f, 1.0f);		
				yield return new WaitForSeconds (0.8f);
				GamePad.SetVibration (0, 0.0f, 0.0f);
	}
	
		IEnumerator stunForSeconds (int sec)
		{
				//pController = GameObject.Find ("OVRPlayerController");
				//OVRPlayerController controller = pxController.GetComponent<OVRPlayerController> ();
				xcontroller.SetMoveScaleMultiplier (0.0f);
				yield return new WaitForSeconds (sec);
				moveScale = moveScale * 0.5f;
				xcontroller.SetMoveScaleMultiplier (moveScale);
				float temp = 0.0f;
				xcontroller.GetMoveScaleMultiplier (ref temp);
				Debug.Log ("move scale value -->" + temp);
		}

		public void newTrial ()
		{
				moveScale = 3.0f;
				xcontroller.SetMoveScaleMultiplier (3.0f);
				Debug.Log (ManagerScript.CondtionTypeVariableInContainer);
				// set respawn time acording to condition
				
				GenerateTimeWindowForResponce ();

				switchState (yellowSphereStates.hidden);
		}

		private void move ()
		{
				v = cameraTransform.position;
				rayDirection = cameraTransform.TransformDirection (Vector3.forward);
				v.x = v.x + rayDirection.x * distanceToGoal + Mathf.Sin (Time.time) * 2;
				v.z = v.z + rayDirection.z * distanceToGoal + Mathf.Sin (Time.time) * 2;
				v.y = 7 + Mathf.Sin (Time.time) * 2;			
				transform.position = Vector3.MoveTowards (transform.position, v, (float)(transformationSpeed * Time.deltaTime));
				transform.Rotate (Vector3.right * Time.deltaTime * rotationSpeed);
		}

		void switchState (yellowSphereStates newState)
		{
				displaytext.GetComponent<TextMesh> ().text = "";
				Debug.Log (newState);
				switch (newState) {
				case yellowSphereStates.defeatable:
						
						displaytext.GetComponent<TextMesh> ().text = "SHOOT";
						s = yellowSphereStates.defeatable;
						defeatableTillTime = Time.time + durationOfResponsePeriod;
						recordData.recordDataStressors("Onset",durationOfResponsePeriod.ToString());
						Debug.Log ("DurationOfresponsePeriod:" + durationOfResponsePeriod);
						break;
				case yellowSphereStates.hidden:
						s = yellowSphereStates.hidden;
						reset ();
						break;
				case yellowSphereStates.moving:
						s = yellowSphereStates.moving;
						MoveAndShow ();
						break;
				case yellowSphereStates.notDefeatedInTime:
						Pause.ChangeNumberOfYellowMissed ();
						Invoke ("startExp", timeTillExp);
						s = yellowSphereStates.notDefeatedInTime;
					
						if (ManagerScript.CondtionTypeVariableInContainer == "Easy" ) {
						missedEasyBalls ++ ;
						}
					
						else if (ManagerScript.CondtionTypeVariableInContainer == "Hard") {
					
						missedHardBalls++ ;
						}		
						GenerateTimeWindowForResponce(); // lets randomise the time window to respind every time the ball is not defeaded
						


						break;	
				}
		}
		
		// this schould happen every time we switch the blokcs
		public void ResetBallsCounterForDynamicDifficulty () {

		missedHardBalls = 0 ;
		missedEasyBalls = 0 ;
		catchedHardBalls = 0 ;
		catchedEasyBalls = 0 ;

	
		}

		public void onDestroy ()
		{
				Debug.Log ("wtf");
		}
}


	
