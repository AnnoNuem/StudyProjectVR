/*
 * This class holds the parameters and also randomizes parameters 
 * for different types of trials
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class trialContainer
{
		public int spawnDistance ;
		public double CoolDown ;       // How long to hide
		public float timer_red ; // timer, than needs to reach CoolDown
		public float TimerForLooking ; // timer, than needs to reach CoolDownValue
		public int moveDistance;   // How close can the character get
		public float speed ;
		public Color bColor;

		public trialContainer ()
		{
		}

		public trialContainer (string trialType)
		{
				Camera.main.clearFlags = CameraClearFlags.SolidColor;

				if (trialType == "Easy") {

						//Applying parameters
						bColor = Color.green;
						ManagerScript.spawnDistance = 25;
						ManagerScript.CoolDown = 2.0;    
						ManagerScript.timer_red = 0.0f; 
						ManagerScript.TimerForLooking = 0.0f; 
						ManagerScript.moveDistance = 5;  
						ManagerScript.speed = 5.0f; 

						//Placing the value in container
						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;
						
		
				} else if (trialType == "Hard") {
						bColor = Color.red;
						ManagerScript.spawnDistance = 20;
						ManagerScript.CoolDown = 2.5;    
						ManagerScript.timer_red = 0.0f; 
						ManagerScript.TimerForLooking = 0.0f; 
						ManagerScript.moveDistance = 5;  
						ManagerScript.speed = 7.0f; 

						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;
						
				} else if (trialType == "Easy-False") {
						ManagerScript.spawnDistance = 25;
						ManagerScript.CoolDown = 2.0;    
						ManagerScript.timer_red = 0.0f; 
						ManagerScript.TimerForLooking = 0.0f; 
						ManagerScript.moveDistance = 5;  
						ManagerScript.speed = 5.0f; 

						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;

				} else if (trialType == "Hard-False") {
						ManagerScript.spawnDistance = 25;
						ManagerScript.CoolDown = 2.0;    
						ManagerScript.timer_red = 0.0f; 
						ManagerScript.TimerForLooking = 0.0f; 
						ManagerScript.moveDistance = 5;  
						ManagerScript.speed = 5.0f; 

						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;
				} else {
						
						// intro trials , no stressors
						ManagerScript.spawnDistance = 30;
						ManagerScript.CoolDown = 2.0;    
						ManagerScript.timer_red = 0.0f; 
						ManagerScript.TimerForLooking = 0.0f; 
						ManagerScript.moveDistance = 5;  
						ManagerScript.speed = 5.0f; 

						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;
				}
		}
}
