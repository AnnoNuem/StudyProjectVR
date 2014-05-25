#pragma strict

//this script is a proof of concept
//right now it generates an array of 120 values
//they are rondomly from 1 to 6
//and code where the blue sphere should respawn.
//30,60,90 degree left or right 

// VERY IMPORTEND !!!
// THIS SCRIPT IS IN STANDART ASSETS
// so we can later access it from the c sharp recodring script.



// our array
var myNumbers = new int [ 120 ];
// the laufvariable
var Laufvariable123 : int = 0 ;
// we will pass this later to other scripts
var variableToPass  ;

function Start () {

// for loop for fillinf with numbers 

for( var x : int = 0; x < 120; x++ ) {

myNumbers[x] = Mathf.Abs(Random.Range(1,7));

variableToPass = myNumbers[Laufvariable123] ;

}


}

function Update () {

// here we go throw the array if k is pressed

if (Input.GetKeyDown ("k")) {
	Laufvariable123 = Laufvariable123 + 1 ;
	variableToPass = myNumbers[Laufvariable123] ; 
			

}

}

//should return value
function GetVarCondtion()
{
    return variableToPass;

}
