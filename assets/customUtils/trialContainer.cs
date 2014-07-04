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
						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;
						
		
				} else if (trialType == "Hard") {
						
						bColor = Color.red;
						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;
						
				} else if (trialType == "Easy-False") {
						
						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;

				} else if (trialType == "Hard-False") {

						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;
				} else {
						
						spawnDistance = ManagerScript.spawnDistance;
						CoolDown = ManagerScript.CoolDown;       // How long to hide
						timer_red = ManagerScript.timer_red; // timer, than needs to reach CoolDown
						TimerForLooking = ManagerScript.TimerForLooking; // timer, than needs to reach CoolDownValue
						moveDistance = ManagerScript.moveDistance;   // How close can the character get
						speed = ManagerScript.speed;
				}
		}
}
