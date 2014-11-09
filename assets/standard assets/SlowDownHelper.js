#pragma strict

 var motor : CharacterMotor ;
 var tempSpeed1 : int;
 var tempAcceleration1 : float;
  
 function Start (){
  	
     motor = GameObject.Find("OVRPlayerController").GetComponent(CharacterMotor);
     tempSpeed1 = motor.movement.maxForwardSpeed;
     tempAcceleration1 = motor.movement.maxGroundAcceleration;
     
     
 }
  
 function SlowSpeed (){
  
     var temp = motor.movement.maxForwardSpeed - 5;
     motor.movement.maxForwardSpeed = temp ;
     motor.ChangemaxForwardSpeed(2);
     //motor.movement.maxForwardSpeed = 20 ;
     
  
 }

function SlowAcceleration (){
  
    var temp = motor.movement.maxGroundAcceleration - 10;
     motor.movement.maxGroundAcceleration = temp;
     //motor.movement.maxForwardSpeed = 20 ;
     
  
}
 
function RestoreSpeed (){
  
     motor.movement.maxGroundAcceleration = tempSpeed1;
     //motor.movement.maxForwardSpeed = 20 ; 
}
 
 
 function RestoreAcceleration (){
  
     motor.movement.maxGroundAcceleration = tempAcceleration1;
     //motor.movement.maxForwardSpeed = 20 ;
     
  
}