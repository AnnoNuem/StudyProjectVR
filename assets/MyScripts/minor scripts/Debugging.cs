using UnityEngine;
using System.Collections;
using URandom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

// this screen helps to debug the game lol ... use it wisly =)

public class Debugging : MonoBehaviour {

	public string filePath; 
	string temp222;
	private UnityRandom urand;

	string path1 = @"c:\temp\MyTestOfTrialRandamisationStandart.txt";
	string path2 = @"c:\temp\MyTestOfTrialRandamisationDice.txt";
	string path3 = @"c:\temp\MyTestOfTrialRandamisationSchuffleBag.txt";


	
	public string filePath2;
	// Use this for initialization
	void Start () {

//				urand = new UnityRandom ((int)System.DateTime.Now.Ticks);
//
//				string createText1 = "start" + Environment.NewLine;
//				string createText2 = "start" + Environment.NewLine;	
//				string createText3 = "start" + Environment.NewLine;
//
//				float[] shufflebag = {1,2,3,4,5,6,};
//		
//				ShuffleBagCollection<float> thebag = urand.ShuffleBag(shufflebag);
//		
//		float randvalue;
//
//		int myInt = 0;

//		while (myInt < 10000) {
//			randvalue = thebag.Next ();
//			string appendText3 = randvalue.ToString() + Environment.NewLine;
//
//			File.AppendAllText (path3, appendText3 );
//
//			myInt++;
//	
//		}
		//		File.AppendAllText (path1, createText1);
		//		File.AppendAllText (path2, createText2);
		//		File.AppendAllText (path3, createText3);


//
//				int myInt = 0;
//
//				while (myInt < 10000) {
//						string appendText1 = urand.Range (1, 6).ToString () + Environment.NewLine;
//						File.AppendAllText (path1, appendText1);
//		
//						myInt++;
//				}
//
//
//
//				int myInt2 = 0;
//
//			
//			
//				while (myInt2 < 10000) {
//						string appendText2 = urand.RollDice (1, DiceRoll.DiceType.D6) + Environment.NewLine;
//						File.AppendAllText (path2, appendText2);
//						myInt2++;
//				}
//
//				int myInt3 = 6;
//
//				float[]  shufflebag = {1,2,3,4,5,6};
//
//				ShuffleBagCollection<float> thebag = urand.ShuffleBag (shufflebag);
//
//		
//				int myInt31 = 0;
//
//				while (myInt31 < 1667){
//						myInt31	++;
//						while (myInt3 > 6) {
//			   			File.AppendAllText (path3, thebag.Next().ToString() );
//						myInt3-- ;
//
//						}
//				thebag=urand.ShuffleBag (shufflebag);
//				}



		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


//
//		if (ManagerScript.getState () == ManagerScript.states.walking) {
//			InvokeRepeating ("Blae", 0, 1f);
//
//		}
//
//		if (ManagerScript.getState () == ManagerScript.states.pause) {
//						ManagerScript.newTrial ();
//		}

		Transform target = GameObject.Find ("StartPoint").transform;
		Vector2 targetVector = new Vector2 (target.position.x, target.position.z); 
		Vector2 transformVector = new Vector2 (transform.position.x, transform.position.z);
		Vector2 forwardVector = new Vector2 (transform.forward.x, transform.forward.z);
		
		float angleBetween;
		
		Vector2 targetDir = targetVector - transformVector;
		
		//Old calculation which does not shows -ve pr +ve angles
		//angleBetween = Vector2.Angle (targetDir, forwardVector);
		angleBetween = Vector3.Angle(targetDir,forwardVector);
		Vector3 cross =  Vector3.Cross(targetDir,forwardVector);
		if (cross.z < 0) angleBetween = -angleBetween;

//		Debug.Log(angleBetween);


	}


	void Blae(){

		if (ManagerScript.getState () == ManagerScript.states.end){
			Application.Quit();
		}
		temp222 = ManagerScript.CondtionTypeVariableInContainer + "," + ManagerScript.trialNumber;
		File.AppendAllText ("Debugging.csv", temp222);
		ManagerScript.newTrial();

	}


//	void OnApplicationQuit() {
//		
//		debugging.Close ();
//	}

		}


