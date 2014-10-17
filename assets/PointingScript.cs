﻿using UnityEngine;
using System.Collections;

public class PointingScript : MonoBehaviour {


	int timeForPointing = 8;
	GameObject displaytext ;

	void Awake ()
	{
		displaytext = GameObject.Find("Displaytext");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.state == ManagerScript.states.pointing)
		{
			//stuff for pointing, waiting for keys computing angles etc goes here
			//transit to walking (new trial) if finisched.

			//ManagerScript.switchState(ManagerScript.states.walking);
		}
	}

	public void NewPointing () {
		Invoke ("toLongPoint", timeForPointing);										
		displaytext.GetComponent<TextMesh> ().text = "Point to Origin";
		Invoke ("clearGUItext", 1f);	
		}

	void toLongPoint(){
		ManagerScript.abortTrial ();
		Debug.Log ("To long for pointing");
//		((GuiScript)(GameObject.Find ("GuiHelper").GetComponent ("GuiScript"))).toSlowPoint ();
	}

	void clearGUItext(){ 		
		displaytext.GetComponent<TextMesh>().text = "" ;
	}

}	
