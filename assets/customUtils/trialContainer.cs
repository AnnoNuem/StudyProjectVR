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
						
						//Placing the value in container
						bColor = Color.green;
						spawnDistance = 25;
						CoolDown = 2.0;    
						timer_red = 0.0f; 
						TimerForLooking = 0.0f; 
						moveDistance = 5;  
						speed = 5.0f;
						
		
				} else if (trialType == "Hard") {
						
						bColor = Color.red;
						spawnDistance = 20;
						CoolDown = 2.5;    
						timer_red = 0.0f; 
						TimerForLooking = 0.0f; 
						moveDistance = 5;  
						speed = 7.0f;
						
						
				} else if (trialType == "Easy-False") {
						
						bColor = Color.red;
						spawnDistance = 20;
						CoolDown = 2.5;    
						timer_red = 0.0f; 
						TimerForLooking = 0.0f; 
						moveDistance = 5;  
						speed = 7.0f;
						

				} else if (trialType == "Hard-False") {

						bColor = Color.red;
						spawnDistance = 20;
						CoolDown = 2.5;    
						timer_red = 0.0f; 
						TimerForLooking = 0.0f; 
						moveDistance = 5;  
						speed = 7.0f;
						
				}
				if (trialType == "Training") {
					
					//Placing the value in container
					bColor = Color.black;
					spawnDistance = 30;
					CoolDown = 2.0;    
					timer_red = 0.0f; 
					TimerForLooking = 0.0f; 
					moveDistance = 5;  
					speed = 4.0f;		
		
				} else {
						
			bColor = Color.black;
						spawnDistance = 2220;
						CoolDown = 2.5;    
						timer_red = 0.0f; 
						TimerForLooking = 0.0f; 
						moveDistance = 5;  
						speed = 0.0f;
				}
		}
}
