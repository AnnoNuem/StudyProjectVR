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

	string path = @"c:\temp\MyTestOfTrialRandamisation.txt";

	
	public string filePath2;
	// Use this for initialization
	void Start () {

		urand = new UnityRandom ((int)System.DateTime.Now.Ticks);

		string createText = "start" + Environment.NewLine;
	
		File.AppendAllText (path,  createText);

		int myInt = 0;

		while (myInt < 1000) {
			string appendText = urand.Range (1, 6).ToString() + Environment.NewLine;
			File.AppendAllText(path, appendText);
		
			myInt++;
		}

		
	}
	
	// Update is called once per frame
	void Update () {



		if (ManagerScript.getState () == ManagerScript.states.walking) {
			InvokeRepeating ("Blae", 0, 2f);

		}

		if (ManagerScript.getState () == ManagerScript.states.pause) {
						ManagerScript.newTrial ();
		}

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


