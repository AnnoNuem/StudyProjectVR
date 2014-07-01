using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 

public class ChangeCondition : MonoBehaviour {

	// right now changeable in the scene, from the gui. later we can hard code them i guess ?
	public Color RedColor ;
	public Color BlueColor ;
	public Color GreenColor = Color.green ;


	public bool EasyConditioning = false;
	public bool HardConditioning = false;
	public bool FallsEasyConditioning = false;
	public bool FallsHardCondtioning = false;
	public bool TrainingCondition = false ;
	public bool NoConditioning = false;

	// here our array with conditions will be created, very dirty btw
	static int[] values = new int[100] ;
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

	public int LaufVariable = 0;

	void Start() {

		// this is needed for the camera to be able to change the color

		camera.clearFlags = CameraClearFlags.SolidColor;

		// proof of concept
		// here we will create a array of 40 values and decide which condition it is based on those

		// this one will be avoided, it is temporary

		// 1 = traoning
		// 2 = easy
		// 3 = hard
		// 4 = FalseEasy
		// 5 = False Hard
		

	// lets fill the first 10 values with training conditions
		for (int i = 1; i < 2; i++) {
			values[i] = 1; // training
			Debug.Log("motherfucker");
			Debug.Log(values[i]);
		}
		
		for (int i = 3; i < 41; i++) {
			values[i] = 2; // condtioning easy
		}

		for (int i = 42; i < 83; i++) {
			values[i] = 3; // conditioning hard
		}
		for (int i = 84; i < 89; i++) {
			values[i] = 4;
		}
		for (int i = 90; i < 96; i++) {
			values[i] = 5;
		}

	
	}

	public void  NextCondition() {

		Debug.Log (values[LaufVariable]);
		Debug.Log ("this happens in nextcondition" + "this is" + LaufVariable);

		if (values[LaufVariable] == 1) {
				TrainingCondition = true;
		} 
		else if (values [LaufVariable] == 2) {
				EasyConditioning = true;
		} 
		else if (values [LaufVariable] == 3) {
				HardConditioning = false;
		} 	
		else if (values [LaufVariable] == 4) { 
				FallsEasyConditioning = true;
		} 
		else if (values [LaufVariable] == 5) { 
				FallsHardCondtioning = true;
		}

	

		if (EasyConditioning ) 
		{
			ChangeSkyColorToGreen();
			SpawnLookRed.spawnDistance = EasyspawnDistance;
			SpawnLookRed.CoolDown = EasyCoolDown ;
			SpawnLookRed.timer_red = Easytimer_red ;
			SpawnLookRed.TimerForLooking = EasyTimerForLooking;
			SpawnLookRed.moveDistance = EasymoveDistance ;
			SpawnLookRed.speed = Easyspeed;
		} 
		else if (FallsEasyConditioning) 
		{
			ChangeSkyColorToGreen();
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

		Debug.Log (values[LaufVariable]);

	}
	
	void ChangeSkyColorToGreen() {
		camera.backgroundColor = GreenColor;
	}

	public void ChangeLaufVariable(int b) {
	
		LaufVariable = b;
	}
}