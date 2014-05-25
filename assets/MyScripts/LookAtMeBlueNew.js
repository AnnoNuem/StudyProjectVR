#pragma strict

// for looking at check
var percentageOfScreenHeight = .30;
private var centerRect : Rect;

// lets force playber to look 1 second at the blue light
private var timer : float = 0.0;

// variable to count how often the ball is looked at
var HowOftenIsLookedAt :int = 0 ;

// omportend for respawning
var hideTime = 0.5;       // How long to hide
var spawnDistance = 50.0; // How far away to spawn
var moveDistance = 15.0;   // How close can the character get
private var character : Transform; // this will be the variable we can acess players position
private var hiding = false; // for inner logic

var HowHighObjectRespawns : int = 4;  // so the object will respawn on the same hight



// this is for accessing the array from the script Test Array attached to the Character.
var arrayTest : ArrayTest ; // this is very importend !



// this is for the spawn degree
// cation, in genral works, is wrong. see explanation later
var left : boolean = false ;
var DegreeOfSpawn : int ;


// this is for understanding the pricipal i use for checking if looked at or not
function OnGUI () {

var ySize = Screen.height*percentageOfScreenHeight;

 GUI.Box(Rect(Screen.width/2 - ySize/2, Screen.height/2 - ySize/2, ySize, ySize), " ");


}

function Start () {

	// who shall not pass the blue light ?
	character = GameObject.Find("Character").transform;
	

	// this is our rectangle, in middle of creeen. It is the area that counts as our vision middle.
	// we check with this if the object is loocked at
	var ySize = Screen.height*percentageOfScreenHeight;
    centerRect = Rect(Screen.width/2 - ySize/2, Screen.height/2 - ySize/2, ySize, ySize);

	// IMPOREND for getting the condition from the Test array script 
	arrayTest	= GameObject.Find("Character").GetComponent(ArrayTest) ;

	// This puts the blue sphere in th ebeginning of the game in the front of the playr 
	var pos_blue = Vector3(character.position.x, HowHighObjectRespawns , (character.position.z + spawnDistance));
	transform.position = pos_blue ;
}
 
function Update () {
	
	
	// check if is being looked at
    if (centerRect.Contains(Camera.main.WorldToScreenPoint(transform.position) )) {
    timer += Time.deltaTime;   
    }
    else {
    timer = 0.0 ;
    //Debug.Log ("Looked at");
    }
    
    if (!hiding && Vector3.Distance(character.position, transform.position) < moveDistance) {
       
        if ( timer > 1.0 ) {
        HideAndMove();
	
    	}	
    }
    
   
    
}

function HideAndMove() {
	
	HowOftenIsLookedAt++ ;

	if (HowOftenIsLookedAt != 2 ) {
	    hiding = true;
	    renderer.enabled = false;
	    
	    // here we will think about where to spawn.
	    
	    	    
	    if(arrayTest.GetVarCondtion() == 1){
      left = false;
      DegreeOfSpawn = 90 ;
     }
     else if(arrayTest.GetVarCondtion() == 2){
      left = false;
      DegreeOfSpawn = 60 ;
     }
     else if(arrayTest.GetVarCondtion() == 3){
      left = false;
      DegreeOfSpawn = 30 ;
     }
     else if(arrayTest.GetVarCondtion() == 4){
      left = true;
      DegreeOfSpawn = 30 ;
     }
     else if(arrayTest.GetVarCondtion() == 5){
      left = true;
      DegreeOfSpawn = 60 ;
     }
     else if(arrayTest.GetVarCondtion() == 6){
      left = true;
      DegreeOfSpawn = 90 ;
     }
	     
	   	var temp1 : float;
	   	var pos : Vector3 ;
	   	if (left) {
	   	temp1 = Mathf.Sin(DegreeOfSpawn) * spawnDistance ; 
	   	pos = Vector3((character.position.x - temp1  ) , HowHighObjectRespawns,  character.position.z + spawnDistance  ) ;
	   	
	   	}
	   	else if (!left) {
	   	temp1 = Mathf.Sin(DegreeOfSpawn) * spawnDistance ; 
	   	pos = Vector3((character.position.x + temp1  ) , HowHighObjectRespawns,  character.position.z + spawnDistance  ) ;
	   	}
	   
	   transform.position = pos ;
	   
	     //Creates a variable to check the objects position.
	    // var myPosition = transform.position;
	    //Prints the position to the Console.
	   // Debug.Log(myPosition);
	    
	    yield WaitForSeconds(hideTime);
	        

	    
	    renderer.enabled = true;
	    hiding = false;
        }

// this is importend later, when we have a start menu and stuff. it worked, right now not used. 
//    else {
//			renderer.enabled = false;
//			hiding = true;
//    		yield WaitForSeconds(25);
//			Application.LoadLevel("loadingMenu");
//		}
}

