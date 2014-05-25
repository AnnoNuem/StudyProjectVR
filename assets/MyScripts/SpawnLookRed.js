#pragma strict
var spawning_red : boolean = true ; // a variable for controlling processes
var spawnDistance : int = 25 ;
var CoolDown = 2.0;       // How long to hide
var timer_red : float = 0.0; // timer, than needs to reach CoolDown
var TimerForLooking : float = 0.0; // timer, than needs to reach CoolDownValue
var moveDistance = 5;   // How close can the character get
var speed : float = 5.0f ; // the speed of the sphere


// for looking at check
var percentageOfScreenHeight = .30;
private var centerRect : Rect;

// to acces the coordinates of the player so we can move the ball towards him
private var character : Transform ;


function Start () {
	
// importend for checking of looked at - code for normal camera
var ySize = Screen.height*percentageOfScreenHeight;
centerRect = Rect(Screen.width/2 - ySize/2, Screen.height/2 - ySize/2, ySize, ySize);


	// is needed for later rendomly put it somewhere 
	character = GameObject.Find("Character").transform;
	
	// first lets hide the ball
	renderer.enabled = false;
	
}

function Update () {

// this code is fo stoping the ball from respawning when the subject needs to point. does somehow not work

//	if (transform.Find("BlueBallGLow").GetComponent(LookAtMeBlueNew).HowOftenIsLookedAt == 2 ) {
//		spawning_red = false ;
//		renderer.enabled = false;
//		}
			
	
	// this part is responsable for wating some time and respawn the object
	if (spawning_red) { 
	 	timer_red += Time.deltaTime;
	 //	Debug.Log (timer_red) ;
	   if (timer_red > 3.5){ 
		     	spawning_red = false ;
	 	    	MoveAndShow() ;
	       	
	}
	}
	
	// if object visible move it towards the player =)
	if (renderer.enabled) {
		transform.position = Vector3.MoveTowards(transform.position, character.position, speed*Time.deltaTime);
	}

	// here comes code wich will look if the object is looked at or crosses distance to player to near and in both cases 
	// make the object not being rendered, as well as settign spawning_red to true

	// check if is being looked at
    if (centerRect.Contains(Camera.main.WorldToScreenPoint(transform.position)) && renderer.enabled ) {
    TimerForLooking += Time.deltaTime;   
    }
    else {
   	TimerForLooking = 0.0 ;
    }
    
    if (TimerForLooking > 0.5) {
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
function MoveAndShow() {
	// we pick random 1 or 2 
    var randomPickRed : int = Mathf.Abs(Random.Range(1,3));
    var pos : Vector3 ;
    // this is needed to put something in the left or right upper corner of the field of view
    if(randomPickRed == 1){
          pos = Vector3(0.9, 0.9, spawnDistance);
     }
     else if(randomPickRed == 2){
          pos = Vector3(0.1, 0.9, spawnDistance);

     }
     
    // this does the magic to put it in the left or right upper corner 
    pos = Camera.main.ViewportToWorldPoint(pos);
    transform.position = pos; 
    renderer.enabled = true;
    timer_red = 0.0 ;
}