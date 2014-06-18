using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ManagerScript : MonoBehaviour {

	//public list of trials
	public static List<trialContainer> trialList = new List<trialContainer>();
	//static variable tracks what trial is in process
	public static int trialNumber = 0 ;
		
	public static string trialFolder =  Application.dataPath+@"\Trial"+(System.DateTime.Now).ToString("MMM-ddd-d-HH-mm-ss-yyyy");

	public static bool trialINprocess = false;

	public static bool pointTaskINprocess = false;

	//Trials and random variables will be generated here
	void Awake(){

		trialINprocess = true;

		if (!Directory.Exists(ManagerScript.trialFolder)){
			Directory.CreateDirectory(ManagerScript.trialFolder);
		}

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

	public static void abortTrial (){
		trialNumber++;
		trialINprocess = false;
		CameraFade.StartAlphaFade (Color.black, false, 2f, 2f, () => {Application.LoadLevel (0); });
		}
}
