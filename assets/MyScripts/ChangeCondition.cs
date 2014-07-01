using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 

public class ChangeCondition : MonoBehaviour {

	// right now changeable in the scene, from the gui. later we can hard code them i guess ?
	public Color RedColor ;
	public Color BlueColor ;
	public Color GreenColor ;


	public bool EasyConditioning = false;
	public bool HardConditioning = false;
	public bool FallsEasyConditioning = false;
	public bool FallsHardCondtioning = false;
	public bool TrainingCondition = false ;
	public bool NoConditioning = false;

	// here our array with conditions will be created, very dirty btw
	public List<int> arrayOfConditions; 

	// Easy
	int EasyspawnDistance = 25 ;
	double EasyCoolDown = 2.0;       // How long to hide
	float Easytimer_red = 0.0f; // timer, than needs to reach CoolDown
	float EasyTimerForLooking = 0.0f; // timer, than needs to reach CoolDownValue
	int EasymoveDistance = 5;   // How close can the character get
	float Easyspeed = 5.0f ; // the speed of the sphere

	// Hard
	int HardspawnDistance = 20 ;
	double HardCoolDown = 1.2 ;       // How long to hide
	float Hardtimer_red = 0.0f; // timer, than needs to reach CoolDown
	float HardTimerForLooking = 0.0f; // timer, than needs to reach CoolDownValue
	int HardmoveDistance = 5;   // How close can the character get
	float Hardspeed = 7.0f ; // the speed of the sphere

	// Training

	int TrainingspawnDistance = 30 ;
	double TrainingCoolDown = 2.0;       // How long to hide
	float Trainingtimer_red = 0.0f; // timer, than needs to reach CoolDown
	float TrainingTimerForLooking = 0.0f; // timer, than needs to reach CoolDownValue
	int TrainingmoveDistance = 5;   // How close can the character get
	float Trainingspeed = 4.0f ; // the speed of the sphere

	int LaufVariable = 0;

	void Start() {

		// proof of concept
		// here we will create a array of 40 values and decide which condition it is based on those

		// this one will be avoided, it is temporary

		// 1 = traoning
		// 2 = easy
		// 3 = hard
		// 4 = FalseEasy
		// 5 = False Hard
		
		// lets fill the first 10 values with training conditions
		for (int i = 1; i < 10; i++) {
			arrayOfConditions[i]=1; // training
		}
		
		for (int i = 11; i < 41; i++) {
			arrayOfConditions[i]=2; // condtioning easy
		}

		for (int i = 42; i < 43; i++) {
			arrayOfConditions[i] = 3; // conditioning hard
		}
		for (int i = 44; i < 48; i++) {
			arrayOfConditions[i] = 4;
		}
		for (int i = 49; i < 54; i++) {
			arrayOfConditions[i] = 5;
		}

	
	}

	public void  NextCondition() {

		Debug.Log (arrayOfConditions[LaufVariable]);

		LaufVariable = LaufVariable + 1;


		if (arrayOfConditions[LaufVariable] == 1) {
				TrainingCondition = true;
		} 
		else if (arrayOfConditions [LaufVariable] == 2) {
				EasyConditioning = true;
		} 
		else if (arrayOfConditions [LaufVariable] == 3) {
				HardConditioning = false;
		} 	
		else if (arrayOfConditions [LaufVariable] == 4) { 
				FallsEasyConditioning = true;
		} 
		else if (arrayOfConditions [LaufVariable] == 5) { 
				FallsHardCondtioning = true;
		}

	

		if (EasyConditioning ) 
		{
			camera.backgroundColor = GreenColor;
			SpawnLookRed.spawnDistance = EasyspawnDistance;
			SpawnLookRed.CoolDown = EasyCoolDown ;
			SpawnLookRed.timer_red = Easytimer_red ;
			SpawnLookRed.TimerForLooking = EasyTimerForLooking;
			SpawnLookRed.moveDistance = EasymoveDistance ;
			SpawnLookRed.speed = Easyspeed;
		} 
		else if (FallsEasyConditioning) 
		{
			camera.backgroundColor = GreenColor;
			SpawnLookRed.spawnDistance = HardspawnDistance;
			SpawnLookRed.CoolDown = HardCoolDown ;
			SpawnLookRed.timer_red = Hardtimer_red ;
			SpawnLookRed.TimerForLooking = HardTimerForLooking;
			SpawnLookRed.moveDistance = HardmoveDistance ;
			SpawnLookRed.speed = Hardspeed;
		}
		else if (HardConditioning) 
		{ 
			camera.backgroundColor = RedColor;
			SpawnLookRed.spawnDistance = HardspawnDistance;
			SpawnLookRed.CoolDown = HardCoolDown ;
			SpawnLookRed.timer_red = Hardtimer_red ;
			SpawnLookRed.TimerForLooking = HardTimerForLooking;
			SpawnLookRed.moveDistance = HardmoveDistance ;
			SpawnLookRed.speed = Hardspeed;
		} 
		else if (FallsHardCondtioning) 
		{	
			camera.backgroundColor = RedColor;
			SpawnLookRed.spawnDistance = EasyspawnDistance;
			SpawnLookRed.CoolDown = EasyCoolDown ;
			SpawnLookRed.timer_red = Easytimer_red ;
			SpawnLookRed.TimerForLooking = EasyTimerForLooking;
			SpawnLookRed.moveDistance = EasymoveDistance ;
			SpawnLookRed.speed = Easyspeed;
		}
		else if (NoConditioning) {
			camera.backgroundColor = BlueColor;
			SpawnLookRed.spawnDistance = TrainingspawnDistance;
			SpawnLookRed.CoolDown = TrainingCoolDown ;
			SpawnLookRed.timer_red = Trainingtimer_red ;
			SpawnLookRed.TimerForLooking = TrainingTimerForLooking;
			SpawnLookRed.moveDistance = TrainingmoveDistance ;
			SpawnLookRed.speed = Trainingspeed;
				
		}

		Debug.Log (arrayOfConditions[LaufVariable]);

	}
	
	// this is needed for the camera to be able to change the color
	void Example() {
		camera.clearFlags = CameraClearFlags.SolidColor;
	}
}