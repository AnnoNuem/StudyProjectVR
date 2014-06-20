using UnityEngine;
using System.Collections; 

public class LookAtMeBlueBall : MonoBehaviour {

	Vector3 pos_blue;

	// script for arrow pointing
	public ArrowPointingScript pointingScript; 
	public GuiScript guiScript;

	// time a user has to reach the next blue sphere
	int timeToGetToBlueSphere = 8;

	// for looking at check
	double percentageOfScreenHeight = .30;
	private Rect centerRect;

	// lets force playber to look 1 second at the blue light
	private float timer = 0.0f;
	
	// variable to count how often the ball is looked at
	int HowOftenIsLookedAt  = 0 ;

	// omportend for respawning
	double hideTime = 0.5;       // How long to hide
	double spawnDistance = 40.0; // How far away to spawn
	double moveDistance = 15.0;   // How close can the character get
	private Transform character; // this will be the variable we can acess players position
	private bool hiding = false; // for inner logic
	private UnityRandom urand;


	int HowHighObjectRespawns = 4;  // so the object will respawn on the same hight

	// this is for the spawn degree
	// cation, in genral works, is wrong. see explanation later
	bool left = false;
	float DegreeOfSpawn;

	// this is for understanding the pricipal i use for checking if looked at or not
//	void OnGUI () 
//	{		
//		double ySize = Screen.height*percentageOfScreenHeight;
//		GUI.Box(Rect(Screen.width/2 - ySize/2, Screen.height/2 - ySize/2, ySize, ySize), " ");
//	}


	// Use this for initialization
	void Start () 
	{

		// who shall not pass the blue light ?
		character = GameObject.Find("Character").transform;

		// this is our rectangle, in middle of creeen. It is the area that counts as our vision middle.
		// we check with this if the object is loocked at
		double ySize = Screen.height*percentageOfScreenHeight;
		centerRect = new Rect((float)(Screen.width/2 - ySize/2), (float)(Screen.height/2 - ySize/2), (float)ySize, (float)ySize);
				
		// This puts the blue sphere in the beginning of the game in the front of the playr 
		pos_blue.x = (float)character.position.x;
		pos_blue.y = (float)HowHighObjectRespawns;
		pos_blue.z = (float)(character.position.z + spawnDistance);
		transform.position = pos_blue ;

		// user reached blue sphere in time
		Invoke("toLong", timeToGetToBlueSphere);

		ArrowPointingScript pointingScript = (ArrowPointingScript) GameObject.Find("Arrow").GetComponent("ArrowPointingScript");

		urand = new UnityRandom(213123);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// check if is being looked at
		if (centerRect.Contains(Camera.main.WorldToScreenPoint(transform.position) )) 
		{
			timer += Time.deltaTime;   
		}
		else 
		{
			timer = 0.0f;
			//Debug.Log ("Looked at");
		}
		
		if (!hiding && Vector3.Distance(character.position, transform.position) < moveDistance) 
		{
			if ( timer > 1.0 ) 
			{
				HideAndMove();				
			}	
		}
	}

	void HideAndMove() 
	{
		HowOftenIsLookedAt++ ;
		if (HowOftenIsLookedAt != 2 ) 
		{
			// user reached blue sphere in time
			CancelInvoke("toLong");

			hiding = true;
			renderer.enabled = false;
			// spanning random at 30 60 90 degrees left or right
			switch(Random.Range(1,6))
			{
			case 1:
				left = false;
				//DegreeOfSpawn = 90;
				DegreeOfSpawn = urand.Range(75,105,UnityRandom.Normalization.STDNORMAL, 1.0f);
				break;
			case 2:
				left = false;
				//DegreeOfSpawn = 60 ;
				DegreeOfSpawn = urand.Range(45,75,UnityRandom.Normalization.STDNORMAL, 1.0f);
				break;
			case 3:
				left = false;
				//DegreeOfSpawn = 30 ;
				DegreeOfSpawn = urand.Range(15,45,UnityRandom.Normalization.STDNORMAL, 1.0f);
				break;
			case 4:
				left = true;
				DegreeOfSpawn = urand.Range(75,105,UnityRandom.Normalization.STDNORMAL, 1.0f);
				break;
			case 5:
				left = true;
				DegreeOfSpawn = urand.Range(45,75,UnityRandom.Normalization.STDNORMAL, 1.0f);
				break;
			case 6:
				left = true;
				DegreeOfSpawn = urand.Range(15,45,UnityRandom.Normalization.STDNORMAL, 1.0f);
				break;
			}

			Debug.Log(DegreeOfSpawn);
			
			float spawnx = (float)(Mathf.Sin(DegreeOfSpawn) * spawnDistance); 
			float spawnz = (float)(Mathf.Cos(DegreeOfSpawn) * spawnDistance); 
			if (left) 
			{
				pos_blue.x = (float)(character.position.x - spawnx);
				pos_blue.z = (float)(character.position.z + spawnz);
				((ArrowPointingScript) (GameObject.Find("Arrow").GetComponent("ArrowPointingScript"))).Point(Direction.left);
			}
			else
			{
				pos_blue.x = (float)(character.position.x + spawnx);
				pos_blue.z = (float)(character.position.z + spawnz);
				((ArrowPointingScript) (GameObject.Find("Arrow").GetComponent("ArrowPointingScript"))).Point(Direction.right);
			}

			transform.position = pos_blue ;
			
			//Creates a variable to check the objects position.
			// var myPosition = transform.position;
			//Prints the position to the Console.
			// Debug.Log(myPosition);
								
			renderer.enabled = true;


			// call function if user takes to long to get to blue sphere
			Invoke("toLong", timeToGetToBlueSphere);


			hiding = false;
;
		}

		
		// this is importend later, when we have a start menu and stuff. it worked, right now not used. 
		//    else {
		//			renderer.enabled = false;
		//			hiding = true;
		//    		yield WaitForSeconds(25);
		//			Application.LoadLevel("loadingMenu");
		//		}
	}

	void toLong(){
		ManagerScript.abortTrial ();
		Debug.Log("Blue Sphere not reached in time");
		((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).toSlow ();
	}

}