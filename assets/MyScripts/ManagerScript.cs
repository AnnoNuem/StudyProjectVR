using UnityEngine;
using System.Collections.Generic;

public class ManagerScript : MonoBehaviour {

	//public list of trials
	public static List<trialContainer> trialList = new List<trialContainer>();
	//static variable tracks what trial is in process
	public static int trialNumber = 0 ;

	//Trials and random variables will be generated here
	void Awake(){

		//adding trials to the list
		//Generate random values for conditions
		for (int i=0; i<5; i++) {
			trialContainer tempTrial = new trialContainer();
			tempTrial.lightColor = "red";
			trialList.Add (tempTrial);
		}
	}
	//
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
