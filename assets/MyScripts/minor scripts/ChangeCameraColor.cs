﻿using UnityEngine;
using System.Collections;

public class ChangeCameraColor : MonoBehaviour
{


		public static string CondtionTypeVariableInContainer;
		public static string CondtionTypeVariableInContainerOld;
		GameObject cam;
		ChangeRenderSettings cr;

		// Use this for initialization
		void Start ()
		{
				cam = GameObject.Find ("MainCamera");                                            
				cam.SetActive (false);	
				cr = (ChangeRenderSettings)GameObject.Find ("helperObject").GetComponent ("ChangeRenderSettings");
		}
	
		// Update is called once per frame
		void Update ()
		{
				CondtionTypeVariableInContainer = ManagerScript.CondtionTypeVariableInContainer;
				if (CondtionTypeVariableInContainer != CondtionTypeVariableInContainerOld) {
						ChangeSettings ();
						CondtionTypeVariableInContainerOld = CondtionTypeVariableInContainer;
				}


		}

		void ChangeSettings ()
		{
				if (CondtionTypeVariableInContainer == "Easy" || CondtionTypeVariableInContainer == "Easy-False") {
						//	Debug.Log("easy camera");
						cam.SetActive (true);	
						cr.switchEasy ();
				} else if (CondtionTypeVariableInContainer == "Hard" || CondtionTypeVariableInContainer == "Hard-False") {
						//Debug.Log("hard camera");
						cam.SetActive (true);	
						cr.switchHard ();
				} else if (CondtionTypeVariableInContainer == "Training" || CondtionTypeVariableInContainer == "Explain") {
						//	Debug.Log("no cond camera");	
						cam.SetActive (true);		
						cr.switchNormal ();
				} else if (CondtionTypeVariableInContainer == "ENDTRIAL") {
			
						//	Debug.Log("no camera");
						cam.SetActive (false);	
						cr.switchNormal ();
				}
		}
}