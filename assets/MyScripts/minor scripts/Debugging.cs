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
	public new StreamWriter debugging;

	string temp222;

	public string filePath2;
	// Use this for initialization
	void Start () {
	

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
			debugging.Close ();
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


