#pragma strict

var angleBetween = 0.0;
var target : Transform;

function Start () {

}

function Update () {

	
		var targetDir = target.position - transform.position;
		angleBetween = Vector3.Angle (transform.forward, targetDir);
		Debug.Log(angleBetween);
	    

}