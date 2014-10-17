using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SpawnLookRed : MonoBehaviour
{
		GameObject displaytext ;
	float random;
		Vector3 v;
		Ray r;
		Vector3 rayDirection;
		float rotationSpeed = 130f;
		float transformationSpeed = 15f;
		float distanceToGoal = 10;
//		bool spawning_red = true ; // a variable for controlling processes
		public float spawnDistance = 40f ;
		float spawnheight = 20f;
		public double CoolDown = 2.0;       // How long to hide
		public float timer_red = 0.0f; // timer, than needs to reach CoolDown
		public float TimerForLooking = 0.0f; // timer, than needs to reach CoolDownValue
		public int moveDistance = 5;   // How close can the character get
		//	public float speed = 5.0f ; // the speed of the sphere
		private UnityRandom urand;
		Vector3 pos;
		private int timeTillExp = 1; // how long till explosion
		private bool willExplode = false; // will the ball explode

		// for looking at check
//		float percentageOfScreenHeight = 0.20f;
//		private Rect centerRect;
	
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
		GameObject pController;
		public MonoBehaviour characterMotor;
		Transform cameraTransform = null;
		
		

	void Awake ()
	{
		cameraTransform = GameObject.FindWithTag ("OVRcam").transform;
	}
		// Use this for initialization
		void Start ()
		{	// a variable we use to put the position in
				pos = new Vector3 ();

				displaytext = GameObject.Find ("Displaytext2");
	
				CoolDown = 2.0;    
				timer_red = 0.0f; 
				TimerForLooking = 0.0f; 
				moveDistance = 5;  

				// importend for checking of looked at - code for normal camera
//				double ySize = Screen.height * percentageOfScreenHeight;
//				centerRect = new Rect ((float)(Screen.width / 2 - ySize / 2), (float)(Screen.height / 2 - ySize / 2), (float)(ySize), (float)(ySize));

				// is needed for later rendomly put it somewhere 
				//character = GameObject.Find ("Character").transform;
				character = GameObject.Find ("OVRPlayerController").transform;
				// first lets hide the ball
				renderer.enabled = false;

				//Random Generator intialization
				urand = new UnityRandom (213123);

				renderer.enabled = false;

				
				

		}

		// Update is called once per frame
		void Update ()
		{	
				CondtionTypeVariableInContainer = ManagerScript.CondtionTypeVariableInContainer;

				// if the state is walking, lets render the shit out of it. 
				if (ManagerScript.state == ManagerScript.states.walking && CondtionTypeVariableInContainer != "Explain" && CondtionTypeVariableInContainer != "Dummy" && CondtionTypeVariableInContainer != "Training") {

						// this part is responsable for wating some time and respawn the object
						if (!renderer.enabled) {
								//Debug.Log(CondtionTypeVariableInContainer);
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
										((Crosshairtesting2)(GameObject.Find ("OVRPlayerController").GetComponent ("Crosshairtesting2"))).SmallCrosshair ();
										//	Debug.Log ("bla");
										
								}
						}
		
						// if object visible move it towards the player =)
						if (renderer.enabled) {
								v = character.position;
								rayDirection = cameraTransform.TransformDirection (Vector3.forward);
								v.x = v.x + rayDirection.x * distanceToGoal + Mathf.Sin (Time.time) * 2;
								v.z = v.z + rayDirection.z * distanceToGoal + Mathf.Sin (Time.time) * 2;
								v.y = 7 + Mathf.Sin (Time.time) * 2;			
				
				
				
								transform.position = Vector3.MoveTowards (transform.position, v, (float)(transformationSpeed * Time.deltaTime));

								transform.Rotate (Vector3.right * Time.deltaTime * rotationSpeed);
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
								//((Crosshairtesting2)(GameObject.Find ("Character").GetComponent ("Crosshairtesting2"))).BigCrosshair ();
								//	((Crosshairtesting2)(GameObject.Find ("OVRPlayerController").GetComponent ("Crosshairtesting2"))).BigCrosshair ();
								displaytext.GetComponent<TextMesh> ().text = "SHOOT";

							

						}

						// if you miss the time to respande, you can not respand and the crosshair is small agaian
			
						if (TimerAfterSetOn > TimeToRespand) {
								CanBeDefeated = false;
								displaytext.GetComponent<TextMesh> ().text = " ";


								//  Crosshairtesting2)(GameObject.Find ("Character").GetComponent ("Crosshairtesting2"))).SmallCrosshair ();
								//  ((Crosshairtesting2)(GameObject.Find ("OVRPlayerController").GetComponent("Crosshairtesting2"))).SmallCrosshair ();

								// & renderer.enabled to prevent explosion when the ball is defeated, as it was still expoding.
								if (!willExplode && renderer.enabled) {
										willExplode = true;
										Invoke ("startExp", timeTillExp);
								}
						}	


						// if you can respond and press the key, the ball is defeaded
						if ((Input.GetKeyDown (KeyCode.G) || Input.GetButtonDown ("360controllerButtonB")) && CanBeDefeated) {
								renderer.enabled = false;
								recordData.recordDataStressors ("D");
								Pause.ChangeNumberOfYellowDefeted ();
//								spawning_red = true;
								MoveAndShow ();
						}


						// THATS SO FUCKING STUPID
						// if the object is to near the player , lets respawn the ball
//						if (Vector3.Distance (character.position, transform.position) < moveDistance) {
//								recordData.recordDataStressors ("M");
//								
//								//Debug.Log ("Stressor missed");
//								renderer.enabled = false;
//								Pause.ChangeNumberOfYellowMissed ();
//								MoveAndShow ();
//		
//						}

				} else {
						// if not in proper state just dissable the rendering and everything is fine
						renderer.enabled = false;
						//	Debug.Log("turn off render of ball") ;

				}		
		}

		// this is the function that respawns the yellow sphere
		void MoveAndShow ()
		{	
				CancelInvoke ("startExp");
				willExplode = false;

				// here we get a rondom value for the jidder of the onset
				GenerateTimeOnsetOfDefeatTime ();

				random = (float)urand.Range (-10, 10, UnityRandom.Normalization.STDNORMAL, 0.1f);
				rayDirection = cameraTransform.TransformDirection (Vector3.forward);
				pos.x = (character.position.x + rayDirection.x * spawnDistance) + random;
				pos.z = (character.position.z + rayDirection.z * spawnDistance) - random;
				pos.y = spawnheight;
				transform.position = pos;
			
				timer_red = 0.0f;
//				renderer.enabled = true;
		}


		// this jidders the onset between 0.8 and 2.5 seconds
		void GenerateTimeOnsetOfDefeatTime ()
		{
				TimeOnsetOfDefeatTime = (float)urand.Range (8, 25, UnityRandom.Normalization.STDNORMAL, 1.0f);
				TimeOnsetOfDefeatTime = TimeOnsetOfDefeatTime / 10;
		}

		void startExp ()
		{
				//Debug.Log ("explosion");
				StartCoroutine (stun ());
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

		IEnumerator stun ()
		{
				pController = GameObject.Find ("OVRPlayerController");
				OVRPlayerController controller = pController.GetComponent<OVRPlayerController> ();
				controller.SetMoveScaleMultiplier (0.0f);
				//--GameObject.Find ("Character").SendMessage ("changeMovement", false);
				yield return new WaitForSeconds (1);
				controller.SetMoveScaleMultiplier (3.0f);
				//--GameObject.Find ("Character").SendMessage ("changeMovement", true);
				//GameObject.Find ("Character").SendMessage ("changeMovement", true);
				//characterMotor.enabled = true;
				//character1.GetComponent<CharacterMotor>().enabled = true;
				//GameObject.Find ("Character").SendMessage ("changeMovement", true);
		}

	public void newTrial(){
	//	CanBeDefeated = false;
		renderer.enabled = false;
		timer_red = 0.0f;
	}
}

