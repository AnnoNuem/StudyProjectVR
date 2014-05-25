using UnityEngine;
using System.Collections;

public class SpawnLookRed : MonoBehaviour {

	bool spawning_red = true ; // a variable for controlling processes
	int spawnDistance = 25 ;
	double CoolDown = 2.0;       // How long to hide
	float timer_red = 0.0f; // timer, than needs to reach CoolDown
	float TimerForLooking = 0.0f; // timer, than needs to reach CoolDownValue
	int moveDistance = 5;   // How close can the character get
	float speed = 5.0f ; // the speed of the sphere


	Vector3 pos;

	// for looking at check
	float percentageOfScreenHeight = 0.30f;
	private Rect centerRect;
	
	// to acces the coordinates of the player so we can move the ball towards him
	private Transform character;

	// Use this for initialization
	void Start () 
	{
		pos = new Vector3 ();
		// importend for checking of looked at - code for normal camera
		double ySize = Screen.height*percentageOfScreenHeight;
		centerRect = new Rect((float)(Screen.width/2 - ySize/2), (float)(Screen.height/2 - ySize/2), (float)(ySize), (float)(ySize));

		// is needed for later rendomly put it somewhere 
		character = GameObject.Find("Character").transform;
		
		// first lets hide the ball
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// this code is fo stoping the ball from respawning when the subject needs to point. does somehow not work
		//	if (transform.Find("BlueBallGLow").GetComponent(LookAtMeBlueNew).HowOftenIsLookedAt == 2 ) {
		//		spawning_red = false ;
		//		renderer.enabled = false;
		//		}
		
		
		// this part is responsable for wating some time and respawn the object
		if (spawning_red) 
		{ 
			timer_red += Time.deltaTime;
			//	Debug.Log (timer_red) ;
			if (timer_red > 3.5)
			{ 
				spawning_red = false;
				MoveAndShow();
			}
		}
		
		// if object visible move it towards the player =)
		if (renderer.enabled) 
		{
			transform.position = Vector3.MoveTowards(transform.position, character.position, (float)(speed*Time.deltaTime));
		}
		
		// here comes code wich will look if the object is looked at or crosses distance to player to near and in both cases 
		// make the object not being rendered, as well as settign spawning_red to true
		
		// check if is being looked at
		if (centerRect.Contains(Camera.main.WorldToScreenPoint(transform.position)) && renderer.enabled ) 
		{
			TimerForLooking += Time.deltaTime;   
		}
		else 
		{
			TimerForLooking = 0.0f ;
		}
		
		if (TimerForLooking > 0.5) 
		{
			renderer.enabled = false;
			spawning_red = true ;
		}
		
		// if the object is to near the player , lets respawn the ball
		if (Vector3.Distance(character.position, transform.position) < moveDistance) {
			renderer.enabled = false;
			spawning_red = true ;
		}
		

	}

	// this is the function that respawns the red sphere
	void MoveAndShow() 
	{
		// this is needed to put something in the left or right upper corner of the field of view
		switch(Random.Range(1,3))
		{
		case(1):
			pos.x = 0.9f;
			pos.y = 0.9f;
			pos.z = (float)spawnDistance;
			break;
		case(2):
			pos.x = 0.1f;
			pos.y = 0.9f;
			pos.z = (float)spawnDistance;
			break;	
		}
		
		// this does the magic to put it in the left or right upper corner 
		pos = Camera.main.ViewportToWorldPoint(pos);
		transform.position = pos; 
		renderer.enabled = true;
		timer_red = 0.0f ;
	}
}
