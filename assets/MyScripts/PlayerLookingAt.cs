using UnityEngine;
using System.Collections;

public class PlayerLookingAt : MonoBehaviour
{
Transform cameraTransform = null;
		Vector3 pos_blue;
		Vector3 pos_new;
//		Ray r;
		Vector3 rayDirection;
//	float rotationSpeed = 4f;

		int numberOfSpheres = 2;
		int numberOfReachedSpheres;
	
		// time a user has to reach the next blue sphere
		int timeToGetToBlueSphere = 20;
	
		// for looking at check
//		double percentageOfScreenHeight = .50;
		private Rect centerRect;
	
		// lets force playber to look 1 second at the blue light
		private float timer = 0.0f;
		GameObject displaytext;
		// variable to count how often the ball is looked at
		int HowOftenIsLookedAt = 0 ;
	
		// omportend for respawning
//		double hideTime = 0.5;       // How long to hide
		float spawnDistance = 40.0f; // How far away to spawn
		double moveDistance = 15.0;   // How close can the character get
//		private Transform character; // this will be the variable we can acess players position
		private bool hiding = false; // for inner logic
		private UnityRandom urand;
		int HowHighObjectRespawns = 4;  // so the object will respawn on the same hight

	
		// this is for the spawn degree
		// cation, in genral works, is wrong. see explanation later
		bool left = false;
		float DegreeOfSpawn;
		
		private Transform OVRCamD;
		void Awake ()
		{
				cameraTransform = GameObject.FindWithTag ("OVRcam").transform;
				displaytext = GameObject.Find("Displaytext");
		}

		void Start ()
		{
				renderer.enabled = false;
				urand = new UnityRandom ((int)System.DateTime.Now.Ticks);
		}
	
		void Update ()
		{
				//Debug.Log ("x-coordinate"+character.position.x);
				//Debug.Log ("y-yordinate"+character.position.y);

				float length = 10.0f;
				RaycastHit hit;
				rayDirection = cameraTransform.TransformDirection (Vector3.forward);
				Vector3 rayStart = cameraTransform.position + rayDirection;      // Start the ray away from the player to avoid hitting itself

				Debug.DrawRay (rayStart, rayDirection * length, Color.green);

				//transform.Rotate (Vector3.right * Time.deltaTime * rotationSpeed);
				

		if (ManagerScript.getState () == ManagerScript.states.walking || ManagerScript.getState () == ManagerScript.states.pointing) {
				
						if (Physics.Raycast (rayStart, rayDirection, out hit, length)) {
								if (hit.collider.tag == "blueball") {
										//Debug.Log ("Gazed at");
										timer += Time.deltaTime;  
								
								} 
						} else {
								timer = 0.0f;
						}
				
						if (!hiding && Vector3.Distance (cameraTransform.position, transform.position) < moveDistance) {
								if (timer > 0.5) {
										HideAndMove ();	
					//Debug.Log("Blu");
								}	
						}
				
				}

		}

		public void newTrial ()
		{
				OVRDevice.ResetOrientation();
				numberOfReachedSpheres = 0;

//				r = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
				rayDirection = cameraTransform.TransformDirection (Vector3.forward);
		pos_blue.x = cameraTransform.position.x + spawnDistance * rayDirection.x;
				pos_blue.y = (float)HowHighObjectRespawns;
		pos_blue.z = cameraTransform.position.z + spawnDistance * rayDirection.z;
				transform.position = pos_blue;

				renderer.enabled = true;
				
				hiding = false;
				timer = 0;
				numberOfReachedSpheres = 0;
				HowOftenIsLookedAt = 0;
				// user reached blue sphere in time
				Invoke ("toLong", timeToGetToBlueSphere);
		
		}

		void HideAndMove ()
		{
				HowOftenIsLookedAt++;
		
				if (HowOftenIsLookedAt != 2) {
						numberOfReachedSpheres++;
			
						// user reached blue sphere in time
						CancelInvoke ("toLong");
			
						hiding = true;
						renderer.enabled = false;
			
						// if we reached the number of spheres point back
						if (numberOfReachedSpheres == numberOfSpheres) {
								point ();
								return;
						}
			
						// spanning random at 30 60 90 degrees left or right
						switch (Random.Range (1, 6)) {
						// the jidder should be around 5 to 15 degree in total, so we dont have so many conditions
						// lets try it with 10 degree in total
						case 1:
								left = false;
				//DegreeOfSpawn = 90;
								DegreeOfSpawn = urand.Range (85, 95, UnityRandom.Normalization.STDNORMAL, 1.0f);
								break;
						case 2:
								left = false;
				//DegreeOfSpawn = 60 ;
								DegreeOfSpawn = urand.Range (55, 65, UnityRandom.Normalization.STDNORMAL, 1.0f);
								break;
						case 3:
								left = false;
				//DegreeOfSpawn = 30 ;
								DegreeOfSpawn = urand.Range (25, 35, UnityRandom.Normalization.STDNORMAL, 1.0f);
								break;
						case 4:
								left = true;
								DegreeOfSpawn = urand.Range (85, 95, UnityRandom.Normalization.STDNORMAL, 1.0f);
								break;
						case 5:
								left = true;
								DegreeOfSpawn = urand.Range (55, 8, UnityRandom.Normalization.STDNORMAL, 1.0f);
								break;
						case 6:
								left = true;
								DegreeOfSpawn = urand.Range (25, 35, UnityRandom.Normalization.STDNORMAL, 1.0f);
								break;
						}
			

						// here depending on the conditon, we rotate the spehre and move it forward
						if (left) {
				
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, (float)(360 - DegreeOfSpawn), transform.eulerAngles.z);
								transform.localPosition += transform.forward * (float)spawnDistance;	
								//((ArrowPointingScript)(GameObject.Find ("Arrow").GetComponent ("ArrowPointingScript"))).Point (Direction.left);
				displaytext.GetComponent<TextMesh>().text = "<--" ;
				Invoke("clearGUItext" , 0.5f) ;

						} else {
				
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, (float)(DegreeOfSpawn), transform.eulerAngles.z);
								transform.localPosition += transform.forward * (float)spawnDistance;
								//((ArrowPointingScript)(GameObject.Find ("Arrow").GetComponent ("ArrowPointingScript"))).Point (Direction.right);
				displaytext.GetComponent<TextMesh>().text = "-->" ;
				Invoke("clearGUItext" , 0.5f) ;

								}
			
			
						//Debug.Log (DegreeOfSpawn);
						renderer.enabled = true;

						// call function if user takes to long to get to blue sphere
						Invoke ("toLong", timeToGetToBlueSphere);

						hiding = false;
			
				}
		
				if (numberOfReachedSpheres == 1) {
						ManagerScript.generatedAngle = DegreeOfSpawn;
				}
		}
	
		void toLong ()
		{
				ManagerScript.abortTrial ();
				displaytext.GetComponent<TextMesh>().text = "Time's up for this trial!\nNew Trial";
				Invoke("clearGUItext" , 1f) ;
				//Debug.Log ("Blue Sphere not reached in time");
		//		((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).toSlow ();
		}
	
		void point ()
		{
				CancelInvoke ("toLong");
				//Debug.Log ("Point");
				ManagerScript.switchState (ManagerScript.states.pointing);
			//	((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).point ();
		}

		void clearGUItext(){ 		
			displaytext.GetComponent<TextMesh>().text = "" ;
		}
}